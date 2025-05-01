using Ezest.SPX.RouteFinder.App.Domain.Core;


namespace Ezest.SPX.RouteFinder.App.Application.Services
{
    public class DijkstraPathfindingStrategy : IPathfindingStrategy
    {
        public int FindShortestPath(IGraph graph, string start, string end)
        {
            if (start == end) return 0; // Special case: Start and end are the same

            var distances = new Dictionary<string, int>();
            var priorityQueue = new SortedSet<Tuple<int, string>>(Comparer<Tuple<int, string>>.Create((a, b) => a.Item1 != b.Item1 ? a.Item1.CompareTo(b.Item1) : a.Item2.CompareTo(b.Item2)));
            var previousNodes = new Dictionary<string, string>();

            // Initialize all distances to infinity (int.MaxValue), except for the start node
            foreach (var town in graph.GetRoutesFrom(start))
            {
                distances[town.To] = int.MaxValue; // Set all towns to max distance initially
            }
            distances[start] = 0; // The start node has a distance of 0

            priorityQueue.Add(Tuple.Create(0, start));

            while (priorityQueue.Any())
            {
                var current = priorityQueue.Min;
                priorityQueue.Remove(current);
                var currentNode = current.Item2;
                var currentDistance = current.Item1;

                if (currentNode == end) break; // If we've reached the destination, no need to continue

                foreach (var route in graph.GetRoutesFrom(currentNode))
                {
                    // Check if the route's destination node is already in the distances dictionary
                    if (!distances.ContainsKey(route.To))
                    {
                        distances[route.To] = int.MaxValue; // Initialize if not present
                    }

                    // Calculate the new distance to the neighboring node
                    var newDistance = currentDistance + route.Distance;

                    // If the new distance is shorter, update it
                    if (newDistance < distances[route.To])
                    {
                        distances[route.To] = newDistance;
                        previousNodes[route.To] = currentNode;
                        priorityQueue.Add(Tuple.Create(newDistance, route.To));
                    }
                }
            }

            // If no path is found, return int.MaxValue
            return distances.ContainsKey(end) && distances[end] != int.MaxValue ? distances[end] : int.MaxValue;
        }
    }



}
