using Ezest.SPX.RouteFinder.App.Domain.Core;
using System;

public class RouteService
{
    private readonly IGraph _graph;

    public RouteService(IGraph graph)
    {
        _graph = graph;
    }

    public void RunTests()
    {
        Test1();
        Test2();
        Test3();
        Test4();
        Test5();
        Test6();
        Test7();
        Test8();
        Test9();
        Test10();
    }

    // Test 1: Route A => B => C distance = 9
    private void Test1()
    {
        int distance = _graph.GetShortestPath("A", "B") + _graph.GetShortestPath("B", "C");
        Console.WriteLine($"Test 1: Distance A => B => C = {distance}"); // Expected 9
    }

    // Test 2: Route A => D distance = 5
    private void Test2()
    {
        int distance = _graph.GetShortestPath("A", "D");
        Console.WriteLine($"Test 2: Distance A => D = {distance}"); // Expected 5
    }

    // Test 3: Route A => D => C distance = 13
    private void Test3()
    {
        int distance = _graph.GetShortestPath("A", "D") + _graph.GetShortestPath("D", "C");
        Console.WriteLine($"Test 3: Distance A => D => C = {distance}"); // Expected 13
    }

    // Test 4: Route A => E => B => C => D distance = 22
    private void Test4()
    {
        int distance = _graph.GetShortestPath("A", "E") + _graph.GetShortestPath("E", "B") + _graph.GetShortestPath("B", "C") + _graph.GetShortestPath("C", "D");
        Console.WriteLine($"Test 4: Distance A => E => B => C => D = {distance}"); // Expected 22
    }

    // Test 5: Route A => E => D doesn't exist
    private void Test5()
    {
        // Since no direct route from A to E to D, this should return something like -1 or an appropriate message
        int distance = _graph.GetShortestPath("A", "E") + _graph.GetShortestPath("E", "D");
        Console.WriteLine($"Test 5: Route A => E => D doesn't exist, Distance = {(distance == int.MaxValue ? "Doesn't exist" : distance.ToString())}");
    }

    // Test 6: Number of trips from C to C with max 3 stops
    private void Test6()
    {
        int trips = CountTrips("C", "C", 3);
        Console.WriteLine($"Test 6: Number of trips from C to C with max 3 stops = {trips}");
    }

    // Test 7: Number of trips from A to C with exactly 4 stops
    private void Test7()
    {
        int trips = CountTrips("A", "C", 4, exactStops: true);
        Console.WriteLine($"Test 7: Number of trips from A to C with exactly 4 stops = {trips}");
    }

    // Test 8: Shortest route from A to C
    private void Test8()
    {
        int distance = _graph.GetShortestPath("A", "C");
        if (distance == int.MaxValue)
        {
            Console.WriteLine("Test 8: No route from A to C");
        }
        else
        {
            Console.WriteLine($"Test 8: Shortest route from A to C = {distance}"); // Expected 9
        }
    }

    // Test 9: Shortest route from B to B
    private void Test9()
    {
        int distance = _graph.GetShortestPath("B", "B");
        if (distance == int.MaxValue)
        {
            Console.WriteLine("Test 9: No route from B to B");
        }
        else
        {
            Console.WriteLine($"Test 9: Shortest route from B to B = {distance}"); // Expected 9
        }
    }

    // Test 10: Number of trips from C to C with distance less than 30
    private void Test10()
    {
        int trips = CountTripsWithDistance("C", "C", 30);
        Console.WriteLine($"Test 10: Number of trips from C to C with distance less than 30 = {trips}");
    }


    // Helper Method: Count the number of trips (paths) with a maximum of 'maxStops' from a start to end
    private int CountTrips(string start, string end, int maxStops, bool exactStops = false)
    {
        int tripCount = 0;

        // Helper method for DFS
        void DFS(string current, int stops, int distance)
        {
            if (stops > maxStops || distance >= 30) return; // Prevent excessive stops and distances

            if (current == end && (exactStops ? stops == maxStops : stops <= maxStops))
            {
                tripCount++;
                return;
            }

            foreach (var route in _graph.GetRoutesFrom(current))
            {
                DFS(route.To, stops + 1, distance + route.Distance);
            }
        }

        DFS(start, 0, 0);
        return tripCount;
    }

    // Helper Method: Count the number of trips with a distance constraint
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
}
