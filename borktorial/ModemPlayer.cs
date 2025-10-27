using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

internal static class ModemPlayer
{
    // 1) public API ----------------------------------------------------------
    public static void PlayModemSound()
    {
        string tmp = Path.Combine(Path.GetTempPath(),
                                  $"bam-{Guid.NewGuid():N}.wav");
        ExtractResource("borktorial.SECRETS.modem.wav", tmp);

        try { new System.Media.SoundPlayer(tmp).PlaySync(); }  // blocks
        finally { TryDelete(tmp); }
    }

    // 2) resource extraction -------------------------------------------------
    private static void ExtractResource(string resName, string outPath)
    {
        using Stream? resStream =
            Assembly.GetExecutingAssembly().GetManifestResourceStream(resName);

        if (resStream is null)
            throw new InvalidOperationException(
                $"Embedded resource '{resName}' not found.");

        using FileStream outFile = File.Create(outPath);
        resStream.CopyTo(outFile);          // straight copy, fastest you’ll get
    }

    // 3) util ----------------------------------------------------------------
    private static void TryDelete(string path)
    {
        try { if (File.Exists(path)) File.Delete(path); }
        catch { /* ignore */ }
    }
}