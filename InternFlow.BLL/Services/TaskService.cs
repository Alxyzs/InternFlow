using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskItem> _repository;

        public TaskService(IRepository<TaskItem> repository)
        {
            _repository = repository;
        }

        public List<TaskItem> GetAll() => _repository.GetAll();

        public TaskItem GetById(int id) => _repository.GetById(id);

        public void Add(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Title))
                throw new Exception("Görev başlığı boş olamaz!");

            _repository.Add(task);
        }

        public void Update(TaskItem task) => _repository.Update(task);

        public void Delete(int id) => _repository.Delete(id);

        public void UpdateStatus(int id, string status)
        {
            var task = _repository.GetById(id);

            if (task == null)
                throw new Exception("Görev bulunamadı!");

            task.Status = status;
            _repository.Update(task);
        }
    }
}