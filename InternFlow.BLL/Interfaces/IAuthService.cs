using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Interfaces
{
    public interface IAuthService
    {
        User? Login(string username, string password);
        void Register(User user, string password);
        string HashPassword(string password);
    }
}