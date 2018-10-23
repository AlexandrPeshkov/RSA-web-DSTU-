using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class StepModel
    {
        public int StepNumber;
        public DirectionModel Direction;
        public ArgumentModel X;

        public StepModel(int StepNumber)
        {
            this.StepNumber = StepNumber;
            Direction = new DirectionModel();
            X = new ArgumentModel();
        }
    }

}
