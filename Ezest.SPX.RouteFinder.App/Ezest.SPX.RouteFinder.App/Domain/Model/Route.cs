using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezest.SPX.RouteFinder.App.Domain.Model
{
    public class Route
    {
        public string To { get; set; }
        public int Distance { get; set; }
        public Route(string to, int distance)
        {
            To = to;
            Distance = distance;
        }
    }
}
