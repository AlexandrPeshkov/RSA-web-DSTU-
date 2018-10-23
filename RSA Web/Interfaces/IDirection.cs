using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;

namespace RSA_Web.Interfaces
{
    public interface IDirection
    {
        DirectionModel Direction { get; }
        List<DirectionModel> Directions { get; }
        int Size { set; get; }
        void SetNextDirection();
    }
}
