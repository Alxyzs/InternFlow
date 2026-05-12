using InternFlow.BLL.Interfaces;
using InternFlow.DAL.Interfaces;
using InternFlow.EL.DBContextModels;

namespace InternFlow.BLL.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IRepository<Project> _repositoryDAL;


        public ProjectService(IRepository<Project> repositoryDAL)
        {
            _repositoryDAL = repositoryDAL;
        }

        public List<Project> GetAll() => _repositoryDAL.GetAll();

        public Project GetById(int id) => _repositoryDAL.GetById(id);

        public void Add(Project project)
        {
            var existing = _repositoryDAL.GetAll().FirstOrDefault(p => p.Name == project.Name);

            if (existing != null)
                throw new Exception("Bu isimde bir proje zaten var!");

            _repositoryDAL.Add(project);
        }

        public void Update(Project project) => _repositoryDAL.Update(project);

        public void Delete(int id) => _repositoryDAL.Delete(id);
    }
}