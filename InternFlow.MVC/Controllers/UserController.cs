using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
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

                // 🔥 CRITICAL FIX: tracked entity yerine temiz update
                existing.FullName = user.FullName;
                existing.Email = user.Email;

                _userService.Update(existing);

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
            return RedirectToAction("Index");
        }
    }
}