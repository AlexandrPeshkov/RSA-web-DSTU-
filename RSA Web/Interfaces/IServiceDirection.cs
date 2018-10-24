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
        List<Direction> Directions { get; }
        int Size { set; get; }
        void SetNextDirection();
        void InitialDirections(int StartIndex, int EndIndex);
    }
}
