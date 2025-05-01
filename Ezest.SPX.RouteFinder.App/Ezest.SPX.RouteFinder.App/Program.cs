using Ezest.SPX.RouteFinder.App.Domain;
using Ezest.SPX.RouteFinder.App.Domain.Core;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Setup graph and repository
        IGraph graph = new Graph();
        IFileRepository fileRepository = new FileRepository();
        fileRepository.LoadRoutesFromFile("Input.txt", graph);

        // Setup and run tests
        RouteService routeService = new RouteService(graph);
        routeService.RunTests();
    }
}
