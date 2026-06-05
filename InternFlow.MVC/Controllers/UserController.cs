using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace InternFlow.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var users = _userService.GetAll();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _userService.Add(user);
                TempData["Success"] = "User created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                if (user == null || user.Id == 0)
                    return RedirectToAction("Index");
                var existing = _userService.GetById(user.Id);
                if (existing == null)
                    return RedirectToAction("Index");
                existing.FullName = user.FullName;
                existing.Email = user.Email;
                _userService.Update(existing);
                TempData["Success"] = "User updated successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            TempData["Success"] = "User deleted successfully.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ChangeRole(int id, string role)
        {
            var user = _userService.GetById(id);
            user.Role = role;
            _userService.Update(user);
            TempData["Success"] = $"Role changed to {role} successfully.";
            return RedirectToAction("Index");
        }
    }
}