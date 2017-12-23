using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphVirtualizationTool
{
    class AdjacencyMatrix : FileHandlerInterface
    {
        public List<List<int>> ParseFile(string filename)
        {
            List<List<int>> matrix = new List<List<int>>();

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
                int[] convertedItems = Array.ConvertAll(items, int.Parse);
                foreach (var item in convertedItems)
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

            return matrix;
        }

        public Tuple<IEnumerable<Node>, IEnumerable<Edge>> readMatrix(List<List<int>> matrix)
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
                    if (matrix.ElementAt(row).ElementAt(col) == 1)
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
