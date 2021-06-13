namespace NonCrossingLinesDrawer
{
    public static class ErrorMessages
    {
        public static string CantDrawLine(int firstPoint, int secondPoint)
        {
            return $"You can not draw a line between {firstPoint} and {secondPoint} points in such a way that it does not intersect the currently drawn lines";
        }
    }
}