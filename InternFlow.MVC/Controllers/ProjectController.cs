    using InternFlow.BLL.Interfaces;
    using InternFlow.EL.DBContextModels;
    using InternFlow.MVC.Hubs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    namespace InternFlow.MVC.Controllers
    {
        [Authorize]
        public class ProjectController : Controller
        {
            private readonly IProjectService _projectService;
            private readonly IHubContext<NotificationHub> _hubContext;

            public ProjectController(IProjectService projectService, IHubContext<NotificationHub> hubContext)
            {
                _projectService = projectService;
                _hubContext = hubContext;
            }


            public IActionResult Index(string status)
            {
                var projects = _projectService.GetAll();

                if (!string.IsNullOrEmpty(status))
                {
                    projects = projects
                        .Where(p => p.Status == status)
                        .ToList();
                }

                ViewBag.CurrentFilter = status;

                return View(projects);
            }

            public IActionResult Create()
            {
                return View();
            }


            [HttpPost]
            public async Task<IActionResult> Create(Project project)
            {
                try
                {
                    _projectService.Add(project);

                    TempData["Success"] = "Project created successfully.";

                    await _hubContext.Clients.All.SendAsync(
                        "ReceiveProjectNotification",
                        project.Name
                    );

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

                TempData["Success"] = "Project updated successfully.";

                return RedirectToAction("Index");
            }


           public IActionResult Delete(int id)
    {
        _projectService.Delete(id);

        TempData["Success"] = "Project deleted successfully.";

        return RedirectToAction("Index");
    }
        }
    }