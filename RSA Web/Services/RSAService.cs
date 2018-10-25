using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Interfaces;
using RSA_Web.Models;

namespace RSA_Web.Services
{

    public class RSAService : IServiceRSA
    {
        private double? CurrentExtremum { get; set; }

        private IServiceDirection DirectionService { get; set; }

        private IServiceRSAConfiguration ConfigurationService { get; set; }

        private List<Step> Steps { get; set; }

        public RSAService(IServiceDirection DirectionService, IServiceRSAConfiguration ConfigurationService)
        {
            this.DirectionService = DirectionService;
            this.ConfigurationService = ConfigurationService;

            Steps = new List<Step>();
            CurrentExtremum = null;
        }

        private List<double> MoveArguments(List<double> OldArg, double StepSize)
        {
            List<double> NewArg = new List<double>();
            for (var i = 0; i < OldArg.Count; i++)
            {
                NewArg.Add(OldArg[i] + DirectionService.CurrentDirection * StepSize);
            }
            return NewArg;
        }

        private Step MakeStep(List<double> Arguments)
        {
            Step CurrentStep = new Step()
            {
                StepNumber = Steps.Count,
                StepSize = ConfigurationService.CurrentConfiguration.CurrentStepSize,
                Direction = DirectionService.CurrentDirection,
                Arguments = Arguments,
            };

            CurrentStep.FunctionValue = ConfigurationService.CurrentConfiguration.Function(CurrentStep.Arguments);
            CurrentStep.IsGoodSolution = ConfigurationService.CurrentConfiguration.EvaluateSolutionQuality(CurrentStep, CurrentExtremum);
            CurrentStep.IsFinalStep = ConfigurationService.CurrentConfiguration.IsStop(CurrentStep);

            if (CurrentStep.FunctionValue < CurrentExtremum || CurrentExtremum==null)
            {
                CurrentExtremum = CurrentStep.FunctionValue;
            }

            return CurrentStep;
        }

        private Step BestSolution()
        {
            return (from step in Steps
                    where step.FunctionValue == CurrentExtremum
                    select step).First();
        }

        //I
        public void SetConfiguration(Configuration UserConfiguration)
        {
            UserConfiguration.Function = ConfigurationService.DefaultConfiguration.Function;
            UserConfiguration.EvaluateSolutionQuality = ConfigurationService.DefaultConfiguration.EvaluateSolutionQuality;
            UserConfiguration.IsStop = ConfigurationService.DefaultConfiguration.IsStop;

            ConfigurationService.CurrentConfiguration = UserConfiguration;
        }

        //II get
        public List<double> GenerateZeroPoint()
        {
            List<double> ZeroPoint = new List<double>();

            for (var i = 0; i < ConfigurationService.CurrentConfiguration.FunctionArgumetnsCount; i++)
            {
                var Random = new Random();
                var Value = Random.NextDouble() * (ConfigurationService.CurrentConfiguration.MaxZeroPointValue - ConfigurationService.CurrentConfiguration.MinZeroPointValue) + ConfigurationService.CurrentConfiguration.MinZeroPointValue;
                ZeroPoint.Add(Value);
            }
            SetZeroPoint(ZeroPoint);
            return ZeroPoint;
        }

        //II Post
        public void SetZeroPoint(List<double> Point)
        {
            ConfigurationService.CurrentConfiguration.CurrentZeroPoint = Point;
        }

        // III
        public object[] StartAlghoritmRSA()
        {
            while (!ConfigurationService.CurrentConfiguration.IsStop(Steps.Last()))
            {
                List<double> MovedArguments = MoveArguments(Steps.Last().Arguments, ConfigurationService.CurrentConfiguration.CurrentStepSize);
                Step CurrentStep = MakeStep(MovedArguments);
                Steps.Add(CurrentStep);
                if (!Steps.Last().IsGoodSolution)
                {
                    DirectionService.SetNextDirection();
                }
            }
            return new object[] { CurrentExtremum, BestSolution(), DirectionService.Directions, Steps };
        }

       

    }
}
