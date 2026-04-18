namespace aperture
{
    public static class bktCfg
    {
        public static cfgEnt ln2CfgE(string line)
        {
            try
            {
                string[] tokens = line.Split(" ");
                string name = tokens[1];
                bktTypes type = str2Bt(tokens[0]);
                if (tokens[2] != "=")
                {
                    throw new Exception("Config error: Invalid line");
                }
                object value = strV2BktV(tokens[3]);
                return new(type, name, value);
            }
            catch
            {
                throw;
            }
        }
        public static List<cfgEnt> parseFile(string fn)
        {
            string[] lines = File.ReadAllLines(fn);
            List<cfgEnt> entries = [];
            foreach (string line in lines)
            {
                if (line.StartsWith("//"))
                {
                    continue;
                }
                entries.Add(ln2CfgE(line));
            }
            return entries;
        }
        public static cfgEnt? getEntByName(List<cfgEnt> list, string name)
        {
            foreach (cfgEnt ent in list)
            {
                if (ent.name == name)
                {
                    return ent;
                }
            }
            return null;
        }
        public static bool typeChk(cfgEnt ce, bktTypes type)
        {
            if (ce.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static object strV2BktV(string str)
        {
            if (str.Contains('.'))
            {
                return double.Parse(str);
            }
            else if (str == "true" || str == "false")
            {
                return bool.Parse(str);
            }
            else
            {
                return int.Parse(str);
            }
        }
        public static string bt2Str(bktTypes bt)
        {
            return bt switch
            {
                bktTypes.None => "void",
                bktTypes.Int => "int",
                bktTypes.Float => "float",
                bktTypes.Bool => "bool",
                _ => "unk",
            };
        }
        public static bktTypes str2Bt(string str)
        {
            return str switch
            {
                "void" => bktTypes.None,
                "int" => bktTypes.Int,
                "float" => bktTypes.Float,
                "bool" => bktTypes.Bool,
                _ => throw new Exception($"Config error: Unknown type {str}"),
            };
        }
    }
    public class cfgEnt(bktTypes type, string? name, object? value)
    {
        public bktTypes type = type;
        public string? name = name;
        public object? value = value;
        public override string ToString()
        {
            return $"{type} {name} = {value}";
        }
    }
    public enum bktTypes
    {
        None,
        Int,
        Float,
        Bool,
    }
}
