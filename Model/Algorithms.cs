using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class Algorithms
    {
        private int componentlistIndex = 0;
        private bool bipartiteflag;
        private bool isConnectedflag;
        public bool isBipartite_util<T>(T Graph, int src, int nodes_size, int[] colorArr, GraphTypes graphType, int[] componentlist)
        {
            // Create a queue (FIFO) of vertex numbers and enqueue source vertex
            // for BFS traversal
            Queue<int> q = new Queue<int>();
            // Create a color array to store colors assigned to all veritces. Vertex 
            // number is used as index in this array. The value '-1' of  colorArr[i] 
            // is used to indicate that no color is assigned to vertex 'i'.  The value 
            // 1 is used to indicate first color is assigned and value 0 indicates 
            // second color is assigned.
            switch (graphType)
            {
                case (GraphTypes.Dense):
                    {
                        List<List<bool>> dense_graph = (List<List<bool>>)Convert.ChangeType(Graph, typeof(List<List<bool>>));
                        // Assign first color to source
                        if (colorArr[src] == -1)
                        {
                            colorArr[src] = 1;
                            componentlist[src] = componentlistIndex;
                        }
                        q.Enqueue(src);
                        // Run while there are vertices in queue (Similar to BFS)
                        while (q.Count != 0)
                        {
                            // Dequeue a vertex from queue  
                            int u = q.Peek();
                            q.Dequeue();
                            // Return false if there is a self-loop 
                            if (dense_graph[u][u] == true)
                                return false;
                            // Find all non-colored adjacent vertices
                            for (int v = 0; v < dense_graph[u].Count; ++v)
                            {
                                // An edge from u to v exists and destination v is not colored
                                if (dense_graph[u][v] && (colorArr[v] == -1))
                                {
                                    // Assign alternate color to this adjacent v of u
                                    colorArr[v] = 1 - colorArr[u];
                                    componentlist[v] = componentlistIndex;
                                    q.Enqueue(v);
                                }
                                //  An edge from u to v exists and destination v is colored with
                                // same color as u
                                else if (dense_graph[u][v] && colorArr[v] == colorArr[u])
                                    return false;
                            }
                        }
                        return true;
                    }
                case (GraphTypes.Sparse):
                    {
                        List<List<int>> sparse_graph = (List<List<int>>)Convert.ChangeType(Graph, typeof(List<List<int>>));
                        // Assign first color to source
                        if (colorArr[src] == -1)
                        {
                            colorArr[src] = 1;
                            componentlist[src] = componentlistIndex;
                        }
                        q.Enqueue(src + 1);
                        // Run while there are vertices in queue (Similar to BFS)
                        while (q.Count != 0)
                        {
                            // Dequeue a vertex from queue  
                            int u = q.Peek();
                            q.Dequeue();
                            // int[] level = new int[graph.Count];
                            // int vertex = 0;
                            for (int i = 1; i < sparse_graph[u - 1].Count; i++)
                            {
                                if ((colorArr[sparse_graph[u - 1][i] - 1] == -1))
                                {
                                    // Assign alternate color to this adjacent v of u
                                    colorArr[sparse_graph[u - 1][i] - 1] = 1 - colorArr[u - 1];
                                    componentlist[sparse_graph[u - 1][i] - 1] = componentlistIndex;
                                    q.Enqueue(sparse_graph[u - 1][i]);

                                }
                                else if (colorArr[sparse_graph[u - 1][i] - 1] == colorArr[u - 1])
                                    return false;
                            }
                        }
                        return true;
                    }
                default:
                    return false;
            }
        }

        public bool isBipartite<T>(T G, int nodes_size, int[] colorArr, GraphTypes graphType, int[] componentlist)//remove scr
        {
            bool[] discovered = new bool[nodes_size + 1];
            for (int i = 0; i < nodes_size; ++i)
                colorArr[i] = -1;
            isConnectedflag = true;
            bipartiteflag = true;
            for (int i = 0; i < nodes_size; i++)
            {
                if (colorArr[i] == -1)
                {
                    componentlistIndex++;
                    if (!isBipartite_util(G, i, nodes_size, colorArr, graphType, componentlist))
                    {
                        bipartiteflag = false;
                        if (i == 0)
                        {
                            for (int j = 0; j < nodes_size; ++j)
                            {
                                if (colorArr[j] == -1)
                                    isConnectedflag = false;
                            }
                        }

                    }
                }
            }
            if (bipartiteflag == true)
                return true;
            else
                return false;
        }



        /*must run bipartite function first*/
        public bool isConnected()
        {
            return isConnectedflag;
        }

    }
}
