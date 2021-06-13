using System.Collections.Generic;

namespace NonCrossingLinesDrawer.Interfaces
{
    public interface IAdjacencyList
    {
        Dictionary<int, ISet<Point>> List { get; }
    }
}