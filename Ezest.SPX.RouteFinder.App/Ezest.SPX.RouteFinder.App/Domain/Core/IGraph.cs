using Ezest.SPX.RouteFinder.App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezest.SPX.RouteFinder.App.Domain.Core
{
    public interface IGraph
    {
        void AddRoute(string from, string to, int distance);
        List<Route> GetRoutesFrom(string town);
        int GetShortestPath(string start, string end);
    }
}
