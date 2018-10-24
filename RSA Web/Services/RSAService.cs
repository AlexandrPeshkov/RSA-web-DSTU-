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
        public IServiceDirection DirectionService { get; set; }

        public List<Step> Steps { get; set; }

        private Func<ArgumentsVector, double> Function;

        private Func<Step, bool> EvaluateSolutionQuality;

        private Func<Step, bool> IsStop;

        private int MaxStepsCount;

        private double MaxZeroPointValue;

        private double MinZeroPointValue;

        private int ArgumentsCount;

        private double CurrentStepSize;

        private double Extremum;

        public RSAService(IServiceDirection DirectionService)
        {
            this.DirectionService = DirectionService;
            Steps = new List<Step>();
            Function = DefaultFunction;
            EvaluateSolutionQuality = DefaultQualityLevel;
            IsStop = DefaultStop;
        }

        public void InitStartValue(int MaxSteps = 50,
            double StepSize = 100,
            int ArgumentCount = 1,
            int MaxDirectionsCount = 3,
            double MinZeroPointValue = 10,
            double MaxZeroPointValue = 1000)
        {
            this.MaxStepsCount = MaxSteps;
            this.CurrentStepSize = StepSize;
            this.ArgumentsCount = ArgumentCount;
            DirectionService.Size = MaxDirectionsCount;
            this.MinZeroPointValue = MinZeroPointValue;
            this.MaxZeroPointValue = MaxZeroPointValue;
           
            MakeStep(GenerateZeroPoint());
            Extremum = Steps.LastOrDefault().FunctionValue;
        }

        private ArgumentsVector MoveArguments(ArgumentsVector OldArg, double StepSize)
        {
            ArgumentsVector NewArg = new ArgumentsVector();
            for (var i = 0; i < OldArg.X.Count; i++)
            {
                NewArg.X.Add(OldArg.X[i] + DirectionService.CurrentDirection * StepSize);
            }
            return NewArg;
        }

        private void MakeStep(ArgumentsVector NewX)
        {
            Step CurrentStep = new Step()
            {
                StepNumber = Steps.Count,
                StepSize = CurrentStepSize,
                Direction = DirectionService.CurrentDirection,
                Arguments = NewX,
            };

            CurrentStep.FunctionValue = Function(CurrentStep.Arguments);
            CurrentStep.IsGoodSolution = EvaluateSolutionQuality(CurrentStep);
            CurrentStep.IsFinalStep = IsStop(CurrentStep);
            Steps.Add(CurrentStep);
            if(CurrentStep.FunctionValue < Extremum)
            {
                Extremum = CurrentStep.FunctionValue;
            }
        }

        private ArgumentsVector GenerateZeroPoint()
        {
            ArgumentsVector ZeroPoint = new ArgumentsVector();

            for (var i = 0; i < ArgumentsCount; i++)
            {
                var Random = new Random();
                var Value = Random.NextDouble() * (MaxZeroPointValue - MinZeroPointValue) + MinZeroPointValue;
                ZeroPoint.X.Add(Value);
            }
            return ZeroPoint;
        }

        public object[] StartAlghoritmRSA()
        {
            InitStartValue();

            while (!IsStop(Steps.Last()))
            {
                MakeStep(MoveArguments(Steps.Last().Arguments, CurrentStepSize));
                if (!Steps.Last().IsGoodSolution)
                {
                    DirectionService.SetNextDirection();
                }
            }
            return new object[] {Extremum, BestSolution(), DirectionService.Directions, Steps };
        }

        public Step BestSolution()
        {
            return (from step in Steps
                    where step.FunctionValue == Extremum
                    select step).First();
        }

        private double DefaultFunction(ArgumentsVector Argumetns)
        {
            //var Rez = 140 * Math.Pow(Argumetns.X[0], 0.93) + 150 * Math.Pow(Argumetns.X[0], 0.86);

            return Math.Sin(Argumetns.X[0]);
        }

        private bool DefaultQualityLevel(Step CurrentStep)
        {
            return (Steps.Count == 0 || CurrentStep.FunctionValue < Extremum);
        }

        private bool DefaultStop(Step CurrentStep)
        {
            return (Steps.Count >= MaxStepsCount  || (CurrentStep.Direction.index >= DirectionService.Size-1) && CurrentStep.IsGoodSolution==false);
        }
    }
}
