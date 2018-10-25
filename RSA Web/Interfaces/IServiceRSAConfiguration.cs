using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;

namespace RSA_Web.Interfaces
{
    public interface IServiceRSAConfiguration
    {
        Configuration DefaultConfiguration { get; set; }
        Configuration CurrentConfiguration { get; set; }
    }
}
