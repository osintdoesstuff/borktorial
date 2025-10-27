using System;
using System.Collections.Generic;
using System.IO;

namespace borktorial
{
    internal static class bktParse
    {
        // da parser
        public static Dictionary<string, object> Parse(string fp)
        {
            if (!File.Exists(fp))
                throw new Exception("BKT error 01: File not found");

            string[] lines = File.ReadAllLines(fp);
            var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            bool inBlock = false;

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("$ ")) continue;

                
                if (line.StartsWith(">>BKT") && line.Contains("BEGIN"))
                {
                    inBlock = true;
                    continue;
                }

                if (!inBlock) continue;

                if (!line.Contains("=")) continue;

                string[] parts = line.Split('=', 2, StringSplitOptions.TrimEntries);

                string left = parts[0].Trim();
                string right = parts[1].Trim().Trim(';');

                string[] tokens = left.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length != 2)
                    throw new Exception($"BKT error 02 at line: '{line}'");

                string type = tokens[0];
                string name = tokens[1];

                object value = ParseValue(type.ToLower(), right);
                result[name] = value;
            }

            return result;
        }

        // Value parser
        private static object ParseValue(string type, string raw)
        {
            return type switch
            {
                "string" => raw.Trim('"'), // remove quotes
                "int" => uint.Parse(raw),
                "bool" => bool.Parse(raw),
                "float" => float.Parse(raw),
                _ => throw new Exception($"BKT error 03: Unknown type '{type}'")
            };
        }
    }
}