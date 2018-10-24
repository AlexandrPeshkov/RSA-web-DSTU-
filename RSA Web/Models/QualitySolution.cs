using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class QualitySolution
    {
        private Func<double, bool> QualityMethod { get; set; }

        public bool Quality(double SolutionValue)
        {
            return QualityMethod(SolutionValue);
        }

        public QualitySolution(Func<double, bool> QualityMethod)
        {
            this.QualityMethod = QualityMethod;
        }
    }
}
