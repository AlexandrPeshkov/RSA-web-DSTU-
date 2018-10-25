using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class Step
    {
        public int StepNumber { get; set; }

        public double StepSize { get; set; }

        public Direction Direction { get; set; }

        public List<double> Arguments { get; set; }

        public double FunctionValue { get; set; }

        public bool IsGoodSolution { get; set; }

        public bool IsFinalStep { get; set; }
    }
}
