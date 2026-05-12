using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    public class ProjectController : Controller
    {

        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            var projects = _projectService.GetAll();
            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            try
            {
                _projectService.Add(project);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(project);
            }
        }

        public IActionResult Edit(int id)
        {
            var project = _projectService.GetById(id);
            return View(project);
        }


        [HttpPost]
        public IActionResult Edit(Project project)
        {
            _projectService.Update(project);
            return RedirectToAction("Index");
        }

 
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}