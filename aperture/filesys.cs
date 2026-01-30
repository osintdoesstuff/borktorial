namespace aperture
{
    public class fileSys
    {
        
    }
    public struct vFile
    {
        public vFile(string nm, byte[] ct, attrib[] at)
        {
            name = nm;
            contents = ct;
            attribs = at;
        }
        public string name;
        public byte[] contents;
        public attrib[] attribs;
    }
    public struct vDir
    {
        public vDir(string nm, List<vFile> fls, attrib[] attr)
        {
            name = nm;
            files = fls;
            attribs = attr;
        }
        public string name;
        public List<vFile> files;
        public attrib[] attribs;
    }
    public enum attrib
    { 
        None,
        Hidden,
        System,
        Readonly,
        bktRs1, // these attribs are for later use i guess
        bktRs2,
        bktRs3,
        bktRs4,
        bktRs5,
        bktRs6,
        bktRs7
    }
}
