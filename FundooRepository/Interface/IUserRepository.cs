using FundooModel;
using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface IUserRepository
    {
        string ForgotPassword(string email);
        string JWTTokenGeneration(string email);
        Task<string> Register(RegisterModel userData);
        string LogIn(LoginModel login);
        Task<string> ResetPassword(ResetModel reset);
        string Encryption(string password);
    }
}