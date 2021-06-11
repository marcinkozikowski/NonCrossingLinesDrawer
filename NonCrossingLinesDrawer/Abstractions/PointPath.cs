using System.Collections.Generic;
using NonCrossingLinesDrawer.Interfaces;

namespace NonCrossingLinesDrawer.Abstractions
{
    public abstract class PointPath : IPath
    {
        public List<Point> Path { get; set; }

        protected PointPath()
        {
            Path = new List<Point>();
        }

        protected PointPath(List<Point> input)
        {
            Path = new List<Point>(input);
        }

        public int Count => Path.Count;
        
        public void AddPoint(Point input)
        {
            Path.Add(input);
        }
    }
}