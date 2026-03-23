namespace aperture
{
    public class bktVersion(int maj, int min, int pch, char rev)
    {
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
        public override string ToString()
        {
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
            return (maj * 8) + (min * 6) + (pch * 4) + (rev * 2);
        }
    }
}
