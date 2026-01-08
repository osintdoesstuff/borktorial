namespace aperture
{
    /// <summary>
    /// lib testing dingus
    /// </summary>

    public static class bktLV
    {
        public static int[] puC { get; } = [255, 127, 63, 31, 15, 7, 3, 2];
        public static int[] puD { get; } = [1, 3, 7, 15, 31, 63, 127, 254];
        public static int[] dallf() {

            int[] puE = new int[8];
            for (int i = 0; i < puC.Length; i++)
            {
                puE[i] = puC[i] + puD[i];
            }
            return puE;
        }
    }
    public static class bktStf
    {
        public static int[] nrHndlr(string nrs)
        {
            nrs = nrs.Replace(">", "");
            nrs = nrs.Replace("<", "");
            string[] nrA = nrs.Split("-");
            int i0 = int.Parse(nrA[0]);
            int i1 = int.Parse(nrA[1]);
            return [i0, i1];
        }
        public static string pNrH(string input, Random rand)
        {
            while (input.Contains("<nr"))
            {
                int start = input.IndexOf("<nr");
                int end = input.IndexOf(">", start);
                string tag = input.Substring(start, end - start + 1);
                // tag = "<nr0-255>"

                int[] range = nrHndlr(tag.Substring(3)); // removes "<nr"
                int result = rand.Next(range[0], range[1] + 1);

                input = input.Replace(tag, result.ToString());
            }
            return input;
        }
    }
}
