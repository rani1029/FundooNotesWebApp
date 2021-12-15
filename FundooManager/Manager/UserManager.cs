using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public Task<string> Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string LogIn(LoginModel login)
        {
            try
            {
                return this.repository.LogIn(login);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> ResetPassword(ResetModel reset)
        {
            try
            {
                return await this.repository.ResetPassword(reset);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ForgotPassword(string email)
        {
            return this.repository.ForgotPassword(email);
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
        }
        public string Encryption(string password)
        {
            try
            {
                return this.repository.Encryption(password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

