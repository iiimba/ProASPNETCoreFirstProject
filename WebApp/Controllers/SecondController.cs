﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Index()
        {
            return View("Common");
        }
    }
}
