using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NonCrossingLinesDrawer.Extensions
{
    public static class CanvasExtensions
    {
        public static int GetRowNumber(this Canvas canvas)
        {
            return (int)canvas.GetValue(Grid.RowProperty);
        }

        public static int GetColumnNumber(this Canvas canvas)
        {
            return (int)canvas.GetValue(Grid.ColumnProperty);
        }
    }
}
