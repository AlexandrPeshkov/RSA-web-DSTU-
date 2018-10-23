using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public class ArgumentModel
    {
        public int SizeArgumentVector;
        public List<int> VectorX;

        public ArgumentModel()
        {
            VectorX = new List<int>();
        }
    }
}
