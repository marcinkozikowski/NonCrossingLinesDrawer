using System;

namespace NonCrossingLinesDrawer.Exceptions
{
    public class NoPathException : Exception
    {
        public NoPathException(int startPoint, int destenationPoint) : base(ErrorMessages.CantDrawLine(startPoint,destenationPoint))
        {
        }

        public NoPathException(string message)
            : base(message)
        {
        }
    }
}
