using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Better_NCP_Editor
{
    public static class JsonListLoader
    {
        public static List<string> Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The specified JSON file was not found.", filePath);

            string json = File.ReadAllText(filePath);
            List<string> list = JsonSerializer.Deserialize<List<string>>(json);
            return list;
        }
    }
}
