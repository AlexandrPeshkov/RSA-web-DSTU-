using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;

namespace RSA_Web.Services
{
    public class RSAService
    {
        List<StepModel> Steps;
        List<DirectionModel> Directions;

        public RSAService()
        {
            Steps = new List<StepModel>();
        }

        void InitializeValues()
        {
            Steps.Add(new StepModel(Steps.Count));
        }


    }
}
