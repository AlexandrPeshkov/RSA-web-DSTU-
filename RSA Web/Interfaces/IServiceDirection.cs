using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;

namespace RSA_Web.Interfaces
{
    public interface IServiceDirection
    {
        Direction CurrentDirection { get; }
        List<Direction> Directions { get; set; }
        uint Size { set; get; }

        void SetNextDirection();
        List<Direction> GenerateDirections();
        void ResetDirectionsPointer();
    }
}
