using System;
using System.Collections.Generic;
using System.Text;
using FundooModels;
using FundooRepository.Repository;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {

        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public bool ForgotPassword(string Useremail)
        {
            return repository.ForgotPassword(Useremail);
        }
        public string JWTTokenGeneration(string email)
        {
            try
            {
                return this.repository.JWTTokenGeneration(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //public static string GenerateToken(string username)
            //{
            //    byte[] key = Convert.FromBase64String(Secret);
            //    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            //    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new[] {
            //        new Claim(ClaimTypes.Name, username)
            //    }),
            //        Expires = DateTime.UtcNow.AddMinutes(30),
            //        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //    JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            //    return handler.WriteToken(token);
            //}

        }

    }
}

