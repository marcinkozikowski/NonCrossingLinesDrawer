using NonCrossingLinesDrawer.Interfaces;

namespace NonCrossingLinesDrawer
{
    public class Point : IPoint
    {
        public int X { get;}

        public int Y { get; }

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

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y &&
                   Number == point.Number;
        }

        public override int GetHashCode()
        {
            int hashCode = 1887729291;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            return hashCode;
        }
    }
}