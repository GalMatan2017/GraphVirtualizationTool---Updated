using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphVirtualizationTool.Model
{
    class AdjacencyMatrix : FileHandlerInterface
    {
        public T ParseFile<T>(string filename)
        {
            try
            {
                List<List<bool>> matrix = new List<List<bool>>();

                StreamReader reader = File.OpenText(filename);

                string line;
                int columns = 0,
                    rows = 0;

                //read line
                while ((line = reader.ReadLine()) != null)
                {
                    //split by whitespace
                    string[] items = line.Split(',');
                    ++rows;
                    if (!(items.Length > 1))
                        throw new Exception($"Row {rows} is corrupted!");
                    //convert to integers
                     
                    int[] convertedItemsint = Array.ConvertAll(items, int.Parse);
                    int size =convertedItemsint.Count();
                    bool[] convertedItems = new bool[size] ;
                    for(int i = 0; i < size; i++)
                    {
                        if (convertedItemsint[i] == 1)
                            convertedItems[i] = true;
                        if (convertedItemsint[i] == 0)
                            convertedItems[i] = false;
                    }
                    foreach (var item in convertedItemsint)
                      
                        if (item != 0 && item != 1)
                            throw new Exception($"Found illegal character at row {rows}");
                    if (rows == 1)
                    {
                        matrix.Add(convertedItems.ToList());
                        //columns constant integer is initiliazed
                        columns = convertedItems.Length;
                    }
                    else if (convertedItems.Length == columns)
                    {
                        matrix.Add(convertedItems.ToList());
                    }
                    else
                    {
                        throw new Exception($"Row #{rows} is corrupted!");
                    }

                }

                if (columns != rows)
                {
                    if (rows < columns)
                        throw new Exception("columns is bigger than rows");
                    else
                        throw new Exception("rows is bigger than columns");
                }

                return (T)Convert.ChangeType(matrix, typeof(T)) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (T)Convert.ChangeType(new List<List<bool>>(), typeof(T)) ;

            }
        }

        public Tuple<IEnumerable<Node>, IEnumerable<Edge>> readMatrix(List<List<bool>> matrix)
        {
            List<Node> nodes = new List<Node>();
            List<Edge> edges = new List<Edge>();

            int rows = matrix.Count;

            for (int row = 0; row < rows; row++)
                nodes.Add(new Node() { Name = $"node {row}", X = new Random().Next(100), Y = new Random().Next(100) });
            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = matrix.Count - 1; col > row - 1; col--)
                {
                    if (col == row)
                        continue;
                    if (matrix.ElementAt(row).ElementAt(col) == true)
                    {
                        edges.Add(new Edge()
                        {
                            Name = $"connector {new Random().Next(999)}",
                            Start = nodes.Single(x => x.Name.Equals($"node {row}")),
                            End = nodes.Single(x => x.Name.Equals($"node {col}"))
                        });
                    }
                }
            }

            return new Tuple<IEnumerable<Node>, IEnumerable<Edge>>(nodes, edges);

        }

    }


}
