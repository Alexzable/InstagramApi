using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CallBackController ctrl = new CallBackController();
        //public ActionResult Auhorizate()
        //{
        //    string accesTokenG = ctrl.Authorization(ctrl.GetCode());
        //    if (accesTokenG != "")
        //        return Redirect(ConfigurationManager.AppSettings["localhost2"]);

        //    return Redirect(ConfigurationManager.AppSettings["localhost"]);
        //}
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
