using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IRepository<ActivityLog> _repository;

        public ActivityLogService(IRepository<ActivityLog> repository)
        {
            _repository = repository;
        }

        public List<ActivityLog> GetAll() => _repository.GetAll();

        public List<ActivityLog> GetByTaskId(int taskId) => _repository.GetAll().Where(a => a.TaskItemId == taskId).ToList();

        public void Add(ActivityLog log) => _repository.Add(log);
    }
}