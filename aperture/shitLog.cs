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
            long maxSize = 1048576,
            bool doRotate = true)
        {
            lock (_lock)
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