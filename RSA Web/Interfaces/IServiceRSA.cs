using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Interfaces
{
    public interface IServiceRSA
    {
        object[] StartAlghoritmRSA();
        void SetConfiguration(Configuration configuration);
        List<double> GenerateZeroPoint();
        void SetZeroPoint(List<double> Point);
    }
}
