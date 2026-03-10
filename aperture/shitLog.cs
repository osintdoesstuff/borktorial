namespace aperture
{
    /// <summary>
    /// Shitlog: A shitty log
    /// </summary>
    public static class shitLog
    {
        private static readonly Lock logLock = new();

        public static void createEntry(string proc, 
            string descr, 
            logType lt, 
            string dateFormat = "R", 
            string filename = "bktLog.txt",
            long maxSize = 1048576,
            bool doRotate = true,
            string csLogType = "__bkt::default(0)(correct_horse_battery_staple)")
        {
            lock (logLock)
            {
                FileInfo logFile = new(filename);

                if (logFile.Exists && logFile.Length > maxSize && doRotate == true)
                {
                    string timestamp = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
                    string archive = $"{filename}-{timestamp}.txt";
                    File.Move(filename, archive);
                    File.AppendAllText(filename,
                        $"[{DateTime.UtcNow.ToString(dateFormat)}] info: [SHITLOG]: Get rotated idiot (into {archive}).\r\n");
                }
                string typeStr = lt switch
                {
                    logType.Null => "null",
                    logType.Warn => "warn",
                    logType.Info => "info",
                    logType.Err => "err",
                    _ => "unk"
                };
                if(csLogType != "__bkt::default(0)(correct_horse_battery_staple)")
                {
                    typeStr = csLogType;
                }
                File.AppendAllText(filename,
                    $"[{DateTime.UtcNow.ToString(dateFormat)}] {typeStr}: [{proc}]: {descr}\r\n");
            }
        }
    }
    public enum logType
    {
        Null,
        Info,
        Warn,
        Err
    }
}