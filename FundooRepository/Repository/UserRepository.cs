
namespace FundooRepository.Repository
{
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

    /// <summary>
    /// repositopry class of user
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
        public string Register(RegisterModel userData)
        {
            try
            {
                //Checking  the Email
                var validEmail = this.userContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();

                if (validEmail == null)
                {
                    if (userData != null)
                    {
                        // Encrypting the password
                        userData.Password = this.Encryption(userData.Password);
                        // Add the data to the database
                        this.userContext.Add(userData);
                        this.userContext.SaveChanges();
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
        /// <param name="password"></param>
        /// <returns>encrypted string password</returns>
        public string Encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
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
                var UserEmail = this.userContext.Users.Where(x => x.Email == logIn.Email).FirstOrDefault();

                if (UserEmail != null)
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

        public bool ForgotPassword(string email)
        {
            //var validUser = this.userContext.Users.Where(x => x.Email == email).FirstOrDefault();
            //if (validUser != null)
            //{
            string url = "Go to this link and reset your Password https://localhost:44396/api/ResetPassword";
            // for sending message in MSMQ
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\FundooQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\FundooQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\FundooQueue");
            }

            Message message = new Message();

            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            msgqueue.Label = "url link";
            msgqueue.Send(message);

            // for reading message from MSMQ
            var receivequeue = new MessageQueue(@".\Private$\FundooQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();

            string linktobesend = receivemsg.Body.ToString();
            if (Sendmail(email, linktobesend))
            {
                return true;
            }
            return false;
            //}
        }
        /// <summary>
        /// sends mail to user email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="message"></param>
        /// <returns>boolean value/returns>
        private bool Sendmail(string email, string message)
        {
            var user = userContext.Users.FirstOrDefault(option => option.Email == "ranig1029@gmail.com");
            MailMessage mailmessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailmessage.From = new MailAddress("ranig1029@gmail.com");
            mailmessage.To.Add(new MailAddress(email));
            mailmessage.Subject = "Link to reset your passord for Fundoo Application";
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("ranig1029@gmail.com", user.FirstName);
            smtp.Send(mailmessage);
            return true;
        }
        /// <summary>
        /// generates token
        /// </summary>
        /// <param name="email"></param>
        /// <returns>token</returns>
        public string JWTTokenGeneration(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["Key"]); //Encrypting secret key
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
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); //creating and validating JWT
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }


    }
}
