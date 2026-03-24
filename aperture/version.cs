namespace aperture
{
    public class bktVersion(int maj, int min, int pch, char rev, snapshotVer snapshot, devStage stage = devStage.Release)
    {
        /// <summary>
        /// The minimum version that can be represented by this
        /// </summary>
        public static bktVersion minVer { get; } = new(int.MinValue, int.MinValue, int.MinValue, 'a', snapshotVer.minVer);
        /// <summary>
        /// The maximum version that can be represented by this
        /// </summary>
        public static bktVersion maxVer { get; } = new(int.MaxValue, int.MaxValue, int.MaxValue, 'z', snapshotVer.minVer);
        /// <summary>
        /// Major
        /// </summary>
        public int maj { get; set; } = maj;
        /// <summary>
        /// Minor
        /// </summary>
        public int min { get; set; } = min;
        /// <summary>
        /// Patch
        /// </summary>
        public int pch { get; set; } = pch;
        /// <summary>
        /// Revision
        /// </summary>
        public char rev { get; set; } = rev;
        public devStage stage { get; set; } = stage;
        public snapshotVer snapshot { get; set; } = snapshot;
        /// <summary>
        /// Converts version into a string
        /// </summary>
        /// <returns>A stringified version</returns>
        public override string ToString()
        {
            if (stage != devStage.Release)
            {
                if (snapshot != snapshotVer.minVer)
                {
                    return $"{stage} {maj}.{min}.{pch}{rev} {snapshot}";
                }
                return $"{stage} {maj}.{min}.{pch}{rev}";
            }
            if (snapshot != snapshotVer.minVer)
            {
                return $"{maj}.{min}.{pch}{rev} {snapshot}";
            }
            return $"{maj}.{min}.{pch}{rev}";
        }
        /// <summary>
        /// Checks if v1 and v2 are equal
        /// </summary>
        /// <param name="v1">Version 1</param>
        /// <param name="v2">Version 2</param>
        /// <returns>True if equal, false otherwise</returns>
        public static bool isEqual(bktVersion v1, bktVersion v2)
        {
            if (v1.maj == v2.maj)
            {
                if (v1.min == v2.min)
                {
                    if (v1.pch == v2.pch)
                    {
                        if (v1.rev == v2.rev)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if v1 is newer than v2
        /// </summary>
        /// <param name="v1">Version 1</param>
        /// <param name="v2">Version 2</param>
        /// <returns>True if equal, false otherwise</returns>
        public static bool isNewer(bktVersion v1, bktVersion v2)
        {
            if (v1.maj > v2.maj || v1.min > v2.min || v1.pch > v2.pch || v1.rev > v2.rev)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if v1 is older than v2
        /// </summary>
        /// <param name="v1">Version 1</param>
        /// <param name="v2">Version 2</param>
        /// <returns>True if equal, false otherwis</returns>
        public static bool isOlder(bktVersion v1, bktVersion v2)
        {
            if (v1.maj < v2.maj || v1.min < v2.min || v1.pch < v2.pch || v1.rev < v2.rev)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets a decent-ish hash for comparison
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (maj * 8) + (min * 6) + (pch * 4) + (rev * 2) + snapshot.GetHashCode();
        }
    }
    public class snapshotVer(int year, int month, int day, int build)
    {
        /// <summary>
        /// The minimum version this can represent
        /// </summary>
        public static snapshotVer minVer { get; } = new(0, 0, 0, 0);
        /// <summary>
        /// The maximum version this can represent
        /// </summary>
        public static snapshotVer maxVer { get; } = new(9999, 99, 99, 99);
        /// <summary>
        /// The year
        /// </summary>
        public int year { get; set; } = year;
        /// <summary>
        /// The month
        /// </summary>
        public int month { get; set; } = month;
        /// <summary>
        /// The day
        /// </summary>
        public int day { get; set; } = day;
        /// <summary>
        /// The build
        /// </summary>
        public int build { get; set; } = build;
        /// <summary>
        /// Converts snapshot to string
        /// </summary>
        /// <returns>A string representing the snapshot</returns>
        public override string ToString()
        {
            return $"{year:D4}{month:D2}{day:D2}-{build:D2}";
        }
        /// <summary>
        /// Makes a semi-decent hash for the snapshot
        /// </summary>
        /// <returns>A semi-decent hash for the snapshot</returns>
        public override int GetHashCode()
        {
            return (year * 8) + (month * 6) + (day * 4) + (build * 2);
        }
    }
    /// <summary>
    /// Development stage enum
    /// </summary>
    public enum devStage
    {
        Alpha,
        Beta,
        ReleaseCandidate,
        Release,
        Experimental
    }
}
