using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel userData);
        public string ForgotPassword(string email);
        string JWTTokenGeneration(string email);
        string LogIn(LoginModel login);
        Task<string> ResetPassword(ResetModel reset);

    }
}