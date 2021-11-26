using Experimental.System.Messaging;
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

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        public string Register(RegisterModel userData)
        {
            try
            {
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
        public string LogIn(LoginModel logIn)
        {
            try
            {
                var UserEmail = this.userContext.Users.Where(x => x.Email == logIn.Email).FirstOrDefault();
                ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                IDatabase database = connectionMultiplexer.GetDatabase();
                string FirstName = database.StringGet("FirstName");
                string LastName = database.StringGet("LastName");
                //Httpse
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

        private string Encryption(string password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string email)
        {
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
        }

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
