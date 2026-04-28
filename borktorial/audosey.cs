using System.Media;
using System.Threading.Tasks;

namespace borktorial
{
    /// <summary>
    /// Very dumb audio system
    /// The name comes from a even dumber pun (get it? aud-osey? as in audio-osey? audio-oddesy? yeah it makes less sense the more i look at it)
    /// </summary>
    public static class audosey
    {
        public static List<SoundPlayer> sources = [];
        public static void initAud(int srcCount)
        {
            foreach (SoundPlayer item in sources)
            {
                item.Stop();
            }
            sources.Clear();
            for (int i = 0; i < srcCount; i++)
            {
                sources.Add(new());
            }
        }
        public static bool loadAud(Stream loc, int source)
        {
            try
            {
                sources[source].SoundLocation = "";
                sources[source].Stream = loc;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool loadAud(string loc, int source)
        {
            try
            {
                sources[source].Stream = null;
                sources[source].SoundLocation = loc;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool play(int source, bool sync=false)
        {
            try
            {
                if (!sync)
                {
                    sources[source].Play();
                }
                else
                {
                    sources[source].PlaySync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool stop(int source)
        {
            try
            {
                sources[source].Stop();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool stopAll()
        {
            try
            {
                foreach (SoundPlayer item in sources)
                {
                    item.Stop();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
