using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class AdjacencyList : FileHandlerInterface
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
                //return value
                List<List<int>> list = new List<List<int>>();
                //open file
                StreamReader reader = File.OpenText(filename);
                string line;
                int columns = 0,
                    rows = 0;
                //read line
                while ((line = reader.ReadLine()) != null)
                {
                    //split by comma
                    string[] items = line.Split(':', ',');
                    //split by whitespace
                    int item = -1;
                    //convert to integers
                    List<int> convertedItems = new List<int>();
                    foreach (var integer in items)
                    {
                        if (TryParseInt32(integer, ref item) == 1)
                            convertedItems.Add(item);
                    }
                    ++rows;
                    if (!(items.Length > 1))
                        throw new Exception($"Row {rows} is corrupted!");
                    //convert to integers
                    if (rows == 1)
                    {
                        list.Add(convertedItems.ToList());
                        //columns constant integer is initiliazed
                        columns = convertedItems.Count;
                    }
                    else
                    {
                        list.Add(convertedItems.ToList());
                    }
                }
                reader.Close();
                return   (T)Convert.ChangeType(list, typeof(T));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (T)Convert.ChangeType(new List<List<int>>(), typeof(T));
            }
        }
    }
}
