using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;

namespace RSA_Web.ViewModels
{
    public class ConfigurationView
    {
        public double FunctionArgumetnsCount { get; set; }

        public int MaxStepsCount { get; set; }

        public double MinZeroPointValue { get; set; }

        public double MaxZeroPointValue { get; set; }

        public double CurrentStepSize { get; set; }

        public bool IsMinimization { get; set; }

    }
}

