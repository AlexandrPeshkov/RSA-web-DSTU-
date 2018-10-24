using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Interfaces
{
    public interface IServiceRSA
    {
        void InitStartValue(
            int MaxSteps,
            double StepSize,
            int ArgumentCount ,
            int MaxDirectionsCount,
            double MinZeroPointValue ,
            double MaxZeroPointValue );
        object[] StartAlghoritmRSA();
        List<Step> Steps { get; set; }

        IServiceDirection DirectionService { get; set; }
    }
}
