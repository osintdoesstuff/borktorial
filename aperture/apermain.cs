using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aperture
{
    /// <summary>
    /// lib testing dingus
    /// </summary>
    public static class bktLV
    {
        public static int[] puC { get; } = { 255, 127, 63, 31, 15, 7, 3, 1 };
        public static int[] puD { get; } = { 1, 3, 7, 15, 31, 63, 127, 255 };
        public static int[] dallf() {

            int[] puE = new int[8];
            for (int i = 0; i < puC.Length; i++)
            {
                puE[i] = puC[i] + puD[i];
            }
            return puE;
        }
        /// <summary>
        /// Mod thing.
        /// </summary>
        public static void mod()
        {
            string modLoaderPath = "modldr.dll";
            if (File.Exists(modLoaderPath))
            {
                try
                {
                    var asm = System.Reflection.Assembly.LoadFrom(modLoaderPath);
                    // Look for entry point
                    var type = asm.GetType("modldr");
                    var method = type?.GetMethod("ml_init", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (method != null)
                    {
                        method.Invoke(null, null);
                        Console.WriteLine("LdS000$");
                    }
                    else
                    {
                        throw new Exception("LdE129$");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
        public interface modInterface { 
            string name { get; } // The name of the mod
            void mEntryPoint(); // The entry point
        }
    }
    /// <summary>
    /// Comms packets
    /// </summary>
    public class msgPacket
    {
        public string[] messageData { get; set; }
        public permLevel perm { get; set; }
        public string msgID { get; set; }

        public msgPacket(string[] data, permLevel pmL, string id)
        {
            messageData = data;
            perm = pmL;
            msgID = id;
        }
    }
    /// <summary>
    /// packet handler
    /// </summary>
    public static class bktPkt {
        static Random rand = new Random();
        public static string idGen()
        {
            char[] allowedCh = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
            string id = "msg-";
            for (int i = 0; i < 32; i++)
            {
                char ch = allowedCh[rand.Next(0, allowedCh.Length)];
                id = id + ch;
            }
            return id;
        }
        public static void handlePkt(msgPacket packet) {
            string[] pDat = packet.messageData;
            permLevel pmL = packet.perm;
            switch (pDat)
            {
                case ["aa", "00"]:
                    switch (pmL)
                    {
                        case permLevel.Root:
                            Environment.Exit(0);
                            break;
                        case permLevel.Admin:
                            Environment.Exit(0);
                            break;
                        case permLevel.System:
                            Environment.Exit(0);
                            break;
                        case permLevel.Max:
                            Environment.Exit(0);
                            break;
                        default:
                            throw new messagePermissionException();
                            break;
                    }
                    break;
            }
        }
    }
    public enum permLevel
    {
        Null,
        User,
        Root,
        Admin,
        System,
        Max
    }
    public class messagePermissionException : Exception
    {
        public messagePermissionException() : base("A message permission exception has occurred") { }
        public messagePermissionException(permLevel got, permLevel expected) : 
            base($"Expected permission level {expected.ToString()}, got {got.ToString()}") { }
    }
}
