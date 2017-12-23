using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class Algorithms
    {
        static int V = 4;
        public bool isBipartite(bool[,] G, int src, int[] colorArr)
        {

            // Create a color array to store colors assigned to all veritces. Vertex 
            // number is used as index in this array. The value '-1' of  colorArr[i] 
            // is used to indicate that no color is assigned to vertex 'i'.  The value 
            // 1 is used to indicate first color is assigned and value 0 indicates 
            // second color is assigned.

            for (int i = 0; i < V; ++i)
                colorArr[i] = -1;

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
                if (G[u, u] == true)
                    return false;

                // Find all non-colored adjacent vertices
                for (int v = 0; v < V; ++v)
                {
                    // An edge from u to v exists and destination v is not colored
                    if (G[u, v] && (colorArr[v] == -1))
                    {
                        // Assign alternate color to this adjacent v of u
                        colorArr[v] = 1 - colorArr[u];
                        q.Enqueue(v);
                    }

                    //  An edge from u to v exists and destination v is colored with
                    // same color as u
                    else if (G[u, v] && colorArr[v] == colorArr[u])
                        return false;
                }
            }

            // If we reach here, then all adjacent vertices can be colored with 
            // alternate color
            return true;
        }
        bool isConnected()
        {
            //TODO 
            return true;
        }
    }
}
