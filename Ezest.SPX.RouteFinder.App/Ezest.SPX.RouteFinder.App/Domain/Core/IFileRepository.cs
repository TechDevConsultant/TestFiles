using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezest.SPX.RouteFinder.App.Domain.Core
{
    public interface IFileRepository
    {
        void LoadRoutesFromFile(string filePath, IGraph graph);
    }
}
