using System.Collections.Generic;

namespace NonCrossingLinesDrawer
{
    internal class AdjacencyMatrix
    {
        private readonly int _matrixSize;
        private readonly int _pointsCount;
        private readonly int[,] _matrix;

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

        public void RemoveAdjacency(IEnumerable<int> pointNumbers)
        {
            foreach (var point in pointNumbers)
            {
                for (int i = 0; i < _pointsCount; i++)
                {
                    _matrix[point, i] = 0;
                    _matrix[i, point] = 0;
                }
            }
        }

        private void SetNeighbours(int pointNumber)
        {
            //set left neighbour
            if (pointNumber - 1 > 0 && (pointNumber % _matrixSize != 0))
            {
                _matrix[pointNumber, pointNumber - 1] = 1;
                _matrix[pointNumber - 1, pointNumber] = 1;
            }
            //set right neighbour
            if (pointNumber % _matrixSize != _matrixSize - 1)
            {
                _matrix[pointNumber, pointNumber + 1] = 1;
                _matrix[pointNumber + 1, pointNumber] = 1;
            }
            //set top
            if (pointNumber >= _matrixSize)
            {
                _matrix[pointNumber, pointNumber - _matrixSize] = 1;
                _matrix[pointNumber - _matrixSize, pointNumber] = 1;
            }
            //set bottom
            if (pointNumber < (_pointsCount - _matrixSize))
            {
                _matrix[pointNumber, pointNumber + _matrixSize] = 1;
                _matrix[pointNumber + _matrixSize, pointNumber] = 1;
            }
        }
    }

}
