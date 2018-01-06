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
        public bool isBipartite_util<T>(T Graph, int src, int nodes_size, int[] colorArr, int GRAPH_TYPE_FLAG, int[] componentlist)
        {
            List<List<bool>> G = new List<List<bool>>(); ;
            List<List<int>> graph= new List<List<int>>();
            // Create a color array to store colors assigned to all veritces. Vertex 
            // number is used as index in this array. The value '-1' of  colorArr[i] 
            // is used to indicate that no color is assigned to vertex 'i'.  The value 
            // 1 is used to indicate first color is assigned and value 0 indicates 
            // second color is assigned.
            if (GRAPH_TYPE_FLAG == GraphGlobalVariables.MATRIX_FLAG)
            {
               
                G = (List<List<bool>>)Convert.ChangeType(Graph, typeof(List<List<bool>>));
            }
            if (GRAPH_TYPE_FLAG == GraphGlobalVariables.LIST_FLAG)
            {
              
                graph = (List<List<int>>)Convert.ChangeType(Graph, typeof(List<List<int>>));
            }
                // Assign first color to source
            if (colorArr[src] == -1)
            {

                colorArr[src] = 1;
                componentlist[src] = componentlistIndex;
            }
            // Create a queue (FIFO) of vertex numbers and enqueue source vertex
            // for BFS traversal
            Queue<int> q = new Queue<int>();
            if (GRAPH_TYPE_FLAG == GraphGlobalVariables.MATRIX_FLAG)
                q.Enqueue(src);
            else if (GRAPH_TYPE_FLAG == GraphGlobalVariables.LIST_FLAG)
            {
                q.Enqueue(src + 1);
            }
            // Run while there are vertices in queue (Similar to BFS)
            while (q.Count != 0)
            {
                // Dequeue a vertex from queue  
                int u = q.Peek();
                q.Dequeue();

                // Return false if there is a self-loop 
                if (GRAPH_TYPE_FLAG == GraphGlobalVariables.MATRIX_FLAG)
                {
                  
                    if (G[u][u] == true)
                        return false;

                    // Find all non-colored adjacent vertices
                    for (int v = 0; v < G[u].Count; ++v)
                    {
                        // An edge from u to v exists and destination v is not colored
                        if (G[u][v] && (colorArr[v] == -1))
                        {
                            // Assign alternate color to this adjacent v of u
                            colorArr[v] = 1 - colorArr[u];
                            componentlist[v] = componentlistIndex;
                            q.Enqueue(v);
                        }

                        //  An edge from u to v exists and destination v is colored with
                        // same color as u
                        else if (G[u][v] && colorArr[v] == colorArr[u])
                            return false;
                    }
                }
                else if (GRAPH_TYPE_FLAG == GraphGlobalVariables.LIST_FLAG)
                {
                   
                   // int[] level = new int[graph.Count];
                   // int vertex = 0;
                    for (int i =1; i < graph[u-1].Count; i++)
                    { 
                            if ( (colorArr[graph[u - 1][i]-1] == -1))
                            {
                                // Assign alternate color to this adjacent v of u
                                colorArr[graph[u - 1][i]-1] = 1 - colorArr[u-1];
                                componentlist[graph[u - 1][i]-1] = componentlistIndex;
                                q.Enqueue(graph[u - 1][i]);

                            }
                            else if ( colorArr[graph[u - 1][i]-1] == colorArr[u-1] )
                                return false;
                    }
                }
              
            }
            // If we reach here, then all adjacent vertices can be colored with 
            // alternate color
            return true;
        }
        public bool isBipartite<T>(T G, int nodes_size, int[] colorArr, int GRAPH_TYPE_FLAG, int[] componentlist)//remove scr
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
                    if (!isBipartite_util(G, i, nodes_size, colorArr, GRAPH_TYPE_FLAG, componentlist))
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
