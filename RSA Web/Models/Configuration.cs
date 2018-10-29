using RSA_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class Configuration
    {
        public uint StepsLimit { get; set; }

        public double MaxZeroPointValue { get; set; }

        public double MinZeroPointValue { get; set; }

        public double StepSize { get; set; }

        public uint FunctionArgumetnsCount { get; set; }

        public uint DirectionsCount { get; set; }

        public List<double> ZeroPoint { get; set; }

        public Func<List<double>, double> Function { get; set; }

        public Func<Step, double?, bool> EvaluateSolutionQuality { get; set; }

        public Func<Step, bool> IsStop { get; set; }

        public Func<double?, double?, bool> ExtremumPredicate { get; set; }

        public static implicit operator ConfigurationView(Configuration Configuration)
        {
            return new ConfigurationView()
            {
                FunctionArgumetnsCount = Configuration.FunctionArgumetnsCount,
                StepsLimit = Configuration.StepsLimit,
                MaxZeroPointValue = Configuration.MaxZeroPointValue,
                MinZeroPointValue = Configuration.MinZeroPointValue,
                StepSize = Configuration.StepSize,
                DirectionsCount = Configuration.DirectionsCount,
                IsMinimization = Configuration.ExtremumPredicate(double.MinValue, double.MaxValue)
            };
        }

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
                StepsLimit = ConfigurationView.StepsLimit,
                MaxZeroPointValue = ConfigurationView.MaxZeroPointValue,
                MinZeroPointValue = ConfigurationView.MinZeroPointValue,
                DirectionsCount = ConfigurationView.DirectionsCount,
                StepSize = ConfigurationView.StepSize,
                FunctionArgumetnsCount = ConfigurationView.FunctionArgumetnsCount,
                ExtremumPredicate = Predicate
            };
        }
    }
}
