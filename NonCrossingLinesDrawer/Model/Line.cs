using System.Collections.Generic;
using System.IO;
using NonCrossingLinesDrawer.Abstractions;
using NonCrossingLinesDrawer.Interfaces;

namespace NonCrossingLinesDrawer
{
    public class Line : PointPath,ILine
    {
        public Line(List<Point> path)
        {
            Path = path;
        }
        
        public Line(PointPath path)
        {
            Path = path.Path;
        }
    }
}