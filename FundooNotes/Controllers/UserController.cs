using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FundooModels;
using FundooManager.Manager;
using Microsoft.EntityFrameworkCore;
using FundooRepository.Context;
using FundooNotes.Utilities;
using FundooModel;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace FundooNotes.Controllers
{
    // [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly UserContext _dbContext;
        private readonly IConfiguration configuration;

        public UserController(IUserManager manager, UserContext dbContext, IConfiguration configuration)
        {
            this.manager = manager;
            _dbContext = dbContext;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("api/Register")]
        public IActionResult Register([FromBody] RegisterModel newUser)
        {
            try
            {
                var validEmail = this._dbContext.Users.Where(x => x.Email == newUser.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    var encryptedPassword = EncryptionDecryption.Encryption(newUser.Password);
                    newUser.Password = encryptedPassword;
                    _dbContext.Add(newUser);
                    this._dbContext.SaveChanges();
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Registration Successful", Data = " Session data" });
                }
                else
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration UnSuccessful", Data = " Session data" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/LogIn")]

        public IActionResult LogIn([FromBody] LoginModel login)
        {
            try
            {
                //string result = this.manager.LogIn(login);
                var password = EncryptionDecryption.Encryption(login.Password);
                var validUser = this._dbContext.Users.FirstOrDefault(xyz => xyz.Email == login.Email && xyz.Password == password);
                ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                IDatabase database = connectionMultiplexer.GetDatabase();
                string FirstName = database.StringGet("FirstName");
                string LastName = database.StringGet("LastName");
                string lastName = null;
                RegisterModel register = new RegisterModel
                {
                    FirstName = FirstName,
                    LastName = lastName,
                    //UserId = login.Email,
                    Email = login.Email
                };

                if (validUser != null)
                {
                    //string token = this.manager.JWTTokenGeneration(login.Email);
                    //string token = JWTTokenGeneration(validUser.Email);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "login Successful" });
                }
                else
                    //  return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "login failed" });
                    return this.Unauthorized();

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        [HttpPut]
        [Route("api/ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetModel reset)
        {
            try
            {
                var password = EncryptionDecryption.Encryption(reset.Password);
                if (reset.Password == reset.ConfirmPassword)
                {
                    var validUser = this._dbContext.Users.FirstOrDefault(xyz => xyz.Email == reset.Email);
                    validUser.Password = password;
                    this._dbContext.Users.Update(validUser);
                    this._dbContext.SaveChanges();
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Password changed Successful", Data = " Session data" });
                }
                else
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "try with new password" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        [HttpPost]
        [Route("api/ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPassword email)
        {
            try
            {
                var validUser = this._dbContext.Users.FirstOrDefault(xyz => xyz.Email == email.Email);
                if (validUser != null)
                {
                    manager.ForgotPassword(email.Email);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "A link has been sent to you email", Data = " Session data" });
                }
                else
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "user not registered", Data = " Session data" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        //private string GetToken(string userEmail)
        //{
        //    var claim = new[] { new Claim(JwtRegisteredClaimNames.Email, userEmail) };
        //    var signkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        //    int expiryminutes = Convert.ToInt32(this.configuration["Jwt:ExpiryInMinutes"]);
        //    var tokenValue = new JwtSecurityToken(
        //        issuer: this.configuration["Jwt:Issuer"],
        //        audience: this.configuration["Jwt:Issuer"],
        //        expires: DateTime.UtcNow.AddMinutes(expiryminutes),
        //        signingCredentials: new SigningCredentials(signkey, SecurityAlgorithms.HmacSha256));
        //    var token = new JwtSecurityTokenHandler().WriteToken(tokenValue);
        //    return token;
        //}

    }
}
