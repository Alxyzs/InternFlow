using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Interfaces
{
    public interface IActivityLogService
    {
        List<ActivityLog> GetAll();
        List<ActivityLog> GetByTaskId(int taskId);
        void Add(ActivityLog log);
    }
}