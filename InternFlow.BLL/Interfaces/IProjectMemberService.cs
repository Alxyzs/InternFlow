using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Interfaces
{
    public interface IProjectMemberService
    {
        List<ProjectMember> GetAll();
        List<ProjectMember> GetByProjectId(int projectId);
        void Add(ProjectMember member);
        void Delete(int id);
    }
}