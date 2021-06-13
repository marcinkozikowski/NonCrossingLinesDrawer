using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonCrossingLinesDrawer.Extensions
{
    public static class OtherExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        public static int ColumnNumber(this int pointNumber, int matrixSize)
        {
            return pointNumber % matrixSize;
        }

        public static int RowNumber(this int pointNumber, int matrixSize)
        {
            return pointNumber / matrixSize;
        }
    }
}
