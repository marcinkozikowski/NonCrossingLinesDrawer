using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NonCrossingLinesDrawer.Exceptions;
using NonCrossingLinesDrawer.Extensions;
using NonCrossingLinesDrawer.Interfaces;
using static NonCrossingLinesDrawer.PointRelation;

namespace NonCrossingLinesDrawer
{
    public class BreadthPathFinder : IBreadthPathFinder
    {
        private readonly int _pointstCount = 0;
        private readonly Dictionary<int, ISet<Point>> _adjacencyList;
        private int _startPointNumber;
        private int[,] _helpTable { get; set; }    //o  row for before nodes,1 row vistted or not visited
        private readonly ArrayList[] _predecessors;
        private readonly Queue<int> _bfsQueue;
        private readonly int _gridSize;

        public BreadthPathFinder(Dictionary<int, ISet<Point>> adjacencyList)
        {
            _adjacencyList = adjacencyList;
            _pointstCount = _adjacencyList.Count;
            _helpTable = new int[2, _pointstCount];
            _bfsQueue = new Queue<int>();

            //only when matrix is squere as in this example for simplicity
            _gridSize = (int)Math.Sqrt(_adjacencyList.Count);

            //Predecessor array of arrays, every point can have more than one neighbour
            _predecessors = new ArrayList[_pointstCount];
            for (int i = 0; i < _predecessors.Length; i++)
            {
                _predecessors[i] = new ArrayList();
            }
        }

        private void RunBfs()
        {
            _bfsQueue.Clear();
            PrepareHelpTable();
            _helpTable[1, (_startPointNumber)] = VISITED;  //Start point was visited
            _bfsQueue.Enqueue((_startPointNumber));       //Add start point to queue

            while (_bfsQueue.Count() > 0)       //Until queu is not empty
            {
                var currentPoint = _bfsQueue.Dequeue();
                if (currentPoint > -1)
                {
                    foreach (var point in _adjacencyList[currentPoint])
                    {
                        if ((_helpTable[1, point.Number] == NOTVISITED))
                        {
                            _bfsQueue.Enqueue(point.Number);
                            SetHelpers(currentPoint, point.Number);
                            _predecessors[point.Number].Add(currentPoint);
                        }
                        else if (_helpTable[1, point.Number] == (_helpTable[1, currentPoint]))
                        {
                            if (!_predecessors[currentPoint].Contains(point.Number))
                            {
                                _predecessors[currentPoint].Add(point.Number);
                            }
                        }
                    }
                }
            }
        }

        public Line GetLineBeetwen(Point sourcePointNum, Point destentationPointNum)
        {
            _startPointNumber = sourcePointNum.Number;
            RunBfs();
            var path = RecreatePathToPoint(destentationPointNum.Number, new List<int>());

            return new Line(path.Select(x => new Point(x.ColumnNumber(_gridSize), x.RowNumber(_gridSize), x)).ToList());
        }

        private List<int> RecreatePathToPoint(int destenationPoint, List<int> currentPath)
        {
            currentPath.Add(destenationPoint);
            if (_startPointNumber == destenationPoint)
            {
                return currentPath;
            }
            else if (_predecessors[destenationPoint].Count > 0)
            {
                for (int i = 0; i < _predecessors[destenationPoint].Count; i++)
                {
                    var newPath = new List<int>();
                    newPath.AddRange(currentPath);
                    if (currentPath.Contains((int)_predecessors[destenationPoint][i]))
                    {
                        continue;
                    }
                    return RecreatePathToPoint((int)_predecessors[destenationPoint][i], newPath);
                }
            }
            throw new NoPathException(_startPointNumber, destenationPoint);
        }

        private void SetHelpers(int currentPointNumber, int neighbourPointNumber)
        {
            _helpTable[1, neighbourPointNumber] = VISITED;
            _helpTable[0, neighbourPointNumber] = currentPointNumber;
        }

        private void PrepareHelpTable()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _pointstCount; j++)
                {
                    if (i == 0)
                    {
                        _helpTable[i, j] = NONODESBEFORE;
                    }
                    else if (i == 1)
                    {
                        _helpTable[i, j] = NOTVISITED;
                    }
                };
            }
        }
    }
}
