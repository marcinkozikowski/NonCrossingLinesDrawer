using System.Collections.Generic;
using NonCrossingLinesDrawer.Extensions;
using NonCrossingLinesDrawer.Interfaces;

namespace NonCrossingLinesDrawer
{
    internal class AdjacencyList : IAdjacencyList
    {
        private readonly Dictionary<int, ISet<Point>> _list;
        private readonly int _matrixSize;
        private readonly int _pointsCount;

        public Dictionary<int, ISet<Point>> List => _list;

        public AdjacencyList(int size)
        {
            _matrixSize = size;
            _pointsCount = size * size;
            _list = new Dictionary<int, ISet<Point>>();
            PrepareAdjacencyList();
        }

        private void PrepareAdjacencyList()
        {
            for (int i = 0; i < _pointsCount; i++)
            {
                _list.Add(i, new HashSet<Point>());
                SetListNeighbours(i);
            }
        }

        public void RemoveAdjacency(IEnumerable<Point> pointNumbers)
        {
            foreach (var point in pointNumbers)
            {
                _list[point.Number] = new HashSet<Point>();
            }
        }

        private void SetListNeighbours(int pointNumber)
        {
            //set left neighbour
            if (pointNumber - 1 > 0 && (pointNumber % _matrixSize != 0))
            {
                _list[pointNumber].Add(new Point((pointNumber - 1).ColumnNumber(_matrixSize), (pointNumber - 1).RowNumber(_matrixSize), pointNumber - 1));
            }
            //set right neighbour
            if (pointNumber % _matrixSize != _matrixSize - 1)
            {
                _list[pointNumber].Add(new Point((pointNumber + 1).ColumnNumber(_matrixSize), (pointNumber + 1).RowNumber(_matrixSize), pointNumber + 1));
            }
            //set top
            if (pointNumber >= _matrixSize)
            {
                _list[pointNumber].Add(new Point((pointNumber - _matrixSize).ColumnNumber(_matrixSize), (pointNumber - _matrixSize).RowNumber(_matrixSize), pointNumber - _matrixSize));
            }
            //set bottom
            if (pointNumber < (_pointsCount - _matrixSize))
            {
                _list[pointNumber].Add(new Point((pointNumber + _matrixSize).ColumnNumber(_matrixSize), (pointNumber + _matrixSize).RowNumber(_matrixSize), pointNumber + _matrixSize));
            }
        }
    }

}
