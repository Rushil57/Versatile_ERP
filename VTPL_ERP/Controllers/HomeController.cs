using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTPL_ERP.Models;
using VTPL_ERP.Util;
using ERP.DAL.Abstract;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace VTPL_ERP.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userData;
        public HomeController(IUserService userData)
        {
            _userData = userData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
