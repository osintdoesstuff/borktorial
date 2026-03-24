namespace aperture
{
    /// <summary>
    /// Shitlog: A shitty log
    /// </summary>
    public static class shitLog
    {
        private static readonly Lock logLock = new();

        /// <summary>
        /// Creates a log entry. Thread-safe-ish-probably
        /// To specifiy parameters that you keep reusing, just make a wrapper around this
        /// </summary>
        /// <param name="proc">Process</param>
        /// <param name="descr">Description</param>
        /// <param name="lt">Log type</param>
        /// <param name="dateFormat">Date format</param>
        /// <param name="filename">Filename</param>
        /// <param name="maxSize">Max size before rotation</param>
        /// <param name="doRotate">If it should rotate at all</param>
        /// <param name="csLogType">Custom log type</param>
        /// <param name="rotateDateFormat">The date format that gets used for the filename of a archive log</param>
        public static void createEntry(string proc,
            string descr,
            logType lt,
            string dateFormat = "R",
            string filename = "bktLog.txt",
            long maxSize = 1048576,
            bool doRotate = true,
            string csLogType = "",
            string rotateDateFormat = "yyyyMMdd-HHmmss")
        {
            lock (logLock)
            {
                FileInfo logFile = new(filename);

                if (logFile.Exists && logFile.Length > maxSize && doRotate == true)
                {
                    string timestamp = DateTime.UtcNow.ToString(rotateDateFormat);
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
                if (csLogType != "")
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