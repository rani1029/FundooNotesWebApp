using Experimental.System.Messaging;
using FundooModel;
using FundooModels;
using FundooRepository.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    /// <summary>
    /// repository class of user
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// registers new user
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>string value registration successful or not </returns>
        public async Task<string> Register(RegisterModel userData)
        {
            try
            {
                ////Checking  the Email
                var validEmail = this.userContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();

                if (validEmail == null)
                {
                    if (userData != null)
                    {
                        //// Encrypting the password
                        userData.Password = this.Encryption(userData.Password);
                        //// Add the data to the database
                        this.userContext.Add(userData);
                        await this.userContext.SaveChangesAsync();
                        return "Registration Successful";
                    }
                    return "Registration UnSuccessful";
                }

                return "Email Id Already Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// encrpts the password
        /// </summary>
        /// <param name="password">encrypt</param>
        /// <returns>encrypted string password</returns>
        public string Encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            ////encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            ////Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }

            return encryptdata.ToString();
        }

        /// <summary>
        /// log in existing user
        /// </summary>
        /// <param name="logIn"></param>
        /// <returns>login status string value</returns>
        public string LogIn(LoginModel logIn)
        {
            try
            {
                var userEmail = this.userContext.Users.Where(x => x.Email == logIn.Email).FirstOrDefault();

                if (userEmail != null)
                {
                    logIn.Password = this.Encryption(logIn.Password);
                    var existingPassword = this.userContext.Users.Where(x => x.Password == logIn.Password).FirstOrDefault();
                    if (existingPassword == null)
                    {
                        return "Login UnSuccessful";
                    }
                    else
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "First Name", existingPassword.FirstName);
                        database.StringSet(key: "Last Name", existingPassword.LastName);
                        return "Login Successful ";
                    }
                }
                return "Email Id does not Exist,Please Register first";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// resets the user password
        /// </summary>
        /// <param name="reset"></param>
        /// <returns>string value whether password reset or not</returns>
        public async Task<string> ResetPassword(ResetModel reset)
        {
            try
            {
                var existEmail = this.userContext.Users.Where(x => x.Email == reset.Email).FirstOrDefault(); ////checking the email present in the DB or not
                if (reset != null)
                {
                    ////Encrypting the password
                    existEmail.Password = this.Encryption(reset.NewPassword);
                    ////Update the data in the database
                    this.userContext.Update(existEmail);
                    ////Save the change in database
                    await this.userContext.SaveChangesAsync();
                    return "Password Updated Successfully";
                }

                return "Reset Password is Unsuccssful";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// provide link to user to reset password
        /// </summary>
        /// <param name="email"></param>
        /// <returns>boolean value</returns>
        public string ForgotPassword(string email)
        {
            try
            {
                var existEmail = this.userContext.Users.Where(x => x.Email == email).FirstOrDefault(); ////checking the email present in the DB or not
                if (existEmail != null)
                {
                    ////calling SMTP method to sent mail to the user 
                    this.SMTPmail(email);
                    return "Email sent to user";
                }

                return "Sending Email failed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// sends mail from application
        /// </summary>
        /// <param name="email"></param>
        public void SMTPmail(string email)
        {
            MailMessage mailId = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"); ////allow App to sent email using SMTP 
            mailId.From = new MailAddress(this.Configuration["Credentials:Email"]); ////contain mail id from where maill will send
            mailId.To.Add(email); //// the user mail to which maill will be send
            mailId.Subject = "forgot password issue";
            this.SendMSMQ();
            mailId.Body = this.ReceiveMSMQ();
            smtpServer.Port = 587; ////Port no 
            smtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:Email"], this.Configuration["Credentials:Password"]);
            smtpServer.EnableSsl = true;  ////specify smtpserver use ssl or not, default setting is false
            smtpServer.Send(mailId);
        }

        /// <summary>
        /// sets data to the queue
        /// </summary>
        public void SendMSMQ()
        {
            MessageQueue msgQueue; ////provide access to a queue in MSMQ
                                   ////checking this private queue exists or not
            if (MessageQueue.Exists(@".\Private$\fundooNote"))
            {
                msgQueue = new MessageQueue(@".\Private$\fundooNote"); ////Path for queue
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\fundooNote");
            }

            string body = "Please checkout the below url to create your new password";
            msgQueue.Label = "MailBody"; ////Adding label to queue
                                         ////Sending msg
            msgQueue.Send(body);
        }

        /// <summary>
        /// receives mail
        /// </summary>
        /// <returns>receives msg in queue</returns>
        public string ReceiveMSMQ()
        {
            var receivequeue = new MessageQueue(@".\Private$\fundooNote");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemsg.ToString();
        }

        /// <summary>
        /// generates token
        /// </summary>
        /// <param name="email"></param>
        /// <returns>token</returns>
        public string JWTTokenGeneration(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["Key"]); ////Encrypting secret key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); ////creating and validating JWT
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
