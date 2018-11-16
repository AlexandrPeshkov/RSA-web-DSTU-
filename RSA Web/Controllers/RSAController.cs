using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSA_Web.Models;
using RSA_Web.ViewModels;
using RSA_Web.Interfaces;
using RSA_Web.Extensions;

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
        [HttpGet("/")]
        public IActionResult Init()
        {
            return PartialView("Configuration", (ConfigurationView)RSAService.CurrentConfiguration);
        }

        /// <summary>
        /// Применение конфигурации, отправка формы, вывод формы для ввода начальной точки
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>Переход к форме задания начальной точки</returns>
        [HttpPost("/")]
        public IActionResult Init(ConfigurationView configuration)
        {
            if (ModelState.IsValid)
            {
                RSAService.SetConfiguration(configuration);
                return RedirectToAction("StartPoint");
            }
            return View("Configuration", RSAService.DefaultConfiguration);
        }

        /// <summary>
        /// Получить рандомное значение начальной точки
        /// </summary>
        /// <returns>Случайная точка</returns>
        [HttpGet("/X")]
        public IActionResult StartPoint()
        {
            var Point = RSAService.GenerateZeroPoint();
            return PartialView("Point", Point);
        }

        /// <summary>
        /// Устанавлиеваем начальную точку, запускаем алгоритм
        /// </summary>
        /// <param name="Point"></param>
        /// <returns></returns>
        [HttpPost("/X")]
        public IActionResult StartPoint(List<double> Point)
        {
            RSAService.ZeroPoint = Point;
            return RedirectToAction("Directions");
        }

        /// <summary>
        /// Получить направления
        /// </summary>
        /// <returns>Список направлений, дробные числа в [0;1]</returns>
        [HttpGet("/Dirs")]
        public IActionResult Directions()
        {
            return PartialView("Directions", RSAService.GenerateDirections().ToView());
        }

        /// <summary>
        /// Задать направления вручную
        /// </summary>
        /// <param name="Directions"></param>
        /// <returns>Ок</returns>
        [HttpPost("/Dirs")]
        public IActionResult Directions(List<Direction> Directions)
        {
            RSAService.Directions = Directions;
            return RedirectToAction("Start");
        }

        /// <summary>
        /// Запуск работы алгоритма RSA
        /// </summary>
        /// <returns>Экстремум, Лучший шаг, Список направлений, Все шаги</returns>
        [HttpGet("/Solution")]
        public IActionResult Start()
        {
            return PartialView("Result", (ResultView)RSAService.StartAlghoritmRSA());
        }
    }
}
