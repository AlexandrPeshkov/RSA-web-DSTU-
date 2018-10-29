using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public partial class Step
    {
        #region Доступные поля

        public int StepNumber { get; set; }

        public double StepSize { get; set; }

        public Direction Direction { get; set; }

        public List<double> Point { get; set; }

        public double FunctionValue { get; set; }

        public bool IsGoodSolution { get; set; }

        public bool IsFinalStep { get; set; }
        #endregion

        #region Операторы
        public static bool operator > (Step left, Step right)
        {
            return left.FunctionValue > right.FunctionValue;
        }

        public static bool operator < (Step left, Step right)
        {
            return left.FunctionValue < right.FunctionValue;
        }
        #endregion
    }


}
