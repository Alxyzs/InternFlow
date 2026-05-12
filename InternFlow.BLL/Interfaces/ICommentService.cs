using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Interfaces
{
    public interface ICommentService
    {
        List<TaskComment> GetAll();
        List<TaskComment> GetByTaskId(int taskId);
        void Add(TaskComment comment);
        void Delete(int id);
    }
}