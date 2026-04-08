using System.Reflection;
using System.Text;

namespace aperture
{
    public class aprtMain
    {
        public static int aprtVer { get; set; } = 465;
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
                int end = input.IndexOf('>', start);
                string tag = input.Substring(start, end - start + 1);
                // tag = "<nr0-255>"

                int[] range = nrHndlr(tag[3..]); // removes "<nr"
                int result = rand.Next(range[0], range[1] + 1);

                input = input.Replace(tag, result.ToString());
            }
            return input;
        }
        public static string byteFormat(UInt128 bytes)
        {
            if (bytes < (UInt128)Math.Pow(1024, 1))
            {
                return $"{bytes}B";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 2))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 1)}KB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 3))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 2)}MB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 4))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 3)}GB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 5))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 4)}TB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 6))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 5)}PB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 7))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 6)}EB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 8))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 7)}ZB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 9))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 8)}YB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 10))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 9)}RB";
            }
            else if (bytes < (UInt128)Math.Pow(1024, 11))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 10)}QB";
            }
            else
            {
                return $"Yes.";
            }
        }
        public static string byteFormat(ulong bytes)
        {
            if (bytes < (ulong)Math.Pow(1024, 1))
            {
                return $"{bytes}B";
            }
            else if (bytes < (ulong)Math.Pow(1024, 2))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 1)}KB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 3))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 2)}MB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 4))
            {
                return $"{bytes / (UInt128)Math.Pow(1024, 3)}GB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 5))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 4)}TB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 6))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 5)}PB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 7))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 6)}EB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 8))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 7)}ZB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 9))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 8)}YB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 10))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 9)}RB";
            }
            else if (bytes < (ulong)Math.Pow(1024, 11))
            {
                return $"{bytes / (ulong)Math.Pow(1024, 10)}QB";
            }
            else
            {
                return $"Yes.";
            }
        }
        public static byte[] mkRndByteArray(int length)
        {
            List<byte> tempBytes = [];
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

            if (input <= sorted.First().from)
            {
                return sorted.First().to;
            }

            if (input >= sorted.Last().from)
            {
                return sorted.Last().to;
            }

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
            if (un.Length > pw.Length)
            {
                int diff = un.Length - pw.Length;
                pw = pw.PadLeft(diff, '\xFF');
            }
            if (pw.Length > un.Length)
            {
                int diff = pw.Length - un.Length;
                un = un.PadLeft(diff, '\xFF');
            }
            for (int i = 0; i < pw.Length - 1; i++)
            {
                rslt += un[i] ^ pw[i];
                rslt += pw[i] ^ un[i];
            }
            return rslt;
        }

        public static string ba2Str(byte[] ba)
        {
            List<char> inter = [];
            foreach(byte item in ba)
            {
                inter.Add((char)item);
            }
            char[] inter2 = [.. inter];
            StringBuilder sb = new(inter2.Length);
            for (int i = 0; i < inter2.Length; i++)
            {
                sb.Append(inter2[i]);
            }
            return sb.ToString();
        }
        public static byte[] str2Ba(string s)
        {
            List<char> inter = [.. s.ToCharArray()];
            List<byte> inter2 = [];
            for (int i = 0; i < inter.Count; i++)
            {
                inter2.Add((byte)inter[i]);
            }
            return [.. inter2];
        }
        public static string toB64(string norm)
        {
            return System.Convert.ToBase64String(str2Ba(norm));
        }
        public static string fromB64(string b64)
        {
            return System.Text.Encoding.UTF8.GetString(
                                        System.Convert.FromBase64String(b64));
        }
        public static string md5(byte[] data)
        {
            List<byte> hData = [.. data];
            hData.AddRange(str2Ba("__bktsortedMD5::why_so_salty?#lazyCrypto__a secret that is not secret at all:7B541C0441FC5507B453656F0BE2B2EBEA944A7BDB7E46E4B8E6AC75DAFFE2BFAF1310F3EDF2110146E3CDC26F7A12B702FC53264B4EDEA533857264CC8F3EB43CA3BEA4F161F6BB"));
            hData.Reverse();
            byte[] hashed = System.Security.Cryptography.MD5.HashData([.. hData]);
            string str = "";
            foreach (byte item in hashed)
            {
                str += item.ToString("X2");
            }
            List<byte> strL = [.. str2Ba(str)];
            strL.Reverse(); // i kinda wonder if it's possible to have a palindrome hash? i guess we'll never really know
            str = ba2Str([.. strL]);
            return str;
        }
        public static void dumpState<T>()
        {
            Type type = typeof(T);
            Console.WriteLine($"--- {type.Name} State ---");

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(null);
                Console.WriteLine($"{property.Name} = {value}");
            }
        }
        /// <summary>
        /// Makes a shitty username. Kinda-ish like the Reddit default usernames
        /// </summary>
        /// <param name="rand">A suitable RNGesus to decide shit</param>
        /// <returns>A shitty username</returns>
        public static string mkShitUsername(Random rand)
        {
            string[] adjectives = [
                "Big", "Small", "Huge", "Little", "Lil", "Medium",
                "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
                "Swinging", "Running", "Walking", "Ducking", "Crouching", "Climbing",
                "Stupid", "Smart", "Ugly", "Pretty", "Fast", "Slow",
                "Edible", "Usable", "Delicious", "Bitter", "Shitty",
                "Shiny", "Glowing", "Heavenly", "Demonic",
                "Sneaky", "Silent", "Loud"
            ];
            string[] things = [
                "Fish", "Duck", "Dog", "Cat", "Cow", "Deer", "Mouse", "Turtle", "Bigfoot",
                "Computer", "Printer", "Robot", "Program", "Monitor",
                "Cup", "Mug", "Plate", "Kitchen", "Pen", "Clock",
                "Table", "Chair", "Couch", "Flooring", "Wallpaper",
                "Fruit", "Paper", "Carpet", "Sign", "Tree",
                "Command", "Kerbal", "Planet", "Moon", "Asteroid", "Comet",
                "Creeper", "Skeleton", "Zombie", "Spider", "Witch", "Pillager", "Enderman",
                "Pizza", "Chicken", "Potato", "Fries", "Pancake", "Fry", "Salad",
                "Sandwich", "Corn", "Popcorn", "Chips"
                ];
            string[] separators = ["", "-", "_", "", "", "", "", "", ""]; // some padding to make the separators more rare
            string name = adjectives[rand.Next(adjectives.Length)] +
                separators[rand.Next(separators.Length)] +
                adjectives[rand.Next(adjectives.Length)] +
                separators[rand.Next(separators.Length)] +
                things[rand.Next(things.Length)];
            if (rand.Next(0, 10) == 0)
            {
                name += rand.Next(1000, 10000); // random 4 digit value
            }
            return name;
        }
        /// <summary>
        /// Generate random hex strings
        /// </summary>
        /// <param name="length">Length</param>
        /// <param name="gs">Group size</param>
        /// <param name="sep">Separator character</param>
        /// <returns>A random hex string with the params</returns>
        public static string genHexStr(int length, int gs = 0, char sep = '-')
        {
            Random rand = new();
            char[] hexDigits = "0123456789ABCDEF".ToCharArray();
            StringBuilder sb = new(length + (gs > 0 ? length / gs : 0));

            for (int i = 0; i < length; i++)
            {
                if (gs > 0 && i > 0 && i % gs == 0)
                {
                    sb.Append(sep);
                }

                sb.Append(hexDigits[rand.Next(hexDigits.Length)]);
            }

            return sb.ToString();
        }
        /// <summary>
        /// Generates random alphanumeric string
        /// </summary>
        /// <param name="length">Length</param>
        /// <param name="gs">Group size</param>
        /// <param name="sep">Separator character</param>
        /// <returns>A random alphanumeric string with the params</returns>
        public static string genAnStr(int length, int gs = 0, char sep = '-')
        {
            Random rand = new();
            char[] anDigits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            StringBuilder sb = new(length + (gs > 0 ? length / gs : 0));

            for (int i = 0; i < length; i++)
            {
                if (gs > 0 && i > 0 && i % gs == 0)
                {
                    sb.Append(sep);
                }

                sb.Append(anDigits[rand.Next(anDigits.Length)]);
            }

            return sb.ToString();
        }
        /// <summary>
        /// you get the idea by now. much more general purpose tho
        /// </summary>
        /// <param name="length">length</param>
        /// <param name="gs">group size</param>
        /// <param name="sep">separator</param>
        /// <param name="key">chars</param>
        /// <returns>random string with params</returns>
        public static string rndStrGen(int length, int gs = 0, char sep = '-', string key = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
        {
            Random rand = new();
            char[] anDigits = key.ToCharArray();
            StringBuilder sb = new(length + (gs > 0 ? length / gs : 0));

            for (int i = 0; i < length; i++)
            {
                if (gs > 0 && i > 0 && i % gs == 0)
                {
                    sb.Append(sep);
                }

                sb.Append(anDigits[rand.Next(anDigits.Length)]);
            }

            return sb.ToString();
        }
    }
}
