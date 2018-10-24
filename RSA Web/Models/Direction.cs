using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class Direction
    {
        public double direction { get; set; }
        public int index { get; set; }

        public static explicit operator double(Direction Direction)
        {
            return Direction.direction;
        }

        public static double operator* (Direction left, double right)
        {
            return left.direction * right;
        }
    }
}
