using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<TaskComment> _repository;

        public CommentService(IRepository<TaskComment> repository)
        {
            _repository = repository;
        }

        public List<TaskComment> GetAll() => _repository.GetAll();

        public List<TaskComment> GetByTaskId(int taskId) => _repository.GetAll().Where(c => c.TaskItemId == taskId).ToList();

        public void Add(TaskComment comment)
        {
            if (string.IsNullOrEmpty(comment.Content))
                throw new Exception("Yorum boş olamaz!");

            _repository.Add(comment);
        }

        public void Delete(int id) => _repository.Delete(id);
    }
}