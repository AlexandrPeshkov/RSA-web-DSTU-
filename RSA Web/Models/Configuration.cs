using RSA_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class Configuration
    {
        public int MaxStepsCount { get; set; }

        public double MaxZeroPointValue { get; set; }

        public double MinZeroPointValue { get; set; }

        public double CurrentStepSize { get; set; }

        public double FunctionArgumetnsCount { get; set; }

        public List<double> CurrentZeroPoint { get; set; }

        public Func<List<double>, double> Function { get; set; }

        public Func<Step, double?, bool> EvaluateSolutionQuality { get; set; }

        public Func<Step, bool> IsStop { get; set; }

        public Func<double?, double?, bool> ExtremumPredicate { get; set; }

        public static implicit operator Configuration(ConfigurationView ConfigurationView)
        {
            Func<double?, double?, bool> Predicate = null;

            if (ConfigurationView.IsMinimization)
            {
                Predicate = (double? left, double? right) => { return left < right; };
            }
            else
            {
                Predicate = (double? left, double? right) => { return left > right; };
            }

            return new Configuration()
            {
                MaxStepsCount = ConfigurationView.MaxStepsCount,
                MaxZeroPointValue = ConfigurationView.MaxZeroPointValue,
                MinZeroPointValue = ConfigurationView.MinZeroPointValue,
                CurrentStepSize = ConfigurationView.CurrentStepSize,
                FunctionArgumetnsCount = ConfigurationView.FunctionArgumetnsCount,
                ExtremumPredicate = Predicate
            };
        }
    }
}
