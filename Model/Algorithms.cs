using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class Algorithms
    {
        public bool isBipartite_util(List<List<bool>> G, int src, int[] colorArr)
        {
            // Create a color array to store colors assigned to all veritces. Vertex 
            // number is used as index in this array. The value '-1' of  colorArr[i] 
            // is used to indicate that no color is assigned to vertex 'i'.  The value 
            // 1 is used to indicate first color is assigned and value 0 indicates 
            // second color is assigned.

            int V = G.Count;
            // Assign first color to source
            colorArr[src] = 1;

            // Create a queue (FIFO) of vertex numbers and enqueue source vertex
            // for BFS traversal
            Queue<int> q = new Queue<int>();
            q.Enqueue(src);

            // Run while there are vertices in queue (Similar to BFS)
            while (q.Count != 0)
            {
                // Dequeue a vertex from queue  
                int u = q.Peek();
                q.Dequeue();

                // Return false if there is a self-loop 
                if (G[u][u] == true)
                    return false;

                // Find all non-colored adjacent vertices
                for (int v = 0; v < V; ++v)
                {
                    // An edge from u to v exists and destination v is not colored
                    if (G[u][v] && (colorArr[v] == -1))
                    {
                        // Assign alternate color to this adjacent v of u
                        colorArr[v] = 1 - colorArr[u];
                        q.Enqueue(v);
                    }

                    //  An edge from u to v exists and destination v is colored with
                    // same color as u
                    else if (G[u][v] && colorArr[v] == colorArr[u])
                        return false;
                }
            }
            // If we reach here, then all adjacent vertices can be colored with 
            // alternate color
            return true;
        }
        public bool isBipartite(List<List<bool>> G, int src, int[] colorArr)//remove scr
        {
            int V = G.Count;
            for (int i = 0; i < V; ++i)
                colorArr[i] = -1;

            for (int i = 0; i < V; i++)
            {
                if (colorArr[i] == -1)
                {
                    if (!isBipartite_util(G,i, colorArr))
                        return false;
                }
            }
            return true;
        }
        public bool isBipartite_util(List<List<int>> graph, int v, bool[] discovered)
        {
            // stores vertex is discovered or not
           

            // stores level of each vertex in BFS
            int[] level = new int[graph.Count+1];

            // mark source vertex as discovered and 
            // set its level to 0
            discovered[v] = true;
            level[v] = 0;

            // create a queue to do BFS and enqueue 
            // source vertex in it
            Queue<int> q = new Queue<int>();
            q.Enqueue(v);

            // run till queue is not empty
            while (q.Count !=0)
            {
                // pop front node from the queue
                v = q.Peek();
                q.Dequeue();

                // do for every edge (v -> u)
                int u = 0;
                for (int i = 1 ; i< graph[v-1].Count;i++)
                {
                    u = graph[v - 1][i];
                    // if vertex u is explored for first time
                    if (!discovered[u])
                    {
                        // mark it discovered
                        discovered[u] = true;

                        // set level as level of parent node + 1
                        level[u] = level[v] + 1;

                        // push the vertex into the queue
                        q.Enqueue(u);
                    }

                    // if the vertex is already been discovered and
                    // level of vertex u and v are same, then the 
                    // graph contains an odd-cycle & is not biparte
                    else if (level[v] == level[u])
                        return false;
                }
            }

            return true;

        }
        public bool isBipartite(List<List<int>> graph, int src, int[] color)//remove source
        {
            int V = graph.Count;
            bool[] discovered = new bool[graph.Count + 1];

            for (int i = 1; i < V; i++)
            {
                if (discovered[i] == false)
                {
                    if (!isBipartite_util(graph, i, discovered))
                        return false;
                }
            }
            return true;
        }
           public bool isConnected(List<List<int>> graph, int v)
           {
            // stores vertex is discovered or not
            bool[] discovered = new bool[graph.Count + 1];

            // stores level of each vertex in BFS
            int[] level = new int[graph.Count + 1];

            // mark source vertex as discovered and 
            // set its level to 0
            discovered[v] = true;
            level[v] = 0;

            // create a queue to do BFS and enqueue 
            // source vertex in it
            Queue<int> q = new Queue<int>();
            q.Enqueue(v);

            // run till queue is not empty
            while (q.Count != 0)
            {
                // pop front node from the queue
                v = q.Peek();
                q.Dequeue();

                // do for every edge (v -> u)
                int u = 0;
                for (int i = 1; i < graph[v - 1].Count; i++)
                {
                    u = graph[v - 1][i];
                    // if vertex u is explored for first time
                    if (!discovered[u])
                    {
                        // mark it discovered
                        discovered[u] = true;

                        // set level as level of parent node + 1
                        level[u] = level[v] + 1;

                        // push the vertex into the queue
                        q.Enqueue(u);
                    }

                    // if the vertex is already been discovered and
                    // level of vertex u and v are same, then the 
                    // graph contains an odd-cycle & is not biparte
                    else if (level[v] == level[u])
                        return false;
                }
            }

            for (int i = 1; i < discovered.Length; i++)
            {
                if(discovered[i] == false)
                {
                    return false;
                }
            }
            return true;

        }

       /* public bool isConnected(List<List<bool>> graph, int v)
        {
            // stores vertex is discovered or not
            bool[] discovered = new bool[graph.Count + 1];

            // stores level of each vertex in BFS
            int[] level = new int[graph.Count + 1];

            // mark source vertex as discovered and 
            // set its level to 0
            discovered[v] = true;
            level[v] = 0;

            // create a queue to do BFS and enqueue 
            // source vertex in it
            Queue<int> q = new Queue<int>();
            q.Enqueue(v);

            // run till queue is not empty
            while (q.Count != 0)
            {
                // pop front node from the queue
                v = q.Peek();
                q.Dequeue();

                // do for every edge (v -> u)
                int u = 0;
                for (int i = 1; i < graph[v - 1].Count; i++)
                {
                    u = graph[v - 1][i];
                    // if vertex u is explored for first time
                    if (!discovered[u])
                    {
                        // mark it discovered
                        discovered[u] = true;

                        // set level as level of parent node + 1
                        level[u] = level[v] + 1;

                        // push the vertex into the queue
                        q.Enqueue(u);
                    }

                    // if the vertex is already been discovered and
                    // level of vertex u and v are same, then the 
                    // graph contains an odd-cycle & is not biparte
                    else if (level[v] == level[u])
                        return false;
                }
            }

            for (int i = 1; i < discovered.Length; i++)
            {
                if (discovered[i] == false)
                {
                    return false;
                }
            }
            return true;

        }*/

    }
}
