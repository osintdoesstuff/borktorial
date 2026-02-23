namespace aperture
{
    /// <summary>
    /// Shitlog: A shitty log
    /// </summary>
    public static class shitLog
    {
        private static readonly Lock _lock = new();

        public static void createEntry(string proc, 
            string descr, 
            logType lt, 
            string dateFormat = "R", 
            string filename = "bktLog.txt",
            long maxSize = 1048576)
        {
            lock (_lock)
            {
                FileInfo logFile = new(filename);

                if (logFile.Exists && logFile.Length > maxSize)
                {
                    string timestamp = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
                    string archive = $"{filename}-{timestamp}.txt";
                    File.Move(filename, archive);
                    createEntry("SHITLOG", $"Get rotated idiot (Into {archive})", logType.Info);
                }
                string typeStr = lt switch
                {
                    logType.Null => "null",
                    logType.Warn => "warn",
                    logType.Info => "info",
                    logType.Err => "err",
                    _ => "unk"
                };
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