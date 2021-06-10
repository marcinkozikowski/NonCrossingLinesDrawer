using System.Collections.Generic;

namespace NonCrossingLinesDrawer
{
    internal class AdjacencyMatrix
    {
        private readonly int _matrixSize;
        private int _pointsCount;
        private int[,] _matrix;

        public int[,] Matrix => _matrix;
        public int MatrixSize => _matrixSize;

        public AdjacencyMatrix(int size)
        {
            _matrixSize = size;
            _pointsCount = size * size;
            _matrix = new int[_pointsCount, _pointsCount];
            PrepareAdjacencyMatrix();
        }

        private void PrepareAdjacencyMatrix()
        {
            for (int i = 0; i < _pointsCount; i++)
            {
                SetNeighbours(i);
            }
        }

        public void RemoveAdjacency(IEnumerable<int> points)
        {
            foreach (var point in points)
            {
                for (int i = 0; i < _pointsCount; i++)
                {
                    _matrix[point, i] = 0;
                    _matrix[i, point] = 0;
                }
            }
        }

        private void SetNeighbours(int point)
        {
            //set left neighbour
            if (point - 1 > 0 && (point % _matrixSize != 0))
            {
                _matrix[point, point - 1] = 1;
                _matrix[point - 1, point] = 1;
            }
            //set right neighbour
            if (point % _matrixSize != _matrixSize - 1)
            {
                _matrix[point, point + 1] = 1;
                _matrix[point + 1, point] = 1;
            }
            //set top
            if (point >= _matrixSize)
            {
                _matrix[point, point - _matrixSize] = 1;
                _matrix[point - _matrixSize, point] = 1;
            }
            //set bottom
            if (point < (_pointsCount - _matrixSize))
            {
                _matrix[point, point + _matrixSize] = 1;
                _matrix[point + _matrixSize, point] = 1;
            }
        }
    }

}
