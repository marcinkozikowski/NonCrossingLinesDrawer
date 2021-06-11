using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NonCrossingLinesDrawer.PointRelation;

namespace NonCrossingLinesDrawer
{
    public class BreadthPathFinder
    {
        private readonly int _pointstCount = 0;
        private readonly int[,] _adjacencyList;
        private readonly int _startPointNumber;
        private int[,] _helpTable { get; set; }    //o row for nodes, 1 row for distance, 2 row for before nodes,3 row vistted or not visited
        private readonly ArrayList[] _predecessors;
        private readonly Queue<int> _bfsQueue;

        public BreadthPathFinder(int startPointNumber, int[,] adjacencyList)
        {
            _adjacencyList = adjacencyList;
            _startPointNumber = startPointNumber;
            _pointstCount = _adjacencyList.GetLength(0);
            _helpTable = new int[4, _pointstCount];
            _bfsQueue = new Queue<int>();
            
            //Predecessor array of arrays, every point can have more than one neighbour
            _predecessors = new ArrayList[_pointstCount];
            for (int i = 0; i < _predecessors.Length; i++)
            {
                _predecessors[i] = new ArrayList();
            }
        }

        public void GetBFSPath()
        {
            _bfsQueue.Clear();
            PrepareHelpTable();
            _helpTable[3, (_startPointNumber)] = VISITED;  //Start point was visited
            _helpTable[1, (_startPointNumber)] = 0;        //Distance from start point to start point equals 0
            _bfsQueue.Enqueue((_startPointNumber));       //Add start point to queue
            
            while (_bfsQueue.Count() > 0)       //Until queu is not empty
            {
                var currentPoint = _bfsQueue.Dequeue();
                if (currentPoint > -1)
                {
                    for (int i = 0; i < _pointstCount; i++)
                    {
                        if (_adjacencyList[currentPoint, i] == 1)
                        {
                            var neighbourPointNumber = i;
                            if ((_helpTable[3, neighbourPointNumber] == NOTVISITED))
                            {
                                _bfsQueue.Enqueue(neighbourPointNumber);
                                SetHelpers(currentPoint,neighbourPointNumber);
                                _predecessors[neighbourPointNumber].Add(currentPoint);
                            }
                            else if (_helpTable[1, neighbourPointNumber] == (_helpTable[1, currentPoint]))
                            {
                                if (!_predecessors[currentPoint].Contains(neighbourPointNumber))
                                {
                                    _predecessors[currentPoint].Add(neighbourPointNumber);
                                }
                            }
                        }

                    }
                }
            }
        }

        public List<int> RecreatePathToPoint(int destenationPoint, List<int> currentPath)
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
            throw new ArgumentException("There is no path between points that dont cross existing lines");
        }

        private void SetHelpers(int currentPointNumber, int neighbourPointNumber)
        {
            _helpTable[3, neighbourPointNumber] = VISITED;
            _helpTable[1, neighbourPointNumber] = _helpTable[1, currentPointNumber];
            _helpTable[2, neighbourPointNumber] = currentPointNumber;
        }

        private void PrepareHelpTable()
        {
            for (int i = 0; i < 4; i++)
            {
                Parallel.For(0, _pointstCount, (j, state) =>
                  {
                      if (i == 0)
                      {
                          _helpTable[i, j] = j;
                      }
                      else if (i == 1)
                      {
                          _helpTable[i, j] = INF;
                      }
                      else if (i == 2)
                      {
                          _helpTable[i, j] = NONODESBEFORE;
                      }
                      else if (i == 3)
                      {
                          _helpTable[i, j] = NOTVISITED;
                      }
                  });
            }
        }
    }
}
