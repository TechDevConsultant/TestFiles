using Ezest.SPX.RouteFinder.App.Application.Services;
using Ezest.SPX.RouteFinder.App.Domain.Core;
using Ezest.SPX.RouteFinder.App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezest.SPX.RouteFinder.App.Domain
{
    public class Graph : IGraph
    {
        public Dictionary<string, List<Route>> AdjacencyList { get; private set; } = new Dictionary<string, List<Route>>();

        public void AddRoute(string from, string to, int distance)
        {
            if (!AdjacencyList.ContainsKey(from))
            {
                AdjacencyList[from] = new List<Route>();
            }
            AdjacencyList[from].Add(new Route(to, distance));

            // If it's undirected graph, add the reverse route
            if (!AdjacencyList.ContainsKey(to))
            {
                AdjacencyList[to] = new List<Route>();
            }
            AdjacencyList[to].Add(new Route(from, distance));
        }

        // Return empty list if the town doesn't have routes
        public List<Route> GetRoutesFrom(string town)
        {
            return AdjacencyList.ContainsKey(town) ? AdjacencyList[town] : new List<Route>();
        }

        public int GetShortestPath(string start, string end)
        {
            // Handle no route scenario with int.MaxValue to represent unreachable paths
            var routes = GetRoutesFrom(start);
            if (!AdjacencyList.ContainsKey(start) || !AdjacencyList.ContainsKey(end))
                return int.MaxValue;

            return new DijkstraPathfindingStrategy().FindShortestPath(this, start, end);
        }
    }



}
