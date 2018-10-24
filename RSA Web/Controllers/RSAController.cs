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
    public class RSA : Controller
    {

        private readonly IServiceRSA RSAService;

        public RSA(IServiceRSA RSAService)
        {
            this.RSAService = RSAService;
        }

        [HttpGet]
        public IActionResult Init()
        {
            return Json(RSAService.StartAlghoritmRSA());
        }

    }
}
