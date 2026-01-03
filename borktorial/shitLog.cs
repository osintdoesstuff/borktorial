namespace borktorial
{
    /// <summary>
    /// Shitlog: A shitty log
    /// </summary>
    internal class shitLog
    {
        public static void createEntry(string proc, string descr, logType lt)
        {
            string typeStr = lt switch
            {
                logType.Null => "null",
                logType.Warn => "warn",
                logType.Info => "info",
                logType.Err => "err",
                _ => "unk"
            };
            File.AppendAllText("bktLog.txt",
                $"{DateTime.UtcNow} {typeStr}: [{proc}]: {descr}\r\n");
        }
    }
    public enum logType
    {
        Null,
        Info,
        Warn,
        Err,
    }
}