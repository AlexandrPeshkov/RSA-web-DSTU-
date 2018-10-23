using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSA_Web.Models;
using RSA_Web.Interfaces;

namespace RSA_Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IDirection DirectionService;

        public HomeController(IDirection DirectionService)
        {
            this.DirectionService = DirectionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Json(DirectionService.Directions);
        }

        [HttpPost]
        public IActionResult Index(int Size)
        {
            DirectionService.Size = Size;
            return Json(DirectionService.Direction);
        }

    }
}
