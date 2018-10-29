using RSA_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class Direction
    {
        public double Value { get; set; }
        public int Index { get; set; }

        public static explicit operator double(Direction Direction)
        {
            return Direction.Value;
        }

        public static double operator *(Direction left, double right)
        {
            return left.Value * right;
        }

        public static implicit operator DirectionView(Direction direction)
        {
            return new DirectionView()
            {
                Value = direction.Value,
                Index = direction.Index
            };
        }

    }
}
