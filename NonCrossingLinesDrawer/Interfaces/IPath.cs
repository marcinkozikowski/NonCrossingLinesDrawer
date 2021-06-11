using System.Collections.Generic;

namespace NonCrossingLinesDrawer.Interfaces
{
    public interface IPath
    {
        int Count { get; }

        void AddPoint(Point input);
    }
}