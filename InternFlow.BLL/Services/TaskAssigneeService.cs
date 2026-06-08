using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class TaskAssigneeService : ITaskAssigneeService
    {
        private readonly IRepository<TaskAssignee> _repository;

        public TaskAssigneeService(IRepository<TaskAssignee> repository)
        {
            _repository = repository;
        }

        public List<TaskAssignee> GetAll() => _repository.GetAll();

        public List<TaskAssignee> GetByTaskId(int taskId) => _repository.GetAll().Where(t => t.TaskItemId == taskId).ToList();

        public void Add(TaskAssignee taskAssignee)
        {
            var existing = _repository.GetAll().FirstOrDefault(t => t.TaskItemId == taskAssignee.TaskItemId && t.UserId == taskAssignee.UserId);

            if (existing != null)
                throw new Exception("This user is already assigned to this task ! ");

            _repository.Add(taskAssignee);
        }

        public void Delete(int id) => _repository.Delete(id);
    }
}
