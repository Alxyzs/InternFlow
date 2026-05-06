using InternFlow.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace InternFlow.MVC.Controllers
{
    public class HomeController : Controller
    {

        //private readonly DataContext _Db;
        //public HomeController(DataContext db)
        //{
        //    _db = db;
        //}   

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ErrorViewModel md = new();

            //DataContext db = new DataContext(); örnek 
            //md.RequestId = "2";

            var jsonVerisi = JsonConvert.SerializeObject(md);

            return Json(jsonVerisi);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
