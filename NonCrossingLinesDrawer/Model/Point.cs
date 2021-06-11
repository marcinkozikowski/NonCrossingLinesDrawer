namespace NonCrossingLinesDrawer
{
    public class Point
    {
        /// <summary>
        /// X cooridante stands for columns in matrix
        /// </summary>
        public int X { get;}
        
        /// <summary>
        /// Y coordinate stands for row in matrix
        /// </summary>
        public int Y { get;}
        
        /// <summary>
        /// Point number in matrix, if we have matrix for example [5,5] and we have point x=1 y=1 point number will be 6
        /// </summary>
        public int Number { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public Point(int x, int y,int number)
        {
            X = x;
            Y = y;
            Number = number;
        }

        public void SetNumber(int number)
        {
            Number = number;
        }
    }
}