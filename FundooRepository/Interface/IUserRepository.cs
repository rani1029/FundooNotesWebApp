using FundooModels;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Repository
{
    public interface IUserRepository
    {
        public bool ForgotPassword(string email);
        string JWTTokenGeneration(string email);
    }
}