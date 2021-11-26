using FundooModels;

namespace FundooManager.Manager
{
    public interface IUserManager
    {
        public bool ForgotPassword(string email);
        string JWTTokenGeneration(string email);
    }
}