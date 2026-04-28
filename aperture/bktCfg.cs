namespace aperture
{
    public static class bktCfg
    {
        public static cfgEnt ln2CfgE(string line)
        {
            try
            {
                string[] tokens = line.Split(" ");
                string name = tokens[0];
                if (tokens[1] != "=")
                {
                    throw new Exception("Config error: Invalid line");
                }
                int value = int.Parse(tokens[2]);
                return new(name, value);
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
        public static cfgEnt getEntByName(List<cfgEnt> list, string name)
        {
            foreach (cfgEnt ent in list)
            {
                if (ent.name == name)
                {
                    return ent;
                }
            }
            throw new Exception($"Could not find {name}");
        }
    }
    public class cfgEnt(string name, int value)
    {
        public string name = name;
        public int value = value;
        public override string ToString()
        {
            return $"{name} = {value}";
        }
    }
}
