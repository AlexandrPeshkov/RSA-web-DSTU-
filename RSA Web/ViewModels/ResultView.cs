﻿using Microsoft.AspNetCore.Mvc;
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
        public ConfigurationView Configuration { get; set; }

        public List<DirectionView> Directions { get; set; }

        public List<double> ZeroPoint { get; set; }

        public List<StepView> Steps { get; set; }

        public StepView BestSolution { get; set; }

        public static implicit operator ResultView(object[] Result)
        {

            ResultView resultView = new ResultView()
            {
                Configuration  = (ConfigurationView)Result[0],
                Directions = (List<DirectionView>)Result[1],
                ZeroPoint = (List<double>)Result[2],
                Steps = (List<StepView>)Result[3],
                BestSolution = (StepView)Result[4]
            };
            return resultView;
        }
    }

}
