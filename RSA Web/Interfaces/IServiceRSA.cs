using Microsoft.AspNetCore.Mvc;
using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Interfaces
{
    public interface IServiceRSA
    {
        object[]  StartAlghoritmRSA();
        void SetConfiguration(Configuration configuration);
        Configuration DefaultConfiguration { get; }
        Configuration CurrentConfiguration { get; }
        List<double> ZeroPoint { get; set; }
        List<double> GenerateZeroPoint();
        List<Direction> GenerateDirections();
        List<Direction> Directions { get; set; }
    }
}
