using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;

namespace RSA_Web.ViewModels
{
    public class ConfigurationView
    {
        [Required]
        [Display(Name = "Арность функции (N)")]
        public uint FunctionArgumetnsCount { get; set; }

        [Required]
        [Display(Name ="Лимит шагов (K)")]
        [Range(0,10000)]
        public uint StepsLimit { get; set; }

        [Required]
        [Display(Name = "Величина шага (h)")]
        public double StepSize { get; set; }

        [Required]
        [Display(Name = "Нижний порог значений начальной точки (Xmin)")]
        public double MinZeroPointValue { get; set; }

        [Required]
        [Display(Name = "Верхний порог значений начальной точки (Xmax)")]
        public double MaxZeroPointValue { get; set; }

        [Required]
        [Display(Name = "Число направлений (M)")]
        public uint DirectionsCount { get; set; }

        [Required]
        [Display(Name = "Задача минимизации")]
        public bool IsMinimization { get; set; }
    }
}

