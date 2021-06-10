using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonCrossingLinesDrawer
{
    class BFS
    {
        public const int INF = int.MinValue;
        public const int NONODESBEFORE = -1;
        public const int NOVISITED = -2;
        public const int VISITED = 1;

        private int nodeNumber = 0;
        public int[,] list;
        private int StartPoint;
        public int[,] PathHelp { get; set; }    //o row for nodes, 1 row for distance, 2 row for before nodes,3 row vistted or not visited
        public ArrayList[] Predecessors;
        private Queue<int> BfsQueue;

        public BFS(int startPoint, int[,] _list)
        {
            list = _list;
            StartPoint = startPoint;
            nodeNumber = list.GetLength(0);
            PathHelp = new int[4, nodeNumber];
            BfsQueue = new Queue<int>();
            Predecessors = new ArrayList[nodeNumber];
            for (int i = 0; i < Predecessors.Length; i++)
            {
                Predecessors[i] = new ArrayList();
            }
        }

        public void getBFSPath()
        {
            int currentNode = -1;
            BfsQueue.Clear();
            setEmptyHelpTable(PathHelp);
            PathHelp[3, (StartPoint)] = VISITED;  //Start point was visited
            PathHelp[1, (StartPoint)] = 0;        //Distance from start point to start point equals 0
            BfsQueue.Enqueue((StartPoint));       //Add start point to queue
            while (BfsQueue.Count() > 0)       //Until queu is not empty
            {
                currentNode = BfsQueue.Dequeue();   //take first node from queu
                if (currentNode > -1)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        if (list[currentNode, i] == 1)
                        {
                            var nodeNumber = i;
                            //Node n = list[currentNode][i] as Node;
                            if ((PathHelp[3, nodeNumber] == NOVISITED))
                            {
                                BfsQueue.Enqueue(nodeNumber);
                                PathHelp[3, nodeNumber] = VISITED;
                                PathHelp[1, nodeNumber] = PathHelp[1, currentNode];
                                PathHelp[2, nodeNumber] = currentNode;
                                Predecessors[nodeNumber].Add(currentNode);
                            }
                            else if (PathHelp[1, nodeNumber] == (PathHelp[1, currentNode]))  //Jezeli mozna dojsc z tym samym kosztem to dodaj nastepnego poprzednika
                            {
                                if (!Predecessors[currentNode].Contains(nodeNumber))
                                {
                                    Predecessors[currentNode].Add(nodeNumber);
                                }
                            }
                        }

                    }
                }
            }
        }

        public List<int> getBFSPathToPointR(int dst, List<int> currentPath)
        {
            currentPath.Add(dst);
            if (StartPoint == dst)
            {
                return currentPath;
            }
            else if (Predecessors[dst].Count > 0)
            {
                for (int i = 0; i < Predecessors[dst].Count; i++)
                {
                    List<int> nowa = new List<int>();
                    nowa.AddRange(currentPath);
                    if (currentPath.Contains((int)Predecessors[dst][i]))
                    {
                        continue;
                    }
                    return getBFSPathToPointR((int)Predecessors[dst][i], nowa);
                }
            }
            throw new ArgumentException("There is no path between points that dont cross existing lines");
        }

        private void setEmptyHelpTable(int[,] helpTable)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < nodeNumber; j++)
                {
                    if (i == 0)
                    {
                        helpTable[i, j] = j;
                    }
                    else if (i == 1)
                    {
                        helpTable[i, j] = INF;
                    }
                    else if (i == 2)
                    {
                        helpTable[i, j] = NONODESBEFORE;
                    }
                    else if (i == 3)
                    {
                        helpTable[i, j] = NOVISITED;
                    }
                }
            }
        }
    }
}
