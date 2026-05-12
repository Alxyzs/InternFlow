using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IRepository<ProjectMember> _repository;

        public ProjectMemberService(IRepository<ProjectMember> repository)
        {
            _repository = repository;
        }

        public List<ProjectMember> GetAll() => _repository.GetAll();

        public List<ProjectMember> GetByProjectId(int projectId) =>
            _repository.GetAll().Where(m => m.ProjectId == projectId).ToList();

        public void Add(ProjectMember member)
        {
            var existing = _repository.GetAll().FirstOrDefault(m => m.ProjectId == member.ProjectId && m.UserId == member.UserId);

            if (existing != null)
                throw new Exception("Bu kullanıcı zaten bu projenin üyesi!");

            _repository.Add(member);
        }

        public void Delete(int id) => _repository.Delete(id);
    }
}