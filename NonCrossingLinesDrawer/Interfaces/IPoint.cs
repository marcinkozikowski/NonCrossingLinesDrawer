namespace NonCrossingLinesDrawer.Interfaces
{
    public interface IPoint
    {
        /// <summary>
        /// X cooridante stands for columns in matrix
        /// </summary>
        int X { get; }
        
        /// <summary>
        /// Y coordinate stands for row in matrix
        /// </summary>
        int Y { get; }
    }
}