using System.Collections.Generic;

namespace NonCrossingLinesDrawer
{
    public class Line
    {
        public List<Point> Path { get;}

        public Line()
        {
            Path = new List<Point>();
        }

        public Line(List<Point> path)
        {
            Path = path;
        }

        public void AddPointToPath(Point input)
        {
            Path.Add(input);
        }
    }
}