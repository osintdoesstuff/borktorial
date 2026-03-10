using System.Buffers.Text;
using System.Reflection;
using System.Text;

namespace aperture
{
    /// <summary>
    /// lib testing dingus
    /// </summary>

    public static class bktLV
    {
        public static (int maj, int min, int pch, char rv) aprtVer = (0, 4, 3, 'b');
        public static int[] puC { get; } = [255, 127, 63, 31, 15, 7, 3, 2];
        public static int[] puD { get; } = [1, 3, 7, 15, 31, 63, 127, 254];
        public static int[] dallf() {

            int[] puE = new int[8];
            for (int i = 0; i < puC.Length; i++)
            {
                puE[i] = puC[i] + puD[i];
            }
            return puE;
        }
    }
    public static class bktStf
    {
        public static int[] nrHndlr(string nrs)
        {
            nrs = nrs.Replace(">", "");
            nrs = nrs.Replace("<", "");
            string[] nrA = nrs.Split("-");
            int i0 = int.Parse(nrA[0]);
            int i1 = int.Parse(nrA[1]);
            return [i0, i1];
        }
        public static string pNrH(string input, Random rand)
        {
            while (input.Contains("<nr"))
            {
                int start = input.IndexOf("<nr");
                int end = input.IndexOf(">", start);
                string tag = input.Substring(start, end - start + 1);
                // tag = "<nr0-255>"

                int[] range = nrHndlr(tag.Substring(3)); // removes "<nr"
                int result = rand.Next(range[0], range[1] + 1);

                input = input.Replace(tag, result.ToString());
            }
            return input;
        }
        public static string byteFormat(UInt128 bytes) 
        {
            if (bytes < (UInt128)Math.Pow(1024, 1))
                return $"{bytes}B";
            else if (bytes < (UInt128)Math.Pow(1024, 2))
                return $"{bytes / (UInt128)Math.Pow(1024, 1)}KB";
            else if (bytes < (UInt128)Math.Pow(1024, 3))
                return $"{bytes / (UInt128)Math.Pow(1024, 2)}MB";
            else if (bytes < (UInt128)Math.Pow(1024, 4))
                return $"{bytes / (UInt128)Math.Pow(1024, 3)}GB";
            else if (bytes < (UInt128)Math.Pow(1024, 5))
                return $"{bytes / (UInt128)Math.Pow(1024, 4)}TB";
            else if (bytes < (UInt128)Math.Pow(1024, 6))
                return $"{bytes / (UInt128)Math.Pow(1024, 5)}PB";
            else if (bytes < (UInt128)Math.Pow(1024, 7))
                return $"{bytes / (UInt128)Math.Pow(1024, 6)}EB";
            else if (bytes < (UInt128)Math.Pow(1024, 8))
                return $"{bytes / (UInt128)Math.Pow(1024, 7)}ZB";
            else if (bytes < (UInt128)Math.Pow(1024, 9))
                return $"{bytes / (UInt128)Math.Pow(1024, 8)}YB";
            else if (bytes < (UInt128)Math.Pow(1024, 10))
                return $"{bytes / (UInt128)Math.Pow(1024, 9)}RB";
            else if (bytes < (UInt128)Math.Pow(1024, 11))
                return $"{bytes / (UInt128)Math.Pow(1024, 10)}QB";
            else return $"Yes.";
        }
        public static string byteFormat(ulong bytes)
        {
            if (bytes < (ulong)Math.Pow(1024, 1))
                return $"{bytes}B";
            else if (bytes < (ulong)Math.Pow(1024, 2))
                return $"{bytes / (ulong)Math.Pow(1024, 1)}KB";
            else if (bytes < (ulong)Math.Pow(1024, 3))
                return $"{bytes / (ulong)Math.Pow(1024, 2)}MB";
            else if (bytes < (ulong)Math.Pow(1024, 4))
                return $"{bytes / (UInt128)Math.Pow(1024, 3)}GB";
            else if (bytes < (ulong)Math.Pow(1024, 5))
                return $"{bytes / (ulong)Math.Pow(1024, 4)}TB";
            else if (bytes < (ulong)Math.Pow(1024, 6))
                return $"{bytes / (ulong)Math.Pow(1024, 5)}PB";
            else if (bytes < (ulong)Math.Pow(1024, 7))
                return $"{bytes / (ulong)Math.Pow(1024, 6)}EB";
            else if (bytes < (ulong)Math.Pow(1024, 8))
                return $"{bytes / (ulong)Math.Pow(1024, 7)}ZB";
            else if (bytes < (ulong)Math.Pow(1024, 9))
                return $"{bytes / (ulong)Math.Pow(1024, 8)}YB";
            else if (bytes < (ulong)Math.Pow(1024, 10))
                return $"{bytes / (ulong)Math.Pow(1024, 9)}RB";
            else if (bytes < (ulong)Math.Pow(1024, 11))
                return $"{bytes / (ulong)Math.Pow(1024, 10)}QB";
            else return $"Yes.";
        }
        public static byte[] mkRndByteArray(int length)
        {
            List<byte> tempBytes = new();
            Random rand = new();
            for (int i = 0; i < length; i++)
            {
                tempBytes.Add((byte)rand.Next(0, 256));
            }
            return [.. tempBytes];
        }
        public static double mapValue(double input, List<(double from, double to)> points)
        {
            List<(double from, double to)> sorted = [.. points.OrderBy(p => p.from)];

            if (input <= sorted.First().from) return sorted.First().to;
            if (input >= sorted.Last().from) return sorted.Last().to;

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                double fromA = sorted[i].from;
                double toA = sorted[i].to;
                double fromB = sorted[i + 1].from;
                double toB = sorted[i + 1].to;

                if (input >= fromA && input <= fromB)
                {
                    double t = (input - fromA) / (fromB - fromA);
                    return toA + t * (toB - toA);
                }
            }

            return 0;
        }
        /// <summary>
        /// NookEnc. Named after Tom Nook (yes, the animal crossing one) for...reasons that exist i'm sure
        /// Not sure why. Note: case sensitive
        /// </summary>
        /// <param name="un">a</param>
        /// <param name="pw">b</param>
        /// <returns></returns>
        public static string nookEnc(string un, string pw)
        {
            string rslt = "";
            if(un.Length > pw.Length)
            {
                int diff = un.Length - pw.Length;
                pw = pw.PadLeft(diff, '\xFF');
            }
            if(pw.Length > un.Length)
            {
                int diff = pw.Length - un.Length;
                un = un.PadLeft(diff, '\xFF');
            }
            for (int i = 0; i < pw.Length-1; i++)
            {
                rslt += un[i] ^ pw[i];
                rslt += pw[i] ^ un[i];
            }
            return rslt;
        }
        public static void dumpState<T>()
        {
            Type type = typeof(T);
            Console.WriteLine($"--- {type.Name} State ---");

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(null);
                Console.WriteLine($"{property.Name} = {value}");
            }
        }
    }
}
