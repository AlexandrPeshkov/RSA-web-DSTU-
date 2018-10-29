using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.ViewModels
{
    public class ResultView
    {
        public Configuration Configuration { get; set; }

        public List<Direction> Directions { get; set; }

        public List<double> ZeroPoint { get; set; }

        public List<Step> Steps { get; set; }

        [Display(Name ="Лучшее решение")]
        public Step BestSolution { get; set; }

        public static implicit operator ResultView(Dictionary<string,object> Result)
        {
            

            ResultView resultView = new ResultView()
            {
                Configuration = (Configuration)Result["Configuration"],
                Directions = (List<Direction>)Result["Directions"],
                ZeroPoint = (List<double>)Result["ZeroPoint"],
                Steps = (List < Step>)Result["Steps"],
                BestSolution = (Step)Result["BestSolution"],
            };

            return resultView;
        }
    }

}
