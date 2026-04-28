namespace aperture
{
    // this is AI slop code but it WORKS

    public class fileSys
    {
        public List<vFile> rootFiles = new(65536);
        public List<vDir> rootDirs = new(65536);
        public string workingPath = "\\";
        public string serNum = "XXXX-XXXX";

        //=== SERIALIZATION ===

        /// <summary>
        /// Serialize to binary blob
        /// </summary>
        public byte[] toBinary()
        {
            using MemoryStream ms = new();
            using BinaryWriter bw = new(ms);

            bw.Write(workingPath);
            bw.Write(serNum);
            writeFileList(bw, rootFiles);
            writeDirList(bw, rootDirs);

            return ms.ToArray();
        }

        /// <summary>
        /// Load from binary blob
        /// </summary>
        public static fileSys fromBinary(byte[] data)
        {
            using MemoryStream ms = new(data);
            using BinaryReader br = new(ms);

            fileSys fs = new()
            {
                workingPath = br.ReadString(),
                serNum = br.ReadString(),
                rootFiles = readFileList(br),
                rootDirs = readDirList(br)
            };

            return fs;
        }

        //=== BINARY HELPERS ===

        public static void writeFileList(BinaryWriter bw, List<vFile> files)
        {
            bw.Write(files.Count);
            foreach (vFile f in files)
            {
                bw.Write(f.name);
                bw.Write(f.contents.Length);
                bw.Write(f.contents);
                bw.Write(f.attribs.Length);
                foreach (fileAttrib a in f.attribs)
                {
                    bw.Write((int)a);
                }
            }
        }

        public static void writeDirList(BinaryWriter bw, List<vDir> dirs)
        {
            bw.Write(dirs.Count);
            foreach (vDir d in dirs)
            {
                bw.Write(d.name);
                bw.Write(d.attribs.Length);
                foreach (fileAttrib a in d.attribs)
                {
                    bw.Write((int)a);
                }

                writeFileList(bw, d.files);
                writeDirList(bw, d.subDirs);  // recursive
            }
        }

        public static List<vFile> readFileList(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<vFile> files = new(count);
            for (int i = 0; i < count; i++)
            {
                string name = br.ReadString();
                int contentLen = br.ReadInt32();
                byte[] contents = br.ReadBytes(contentLen);
                int attrLen = br.ReadInt32();
                fileAttrib[] attribs = new fileAttrib[attrLen];
                for (int j = 0; j < attrLen; j++)
                {
                    attribs[j] = (fileAttrib)br.ReadInt32();
                }

                files.Add(new vFile(name, contents, attribs));
            }
            return files;
        }

        public static List<vDir> readDirList(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<vDir> dirs = new(count);
            for (int i = 0; i < count; i++)
            {
                string name = br.ReadString();
                int attrLen = br.ReadInt32();
                fileAttrib[] attribs = new fileAttrib[attrLen];
                for (int j = 0; j < attrLen; j++)
                {
                    attribs[j] = (fileAttrib)br.ReadInt32();
                }

                List<vFile> files = readFileList(br);
                List<vDir> subDirs = readDirList(br);  // recursive
                dirs.Add(new vDir(name, files, subDirs, attribs));
            }
            return dirs;
        }

        // Converts relative path to absolute
        public string resolvePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return workingPath;
            }

            if (path.StartsWith('\\'))
            {
                return path;
            }

            if (workingPath == "\\")
            {
                return "\\" + path;
            }

            return workingPath + "\\" + path;
        }

        // Splits path into parts
        public static string[] parsePath(string path)
        {
            return path.Split(['\\'], StringSplitOptions.RemoveEmptyEntries);
        }

        // Gets contents of a directory by path (null if not found)
        public (List<vFile> files, List<vDir> dirs)? getDirContents(string path)
        {
            string[] parts = parsePath(resolvePath(path));

            if (parts.Length == 0)
            {
                return (rootFiles, rootDirs);
            }

            List<vDir> currentDirs = rootDirs;
            for (int i = 0; i < parts.Length; i++)
            {
                int idx = -1;
                for (int j = 0; j < currentDirs.Count; j++)
                {
                    if (currentDirs[j].name == parts[i])
                    {
                        idx = j;
                        break;
                    }
                }
                if (idx == -1)
                {
                    return null;
                }

                if (i == parts.Length - 1)
                {
                    return (currentDirs[idx].files, currentDirs[idx].subDirs);
                }

                currentDirs = currentDirs[idx].subDirs;
            }
            return null;
        }

        // Gets parent directory contents + target name
        public (List<vFile> files, List<vDir> dirs, string name)? getParent(string path)
        {
            string[] parts = parsePath(resolvePath(path));
            if (parts.Length == 0)
            {
                return null;
            }

            string targetName = parts[^1];

            if (parts.Length == 1)
            {
                return (rootFiles, rootDirs, targetName);
            }

            string[] parentParts = new string[parts.Length - 1];
            Array.Copy(parts, parentParts, parts.Length - 1);
            string parentPath = "\\" + string.Join("\\", parentParts);

            (List<vFile> files, List<vDir> dirs)? parent = getDirContents(parentPath);
            if (parent == null)
            {
                return null;
            }

            return (parent.Value.files, parent.Value.dirs, targetName);
        }

        //=== PUBLIC FUNCTIONS ===

        /// <summary>
        /// Changes working directory. Supports ".." to go up.
        /// </summary>
        public bool changeDir(string path)
        {
            if (path == "\\")
            {
                workingPath = "\\";
                return true;
            }

            if (path == "..")
            {
                string[] parts = parsePath(workingPath);
                if (parts.Length == 0)
                {
                    return false;
                }

                if (parts.Length == 1)
                {
                    workingPath = "\\";
                }
                else
                {
                    string[] newParts = new string[parts.Length - 1];
                    Array.Copy(parts, newParts, parts.Length - 1);
                    workingPath = "\\" + string.Join("\\", newParts);
                }
                return true;
            }

            if (getDirContents(path) != null)
            {
                workingPath = resolvePath(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a directory. Returns false if parent doesn't exist or already exists.
        /// </summary>
        public bool mkDir(string path, fileAttrib[]? attribs = null)
        {
            (List<vFile> files, List<vDir> dirs, string name)? result = getParent(path);
            if (result == null)
            {
                return false;
            }

            (List<vFile> _, List<vDir> dirs, string name) = result.Value;

            for (int i = 0; i < dirs.Count; i++)
            {
                if (dirs[i].name == name)
                {
                    return false;
                }
            }

            dirs.Add(new vDir(name, [], [], attribs ?? []));
            return true;
        }

        /// <summary>
        /// Creates a file. Returns false if parent doesn't exist or file already exists.
        /// </summary>
        public bool mkFile(string path, byte[]? contents = null, fileAttrib[]? attribs = null)
        {
            (List<vFile> files, List<vDir> dirs, string name)? result = getParent(path);
            if (result == null)
            {
                return false;
            }

            (List<vFile> files, List<vDir> _, string name) = result.Value;

            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].name == name)
                {
                    return false;
                }
            }

            files.Add(new vFile(name, contents ?? [], attribs ?? []));
            return true;
        }
        public bool mkFileChr(string path, char[]? contents = null, fileAttrib[]? attribs = null)
        {
            List<byte> tempBytes = [];
            foreach (char item in contents ?? [])
            {
                tempBytes.Add((byte)item);
            }
            byte[] ctBytes = [.. tempBytes];
            (List<vFile> files, List<vDir> dirs, string name)? result = getParent(path);
            if (result == null)
            {
                return false;
            }

            (List<vFile> files, List<vDir> _, string name) = result.Value;

            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].name == name)
                {
                    return false;
                }
            }

            files.Add(new vFile(name, ctBytes ?? [], attribs ?? []));
            return true;
        }

        /// <summary>
        /// Deletes a file. Returns false if not found.
        /// </summary>
        public bool delFile(string path)
        {
            (List<vFile> files, List<vDir> dirs, string name)? result = getParent(path);
            if (result == null)
            {
                return false;
            }

            (List<vFile> files, List<vDir> _, string name) = result.Value;

            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].name == name)
                {
                    files.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Deletes a directory (and everything inside). Returns false if not found.
        /// </summary>
        public bool delDir(string path)
        {
            (List<vFile> files, List<vDir> dirs, string name)? result = getParent(path);
            if (result == null)
            {
                return false;
            }

            (List<vFile> _, List<vDir> dirs, string name) = result.Value;

            for (int i = 0; i < dirs.Count; i++)
            {
                if (dirs[i].name == name)
                {
                    dirs.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }

    public struct vFile(string nm, byte[] ct, fileAttrib[] at)
    {
        public string name = nm;
        public byte[] contents = ct;
        public fileAttrib[] attribs = at;
    }

    public struct vDir(string nm, List<vFile> fls, List<vDir> subdirs, fileAttrib[] attr)
    {
        public string name = nm;
        public List<vFile> files = fls;
        public List<vDir> subDirs = subdirs;  // this shit is needed
        public fileAttrib[] attribs = attr;
    }

    public enum fileAttrib
    {
        None = 0,
        Hidden = 0b00000001,
        System = 0b00000010,
        Readonly = 0b00000100,
        bktRs1 = 0b00001000,
        bktRs2 = 0b00010000,
        bktRs3 = 0b00100000,
        bktRs4 = 0b01000000,
        bktRs5 = 0b10000000,
    }
}