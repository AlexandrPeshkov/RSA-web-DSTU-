using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSA_Web.Models;
using RSA_Web.ViewModels;
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

        /// <summary>
        /// Вывод констант и формы для ввода параметров 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Init()
        {
            return View("Init",new ConfigurationView());
        }

        /// <summary>
        /// Применение конфигурации, отправка формы, вывод формы для ввода начальной точки
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Init(ConfigurationView configuration)
        {
            RSAService.SetConfiguration(configuration);
            return View("ZeroPoint", new ZeroPointView());
        }

        /// <summary>
        /// Получить рандомное значение начальной точки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetStart()
        {
            return View("ZeroPoint", RSAService.GenerateZeroPoint());
        }

        /// <summary>
        /// Устанавлиеваем начальную точку, запускаем алгоритм
        /// </summary>
        /// <param name="Point"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetStart(List<double> Point)
        {
            RSAService.SetZeroPoint(Point);
            return View("Steps", RSAService.StartAlghoritmRSA());
        }
    }
}
