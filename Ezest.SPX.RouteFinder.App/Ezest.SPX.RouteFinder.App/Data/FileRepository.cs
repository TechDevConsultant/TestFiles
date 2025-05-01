using Ezest.SPX.RouteFinder.App.Domain.Core;
public class FileRepository : IFileRepository
{
    public void LoadRoutesFromFile(string filePath, IGraph graph)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',');
            var fromTown = parts[0].Trim();
            var toTown = parts[1].Trim();
            var distance = int.Parse(parts[2].Trim());

            graph.AddRoute(fromTown, toTown, distance);
        }
    }
}
