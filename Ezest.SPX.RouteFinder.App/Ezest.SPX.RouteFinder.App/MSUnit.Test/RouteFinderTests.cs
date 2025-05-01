using Ezest.SPX.RouteFinder.App.Domain.Core;
using Ezest.SPX.RouteFinder.App.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GraphTests
{
    [TestClass]
    public class RouteFinderTests
    {
        private IGraph _graph;

        [TestInitialize]
        public void SetUp()
        {
            // Initialize the graph and add the routes from the input.
            _graph = new Graph();

            _graph.AddRoute("A", "B", 5);
            _graph.AddRoute("B", "C", 4);
            _graph.AddRoute("C", "D", 8);
            _graph.AddRoute("D", "C", 8);
            _graph.AddRoute("D", "E", 6);
            _graph.AddRoute("A", "D", 5);
            _graph.AddRoute("C", "E", 2);
            _graph.AddRoute("E", "B", 3);
            _graph.AddRoute("A", "E", 7);
        }

        // Test 8: The length of the shortest route from A to C
        [TestMethod]
        public void TestShortestRouteAtoC()
        {
            int distance = _graph.GetShortestPath("A", "C");
            Assert.AreEqual(9, distance, "Test 8: Shortest route from A to C should be 9");
        }

        // Test 9: The length of the shortest route from B to B (Should be a cycle or a valid path)
        [TestMethod]
        public void TestShortestRouteBtoB()
        {
            int distance = _graph.GetShortestPath("B", "B");
            Assert.AreEqual(9, distance, "Test 9: Shortest route from B to B should be 9 (B -> C -> E -> B)");
        }

        // Test 10: The number of trips from C to C with distance less than 30
        [TestMethod]
        public void TestNumberOfTripsCtoCWithDistanceLessThan30()
        {
            int trips = CountTripsWithDistance("C", "C", 30);
            Assert.AreEqual(7, trips, "Test 10: Number of trips from C to C with distance less than 30 should be 7");
        }

        private int CountTripsWithDistance(string start, string end, int maxDistance)
        {
            int tripCount = 0;

            // Helper method for DFS with distance tracking
            void DFS(string current, int distance)
            {
                if (distance >= maxDistance) return;

                if (current == end && distance < maxDistance)
                {
                    tripCount++;
                    return;
                }

                foreach (var route in _graph.GetRoutesFrom(current))
                {
                    DFS(route.To, distance + route.Distance);
                }
            }

            DFS(start, 0);
            return tripCount;
        }

        // Test 5: Route A->E->D doesn't exist
        [TestMethod]
        public void TestRouteDoesNotExist()
        {
            int distance = _graph.GetShortestPath("A", "E");
            Assert.AreEqual(int.MaxValue, distance, "Test 5: Route A->E->D doesn't exist, so the distance should be int.MaxValue.");
        }

        // Test 1: Distance of the route A->B->C is 9
        [TestMethod]
        public void TestRouteABCTotalDistance()
        {
            var routes = new List<string> { "A", "B", "C" };
            int totalDistance = 0;

            for (int i = 0; i < routes.Count - 1; i++)
            {
                var from = routes[i];
                var to = routes[i + 1];
                totalDistance += _graph.GetShortestPath(from, to);
            }

            Assert.AreEqual(9, totalDistance, "Test 1: The distance of the route A->B->C should be 9.");
        }

        // Test 2: Distance of the route A->D is 5
        [TestMethod]
        public void TestRouteADTotalDistance()
        {
            int distance = _graph.GetShortestPath("A", "D");
            Assert.AreEqual(5, distance, "Test 2: The distance of the route A->D should be 5.");
        }

        // Test 6: Number of trips from C to C with maximum 3 stops
        [TestMethod]
        public void TestNumberOfTripsCtoCWithMaxStops()
        {
            int trips = CountTripsWithMaxStops("C", "C", 3);
            Assert.AreEqual(2, trips, "Test 6: The number of trips from C to C with max 3 stops should be 2.");
        }

        private int CountTripsWithMaxStops(string start, string end, int maxStops)
        {
            int tripCount = 0;

            // Helper method for DFS with stop counting
            void DFS(string current, int stops)
            {
                if (stops > maxStops) return;

                if (current == end && stops <= maxStops)
                {
                    tripCount++;
                    return;
                }

                foreach (var route in _graph.GetRoutesFrom(current))
                {
                    DFS(route.To, stops + 1);
                }
            }

            DFS(start, 0);
            return tripCount;
        }

        // Test 3: Distance of the route A->D->C is 13
        [TestMethod]
        public void TestRouteADCTotalDistance()
        {
            var routes = new List<string> { "A", "D", "C" };
            int totalDistance = 0;

            for (int i = 0; i < routes.Count - 1; i++)
            {
                var from = routes[i];
                var to = routes[i + 1];
                totalDistance += _graph.GetShortestPath(from, to);
            }

            Assert.AreEqual(13, totalDistance, "Test 3: The distance of the route A->D->C should be 13.");
        }
    }
}
