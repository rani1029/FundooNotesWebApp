using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FundooModels;
using FundooManager.Manager;
using Microsoft.EntityFrameworkCore;
using FundooRepository.Context;
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

    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        private readonly IConfiguration configuration;

        public UserController(IUserManager manager, IConfiguration configuration)
        {
            this.manager = manager;

            this.configuration = configuration;
        }
        /// <summary>
        /// api to register new user
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>registration status successful or not</returns>
        [HttpPost]
        [Route("api/Register")]
        public IActionResult Register([FromBody] RegisterModel userData)
        {
            try
            {
                var validEmail = this.manager.Register(userData);
                if (validEmail.Equals("Registration Successful"))
                {

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
        /// <summary>
        /// api to user login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>string value user login successful or not</returns>
        [HttpPost]
        [Route("api/LogIn")]
        public IActionResult LogIn([FromBody] LoginModel login)
        {
            try
            {
                string result = this.manager.LogIn(login);
                if (result.Equals("Login Successful "))
                {

                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    int userId = Convert.ToInt32(database.StringGet("User Id"));
                    RegisterModel data = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = login.Email,

                    };
                    string token = this.manager.JWTTokenGeneration(login.Email);
                    return this.Ok(new { Status = true, Message = result, Data = data, Token = token });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }

            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        /// <summary>
        /// api to reset user password
        /// </summary>
        /// <param name="reset"></param>
        /// <returns>response</returns>
        [HttpPut]
        [Route("api/ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetModel reset)
        {
            try
            {
                string result = await this.manager.ResetPassword(reset);

                if (result.Equals("Password Updated Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// api to forget password
        /// </summary>
        /// <param name="email"></param>
        /// <returns>reponse</returns>

        [HttpPost]
        [Route("api/ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                bool validUser = this.manager.ForgotPassword(email);
                if (validUser == true)
                {
                    //manager.ForgotPassword(email.Email);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "A link has been sent to you email", Data = " Session data" });
                }
                else
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "user not registered", Data = " Session data" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
            //}

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
}
