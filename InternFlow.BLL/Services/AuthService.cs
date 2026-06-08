using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;
using System.Security.Cryptography;
using InternFlow.EL.Enums;
using System.Text;

namespace InternFlow.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;

        public AuthService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Login(string username, string password)
        {
            var user = _userRepository.GetAll()
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
                return null;

            // Şifreyi kontrol et
            if (!VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        public void Register(User user, string password)
        {
            var existing = _userRepository.GetAll()
                .FirstOrDefault(u => u.Username == user.Username);

            if (existing != null)
                throw new Exception("Username zaten alınmış!");

            user.Password = HashPassword(password);

            if (string.IsNullOrEmpty(user.Role))
                user.Role = UserRole.Stajyer.ToString();


            _userRepository.Add(user);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}