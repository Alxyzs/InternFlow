using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Interfaces
{
    public interface ITaskService
    {
        List<TaskItem> GetAll();
        TaskItem GetById(int id);
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(int id);
        void UpdateStatus(int id, string status);
    }
}