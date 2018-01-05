using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphVirtualizationTool.Model
{
    class AdjacencyMatrix : FileHandlerInterface
    {
        public int TryParseInt32(string text, ref int value)
        {
            int tmp;
            if (int.TryParse(text, out tmp))
            {
                value = tmp;
                return 1;
            }
            else
            {
                return -1; // Leave "value" as it was
            }
        }
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
                    int item = -1;
                    //convert to integers
                    List<bool> convertedItems = new List<bool>();
                    foreach (var integer in items)
                    {
                        if (TryParseInt32(integer, ref item) == 1 && (item == 0 || item == 1))
                            convertedItems.Add(item == 0 ? false : true);
                        item = -1;
                    }
                    ++rows;
                    if (!(items.Length > 1))
                        throw new Exception($"Row {rows} is corrupted!");

                    if (rows == 1)
                    {
                        matrix.Add(convertedItems.ToList());

                        //columns constant integer is initiliazed
                        columns = convertedItems.Count;
                    }
                    else if (convertedItems.Count == columns)
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
                reader.Close();
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
            Random random = new Random();

            int rows = matrix.Count;

            for (int row = 0; row < rows; row++)
                nodes.Add(new Node() { Name = $"node {row}", X = random.Next(50,500), Y = random.Next(50, 500) });
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
