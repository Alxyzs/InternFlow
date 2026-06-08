using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Interfaces
{
    public interface ITaskAssigneeService
    {
        List<TaskAssignee> GetAll();
        List<TaskAssignee> GetByTaskId(int taskId);
        void Add(TaskAssignee taskAssignee);
        void Delete(int id);
    }
}