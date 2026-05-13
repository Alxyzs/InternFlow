using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    public class ProjectMemberController : Controller
    {
        private readonly IProjectMemberService _projectMemberService;

        public ProjectMemberController(IProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
        }

        //Listeleme
        public IActionResult Index()
        {
            var members = _projectMemberService.GetAll();
            return View(members);
        }

        //Ekleme
        [HttpPost]
        public IActionResult Add(ProjectMember member)
        {
            try
            {
                _projectMemberService.Add(member);
                return RedirectToAction("Detail", "Project", new { id = member.ProjectId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Detail", "Project", new { id = member.ProjectId });
            }
        }

        //Silme
        public IActionResult Delete(int id, int projectId)
        {
            _projectMemberService.Delete(id);
            return RedirectToAction("Detail", "Project", new { id = projectId });
        }
    }
}