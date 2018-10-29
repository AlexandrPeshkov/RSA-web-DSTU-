using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RSA_Web.Interfaces;
using RSA_Web.Models;
using RSA_Web.ViewModels;

namespace RSA_Web.Services
{

    public class RSAService : IServiceRSA
    {
        #region Сервисы
        private IServiceDirection DirectionService { get; set; }

        private IServiceRSAConfiguration ConfigurationService { get; set; }
        #endregion

        #region Поля
        /// <summary>
        /// Значение текущего экстремума функции
        /// </summary>
        private double? CurrentExtremum { get; set; }

        /// <summary>
        /// Лучший шаг
        /// </summary>
        private Step BestSolution { get; set; }

        /// <summary>
        /// Список шагов
        /// </summary>
        private List<Step> Steps { get; set; }

        /// <summary>
        /// Конфигурация по умолчанию
        /// </summary>
        public Configuration DefaultConfiguration
        {
            get
            {
                return ConfigurationService.DefaultConfiguration;
            }
        }

        /// <summary>
        /// Текущая конфигурация
        /// </summary>
        public Configuration CurrentConfiguration
        {
            get
            {
                return ConfigurationService.CurrentConfiguration;
            }
        }

        /// <summary>
        /// Направления в сервисе направлений
        /// </summary>
        public List<Direction> Directions
        {
            get
            {
                return DirectionService.Directions;
            }
            set
            {
                DirectionService.Directions = value;
            }
        }

        /// <summary>
        /// Начальная точка
        /// </summary>
        /// <param name="Point"></param>
        public List<double> ZeroPoint
        {
            get
            {
                return ConfigurationService.CurrentConfiguration.ZeroPoint;
            }
            set
            {
                ConfigurationService.CurrentConfiguration.ZeroPoint = value;
            }
        }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="DirectionService"></param>
        /// <param name="ConfigurationService"></param>
        public RSAService(IServiceDirection DirectionService, IServiceRSAConfiguration ConfigurationService)
        {
            this.DirectionService = DirectionService;
            this.ConfigurationService = ConfigurationService;

            Steps = new List<Step>();
            CurrentExtremum = null;
        }

        /// <summary>
        /// Перемещает точку 
        /// </summary>
        /// <param name="Point"></param>
        /// <param name="StepSize"></param>
        /// <returns></returns>
        private List<double> MoveArguments(List<double> Point)
        {
            List<double> NewPoint = new List<double>();
            for (var i = 0; i < Point.Count; i++)
            {
                NewPoint.Add(Point[i] + DirectionService.CurrentDirection * ConfigurationService.CurrentConfiguration.StepSize);
            }
            return NewPoint;
        }

        private Step MakeStep(List<double> Arguments)
        {
            Step CurrentStep = new Step()
            {
                StepNumber = Steps.Count,
                StepSize = ConfigurationService.CurrentConfiguration.StepSize,
                Direction = DirectionService.CurrentDirection,
                Point = Arguments,
            };

            CurrentStep.FunctionValue = ConfigurationService.CurrentConfiguration.Function(CurrentStep.Point);
            CurrentStep.IsGoodSolution = ConfigurationService.CurrentConfiguration.EvaluateSolutionQuality(CurrentStep, CurrentExtremum);
            CurrentStep.IsFinalStep = ConfigurationService.CurrentConfiguration.IsStop(CurrentStep);

            if (BestSolution == null || CurrentStep < BestSolution)
            {
                BestSolution = CurrentStep;
                CurrentExtremum = CurrentStep.FunctionValue;
            }

            return CurrentStep;
        }

        //I
        public void SetConfiguration(Configuration UserConfiguration)
        {
            UserConfiguration.Function = ConfigurationService.DefaultConfiguration.Function;
            UserConfiguration.EvaluateSolutionQuality = ConfigurationService.DefaultConfiguration.EvaluateSolutionQuality;
            UserConfiguration.IsStop = ConfigurationService.DefaultConfiguration.IsStop;
            UserConfiguration.ZeroPoint = ConfigurationService.DefaultConfiguration.ZeroPoint;

            DirectionService.Size = UserConfiguration.DirectionsCount;
            ConfigurationService.CurrentConfiguration = UserConfiguration;
        }

        //II get
        public List<double> GenerateZeroPoint()
        {
            List<double> Point = new List<double>();

            for (var i = 0; i < ConfigurationService.CurrentConfiguration.FunctionArgumetnsCount; i++)
            {
                var Random = new Random();
                var Value = Random.NextDouble() * (ConfigurationService.CurrentConfiguration.MaxZeroPointValue - ConfigurationService.CurrentConfiguration.MinZeroPointValue) + ConfigurationService.CurrentConfiguration.MinZeroPointValue;
                Point.Add(Value);
            }
            this.ZeroPoint = Point;
            return Point;
        }

        //II Post

        // Zero Step
        public void MakeZeroStep()
        {
            Step ZeroStep = MakeStep(ConfigurationService.CurrentConfiguration.ZeroPoint);
            Steps.Add(ZeroStep);
        }

        private void ResetState()
        {
            DirectionService.ResetDirectionsPointer();
            CurrentExtremum = null;
            BestSolution = null;
            Steps = new List<Step>();
        }

        public List<Direction> GenerateDirections()
        {
            return DirectionService.GenerateDirections();
        }

        // III
        public Dictionary<string, object> StartAlghoritmRSA()
        {
            ResetState();
            MakeZeroStep();

            while (!ConfigurationService.CurrentConfiguration.IsStop(Steps.LastOrDefault()))
            {
                List<double> NextPoint = MoveArguments(Steps.LastOrDefault().Point);
                Step CurrentStep = MakeStep(NextPoint);
                Steps.Add(CurrentStep);
                if (!Steps.Last().IsGoodSolution)
                {
                    DirectionService.SetNextDirection();
                }
            }

            ResultView result = new ResultView()
            {
                //Configuration = ConfigurationService.CurrentConfiguration,
                //Directions = Directions,
                //ZeroPoint = ZeroPoint,
                Steps = Steps,
                //BestSolution = BestSolution
            };

            Dictionary<string, object> Result = new Dictionary<string, object>()
            {
                {
                    "Configuration",
                     ConfigurationService.CurrentConfiguration
                },
                {
                    "Directions",
                    Directions
                },
                {
                    "ZeroPoint",
                    ZeroPoint
                },
                {
                    "Steps",
                    Steps
                },
                {
                    "BestSolution",
                    BestSolution
                }
            };
            return Result;
        }

    }
}
