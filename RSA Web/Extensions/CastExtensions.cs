using RSA_Web.Models;
using RSA_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Extensions
{
    public static class CastExtensions
    {
        public static List<DirectionView> ToView(this List<Direction> list)
        {
            List<DirectionView> Views = new List<DirectionView>();
            foreach (var elem in list)
            {
                Views.Add((DirectionView)elem);
            }
            return Views;
        }

        public static List<StepView> ToView(this List<Step> list)
        {
            List<StepView> Views = new List<StepView>();
            foreach (var elem in list)
            {
                Views.Add((StepView)elem);
            }
            return Views;
        }
    }
}


