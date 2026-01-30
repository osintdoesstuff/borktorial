using System.Text.Json;

namespace aperture
{
    // this is AI slop code but it WORKS
    public class fileSys
    {
        public List<vFile> rootFiles = new(65536);
        public List<vDir> rootDirs = new(65536);
        public string workingPath = "\\";

        //=== SERIALIZATION ===

        private static readonly JsonSerializerOptions jsonOpts = new()
        {
            IncludeFields = true,
            WriteIndented = true  // remove this if you want compact JSON
        };

        /// <summary>
        /// Serialize to JSON string
        /// </summary>
        public string ToJson()
        {
            var data = new fsData
            {
                workingPath = this.workingPath,
                rootFiles = this.rootFiles,
                rootDirs = this.rootDirs
            };
            return JsonSerializer.Serialize(data, jsonOpts);
        }

        /// <summary>
        /// Load from JSON string
        /// </summary>
        public static fileSys FromJson(string json)
        {
            var data = JsonSerializer.Deserialize<fsData>(json, jsonOpts);
            return new fileSys
            {
                workingPath = data.workingPath ?? "\\",
                rootFiles = data.rootFiles ?? new List<vFile>(),
                rootDirs = data.rootDirs ?? new List<vDir>()
            };
        }

        /// <summary>
        /// Serialize to binary blob
        /// </summary>
        public byte[] ToBinary()
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write(workingPath);
            WriteFileList(bw, rootFiles);
            WriteDirList(bw, rootDirs);

            return ms.ToArray();
        }

        /// <summary>
        /// Load from binary blob
        /// </summary>
        public static fileSys FromBinary(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            var fs = new fileSys();
            fs.workingPath = br.ReadString();
            fs.rootFiles = ReadFileList(br);
            fs.rootDirs = ReadDirList(br);

            return fs;
        }

        //=== BINARY HELPERS ===

        private static void WriteFileList(BinaryWriter bw, List<vFile> files)
        {
            bw.Write(files.Count);
            foreach (var f in files)
            {
                bw.Write(f.name);
                bw.Write(f.contents.Length);
                bw.Write(f.contents);
                bw.Write(f.attribs.Length);
                foreach (var a in f.attribs)
                    bw.Write((int)a);
            }
        }

        private static void WriteDirList(BinaryWriter bw, List<vDir> dirs)
        {
            bw.Write(dirs.Count);
            foreach (var d in dirs)
            {
                bw.Write(d.name);
                bw.Write(d.attribs.Length);
                foreach (var a in d.attribs)
                    bw.Write((int)a);
                WriteFileList(bw, d.files);
                WriteDirList(bw, d.subDirs);  // recursive
            }
        }

        private static List<vFile> ReadFileList(BinaryReader br)
        {
            int count = br.ReadInt32();
            var files = new List<vFile>(count);
            for (int i = 0; i < count; i++)
            {
                string name = br.ReadString();
                int contentLen = br.ReadInt32();
                byte[] contents = br.ReadBytes(contentLen);
                int attrLen = br.ReadInt32();
                var attribs = new attrib[attrLen];
                for (int j = 0; j < attrLen; j++)
                    attribs[j] = (attrib)br.ReadInt32();
                files.Add(new vFile(name, contents, attribs));
            }
            return files;
        }

        private static List<vDir> ReadDirList(BinaryReader br)
        {
            int count = br.ReadInt32();
            var dirs = new List<vDir>(count);
            for (int i = 0; i < count; i++)
            {
                string name = br.ReadString();
                int attrLen = br.ReadInt32();
                var attribs = new attrib[attrLen];
                for (int j = 0; j < attrLen; j++)
                    attribs[j] = (attrib)br.ReadInt32();
                var files = ReadFileList(br);
                var subDirs = ReadDirList(br);  // recursive
                dirs.Add(new vDir(name, files, subDirs, attribs));
            }
            return dirs;
        }

        // Helper struct for JSON (cleaner serialization)
        private struct fsData
        {
            public string workingPath;
            public List<vFile> rootFiles;
            public List<vDir> rootDirs;
        }

        //=== PRIVATE HELPERS ===

        // Converts relative path to absolute
        private string ResolvePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return workingPath;
            if (path.StartsWith("\\")) return path;
            if (workingPath == "\\") return "\\" + path;
            return workingPath + "\\" + path;
        }

        // Splits path into parts
        private string[] ParsePath(string path)
        {
            return path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
        }

        // Gets contents of a directory by path (null if not found)
        public (List<vFile> files, List<vDir> dirs)? GetDirContents(string path)
        {
            string[] parts = ParsePath(ResolvePath(path));

            if (parts.Length == 0)
                return (rootFiles, rootDirs);

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
                if (idx == -1) return null;

                if (i == parts.Length - 1)
                    return (currentDirs[idx].files, currentDirs[idx].subDirs);

                currentDirs = currentDirs[idx].subDirs;
            }
            return null;
        }

        // Gets parent directory contents + target name
        private (List<vFile> files, List<vDir> dirs, string name)? GetParent(string path)
        {
            string[] parts = ParsePath(ResolvePath(path));
            if (parts.Length == 0) return null;

            string targetName = parts[parts.Length - 1];

            if (parts.Length == 1)
                return (rootFiles, rootDirs, targetName);

            string[] parentParts = new string[parts.Length - 1];
            Array.Copy(parts, parentParts, parts.Length - 1);
            string parentPath = "\\" + string.Join("\\", parentParts);

            var parent = GetDirContents(parentPath);
            if (parent == null) return null;

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
                string[] parts = ParsePath(workingPath);
                if (parts.Length == 0) return false;

                if (parts.Length == 1)
                    workingPath = "\\";
                else
                {
                    string[] newParts = new string[parts.Length - 1];
                    Array.Copy(parts, newParts, parts.Length - 1);
                    workingPath = "\\" + string.Join("\\", newParts);
                }
                return true;
            }

            if (GetDirContents(path) != null)
            {
                workingPath = ResolvePath(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a directory. Returns false if parent doesn't exist or already exists.
        /// </summary>
        public bool mkDir(string path, attrib[] attribs = null)
        {
            var result = GetParent(path);
            if (result == null) return false;

            var (_, dirs, name) = result.Value;

            for (int i = 0; i < dirs.Count; i++)
                if (dirs[i].name == name) return false;

            dirs.Add(new vDir(name, new List<vFile>(), new List<vDir>(), attribs ?? Array.Empty<attrib>()));
            return true;
        }

        /// <summary>
        /// Creates a file. Returns false if parent doesn't exist or file already exists.
        /// </summary>
        public bool mkFile(string path, byte[] contents = null, attrib[] attribs = null)
        {
            var result = GetParent(path);
            if (result == null) return false;

            var (files, _, name) = result.Value;

            for (int i = 0; i < files.Count; i++)
                if (files[i].name == name) return false;

            files.Add(new vFile(name, contents ?? Array.Empty<byte>(), attribs ?? Array.Empty<attrib>()));
            return true;
        }

        /// <summary>
        /// Deletes a file. Returns false if not found.
        /// </summary>
        public bool delFile(string path)
        {
            var result = GetParent(path);
            if (result == null) return false;

            var (files, _, name) = result.Value;

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
            var result = GetParent(path);
            if (result == null) return false;

            var (_, dirs, name) = result.Value;

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
        public vDir(string nm, List<vFile> fls, List<vDir> subdirs, attrib[] attr)
        {
            name = nm;
            files = fls;
            subDirs = subdirs;
            attribs = attr;
        }
        public string name;
        public List<vFile> files;
        public List<vDir> subDirs;  // <-- ADDED THIS
        public attrib[] attribs;
    }

    public enum attrib
    {
        None,
        Hidden,
        System,
        Readonly,
        bktRs1,
        bktRs2,
        bktRs3,
        bktRs4,
        bktRs5,
        bktRs6,
        bktRs7
    }
}