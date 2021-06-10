using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NonCrossingLinesDrawer.Constants;

namespace NonCrossingLinesDrawer
{
    public class BreadthPathFinder
    {
        private readonly int nodeNumber = 0;
        public readonly int[,] list;
        private readonly int _startPoint;
        private int[,] _helpTable { get; set; }    //o row for nodes, 1 row for distance, 2 row for before nodes,3 row vistted or not visited
        private readonly ArrayList[] _predecessors;
        private readonly Queue<int> _bfsQueue;

        public BreadthPathFinder(int startPoint, int[,] _list)
        {
            list = _list;
            _startPoint = startPoint;
            nodeNumber = list.GetLength(0);
            _helpTable = new int[4, nodeNumber];
            _bfsQueue = new Queue<int>();
            _predecessors = new ArrayList[nodeNumber];
            for (int i = 0; i < _predecessors.Length; i++)
            {
                _predecessors[i] = new ArrayList();
            }
        }

        public void getBFSPath()
        {
            int currentNode = -1;
            _bfsQueue.Clear();
            SetHelpTable();
            _helpTable[3, (_startPoint)] = VISITED;  //Start point was visited
            _helpTable[1, (_startPoint)] = 0;        //Distance from start point to start point equals 0
            _bfsQueue.Enqueue((_startPoint));       //Add start point to queue
            while (_bfsQueue.Count() > 0)       //Until queu is not empty
            {
                currentNode = _bfsQueue.Dequeue();   //take first node from queu
                if (currentNode > -1)
                {
                    for (int i = 0; i < nodeNumber; i++)
                    {
                        if (list[currentNode, i] == 1)
                        {
                            var nodeNumber = i;
                            if ((_helpTable[3, nodeNumber] == NOTVISITED))
                            {
                                _bfsQueue.Enqueue(nodeNumber);
                                _helpTable[3, nodeNumber] = VISITED;
                                _helpTable[1, nodeNumber] = _helpTable[1, currentNode];
                                _helpTable[2, nodeNumber] = currentNode;
                                _predecessors[nodeNumber].Add(currentNode);
                            }
                            else if (_helpTable[1, nodeNumber] == (_helpTable[1, currentNode]))
                            {
                                if (!_predecessors[currentNode].Contains(nodeNumber))
                                {
                                    _predecessors[currentNode].Add(nodeNumber);
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
            if (_startPoint == destenationPoint)
            {
                return currentPath;
            }
            else if (_predecessors[destenationPoint].Count > 0)
            {
                for (int i = 0; i < _predecessors[destenationPoint].Count; i++)
                {
                    List<int> nowa = new List<int>();
                    nowa.AddRange(currentPath);
                    if (currentPath.Contains((int)_predecessors[destenationPoint][i]))
                    {
                        continue;
                    }
                    return RecreatePathToPoint((int)_predecessors[destenationPoint][i], nowa);
                }
            }
            throw new ArgumentException("There is no path between points that dont cross existing lines");
        }

        private void SetHelpTable()
        {
            for (int i = 0; i < 4; i++)
            {
                Parallel.For(0, nodeNumber, (j, state) =>
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
