using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    [Authorize]
    public class ProjectMemberController : Controller
    {
        private readonly IProjectMemberService _projectMemberService;
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public ProjectMemberController(
            IProjectMemberService projectMemberService,
            ITaskService taskService,
            IUserService userService,
            IProjectService projectService)
        {
            _projectMemberService = projectMemberService;
            _taskService = taskService;
            _userService = userService;
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            ViewBag.Tasks = _taskService.GetAll();
            ViewBag.Users = _userService.GetAll();
            ViewBag.Projects = _projectService.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Add(ProjectMember member)
        {
            try
            {
                _projectMemberService.Add(member);

                return RedirectToAction(
                    "Detail",
                    "Project",
                    new { id = member.ProjectId });
            }
            catch
            {
                return RedirectToAction(
                    "Detail",
                    "Project",
                    new { id = member.ProjectId });
            }
        }

        public IActionResult Delete(int id, int projectId)
        {
            _projectMemberService.Delete(id);

            return RedirectToAction(
                "Detail",
                "Project",
                new { id = projectId });
        }
    }
}