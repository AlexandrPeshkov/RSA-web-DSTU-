using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.ViewModels
{
    [Display(Name ="Лучшее решение")]
    public class BestStepView
    {
        [Display(Name = "Номер шага")]
        public int StepNumber { get; set; }

        [Display(Name = "Размер шага")]
        public double StepSize { get; set; }

        [Display(Name = "Направление")]
        public Direction Direction { get; set; }

        [Display(Name = "Точка")]
        public List<double> Point { get; set; }

        [Display(Name = "Значение функции")]
        public double FunctionValue { get; set; }

        [Display(Name = "Оценка решения")]
        public bool IsGoodSolution { get; set; }

        [Display(Name = "Выполнение критерия остановки")]
        public bool IsFinalStep { get; set; }
    }
}
