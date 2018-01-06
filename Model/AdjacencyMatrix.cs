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
        public List<List<T>> ParseFile<T>(string filename)
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
                return (List<List<T>>)Convert.ChangeType(matrix, typeof(List<List<T>>)) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (List<List<T>>)Convert.ChangeType(new List<List<bool>>(), typeof(List<List<T>>)) ;
            }
        }
    }
}
