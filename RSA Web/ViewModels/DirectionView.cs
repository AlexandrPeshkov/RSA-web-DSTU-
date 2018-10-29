using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.ViewModels
{
    [Display(Name ="Направление")]
    public class DirectionView
    {
        [Display(Name = "Значение")]
        public double Value { get; set; }

        [Display(Name = "№")]
        public int Index { get; set; }
    }
}
