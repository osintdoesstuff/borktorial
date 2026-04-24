using aperture;
using Microsoft.VisualBasic.Devices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace borktorial
{
    /// <summary>
    /// A system to make sure each copy of Borktorial is tied to the computer it was installed on
    /// Because this is a DOS simulator, it's assumed some less-nice people may try to use it for bad things for some reason (e.g. harmful pranks or using it as part of a virus or something)
    /// So, this tries to mitigate that by tying it to the specific computer it was used on
    /// It's not the most secure of things but ehh
    /// </summary>
    internal class borkVerf
    {
        static readonly string fn = $"VB{Environment.UserName}.bkt";
        public static byte[] sqwimbleify(byte[] data)
        {
            byte[] b64Lot = aprtMain.str2Ba(aprtMain.toB64(aprtMain.toB64(aprtMain.toB64(aprtMain.ba2Str(data)))));
            for (int i = 0; i < 8; i++)
            {
                b64Lot = SHA3_512.HashData(b64Lot);
            }
            return b64Lot;
        }
        public static string getCData()
        {
            string cDat = $"{Environment.UserName}\0";
            cDat += Environment.MachineName + "\0";
            ComputerInfo ci = new();
            cDat += ci.OSFullName + "\0";
            cDat += ci.TotalPhysicalMemory + "\0";
            cDat += Environment.ProcessorCount + "\0";
            cDat += ci.InstalledUICulture;
            // just windows things(R)
            cDat += (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("PROCESSOR_LEVEL") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("PROCESSOR_REVISION") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("USERDOMAIN") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("USERDOMAIN_ROAMINGPROFILE") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("USERNAME") ?? "\0") + "\0";
            cDat += (Environment.GetEnvironmentVariable("USERPROFILE") ?? "\0") + "\0";
            return cDat;
        }
        public static byte[] getSig() 
        {
            return sqwimbleify(aprtMain.str2Ba(getCData()));
        }
        public static void createSig()
        {
            File.WriteAllBytes(fn, getSig());
            return;
        }
        public static void clearSig()
        {
            File.Delete(fn);
        }
        public static bool checkBv()
        {
            if (!File.Exists(fn))
            {
                DialogResult dr1 = MessageBox.Show("A borkVerf sig for the current machine was not found!\r\n" +
                    "If you didn't download this program yourself from the GitHub repo, press No\r\n" +
                    "Otherwise, press yes to create a signature",
                    "borktorial",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (dr1 == DialogResult.Yes)
                {
                    createSig();
                    return true;
                }
                else
                {
                    Environment.Exit(1);
                    return false;
                }
            }
            else
            {
                byte[] currSig = File.ReadAllBytes(fn);
                byte[] normSig = getSig();
                if (!currSig.SequenceEqual(normSig))
                {
                    DialogResult dr2 = MessageBox.Show(
                        "WARNING! A borkVerf sig was found, but it does not match this machine!\r\n" +
                        "This message can appear either because you changed your computer in some way or ran a update or because you got the program from the wrong place\r\n" +
                        "Press Cancel to exit or OK to recreate your sig",
                        "borktorial",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);
                    if (dr2 == DialogResult.OK)
                    {
                        createSig();
                        return true;
                    }
                    else
                    {
                        Environment.Exit(1);
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
