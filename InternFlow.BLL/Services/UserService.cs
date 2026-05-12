using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public List<User> GetAll() => _repository.GetAll();

        public User GetById(int id) => _repository.GetById(id);

        public void Add(User user)
        {
            var existing = _repository.GetAll().FirstOrDefault(u => u.Email == user.Email);

            if (existing != null)
                throw new Exception("Bu email zaten kayıtlı!");

            _repository.Add(user);
        }

        public void Update(User user) => _repository.Update(user);

        public void Delete(int id) => _repository.Delete(id);
    }
}