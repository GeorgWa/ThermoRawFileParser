using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThermoRawFileParser.Util
{
    public class TableData
    {
        public HashSet<string> Headers { get; private set; }
        public List<Dictionary<string,string>> Data { get; private set; }
        public TableData()
        {
            Headers = new HashSet<string>();
            Data = new List<Dictionary<string, string>>();
        }

        public void AddRow(Dictionary<string, string> row)
        {
            Headers.UnionWith(row.Keys);
            Data.Add(row);
        }

        public void ToCSV(string outputpath, string separator = ",", string NA = "na")
        {
            using (StreamWriter output = new StreamWriter(outputpath, false))
            {
                output.WriteLine(String.Join(separator, Headers.Select(k => k.Contains(separator) ? "\"" + k + "\"" : k)));
                foreach (Dictionary<string, string> row in Data)
                {
                    List<string> values = new List<string>(Headers.Count);

                    foreach (string key in Headers)
                    {
                        row.TryGetValue(key, out string value);
                        values.Add(value is null ? 
                            NA : 
                            value.Contains(separator) ? 
                            "\"" + value + "\"" :
                            value);
                    }

                    output.WriteLine(String.Join(separator, values));
                }
            }
        }
    }
}
