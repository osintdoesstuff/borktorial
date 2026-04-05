using aperture;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.FileIO;
using NAudio.Wave;
using NLua;
using Spectre.Console;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
namespace borktorial
{
    public class bktMain
    {
        public static string cat { get; } = """
             |\\_,-~/
             / _  _ |    ,--.
            (  @  @ )   / ,-'
             \  _T_/-._( (
             /         `. \
            |         _  \ |
             \ \ ,  /      |
              || |-_\__   /
             ((_/`(____,-'
            """;

        public static int bktver { get; set; } = 1125;
        public static bool jebconnect { get; set; } = false;
        public static bool mConnected { get; set; } = false;
        public static bool forceNoBoot { get; set; } = false;
        public static bool virused { get; set; } = false;
        public static bool ballmerMode { get; set; } = false;
        public static bool gordonSummoned { get; set; } = File.Exists("GORDON");
        public static bool radioStopped { get; set; } = true;
        public static bool jmtrigger { get; set; } = false;
        public static bool root { get; set; } = false;
        public static bool s5a85 { get; } = OperatingSystem.IsWindows();
        public static int mSpeed { get; set; } = 1800;
        public static int crshChance { get; set; } = 10000;
        public static int jebcounter { get; set; } = 0;
        public static double tick { get; set; } = 0;
        public static double munCycle { get; set; } = 0;
        public static double schonite { get; set; } = 1;
        public static double sysstab { get; set; } = 1;
        public static mConnectTypes mCt { get; set; } = mConnectTypes.Null;
        public static Random rand { get; set; } = new();
        public static Thread? drdhtsr { get; set; }
        public static ComputerInfo compi { get; set; } = new(); // Was readonly, but object state is mutable
        public static fileSys fs { get; set; } = new();
        public static int giftCount { get; set; } = 5;
        public static bool rbt0 { get; set; } = false;
        public static string cfgFn { get; set; } = "bktcfg.ssc";
        public static string username { get; set; } = "";
        public static string password { get; set; } = "";
        public static int iRnd { get; set; } = rand.Next(0, 12); // 1 in 13
        public static byte cmdFErrCount { get; set; } = 0;
        public static List<string> currNews { get; set; } = [];
        public static bool spoopMode { get; set; } = true;
        public static int[] cfg { get; set; } = [15, 10000, 15, 2];
        public static int rSeed { get; set; } = (int)(DateTime.UtcNow.Ticks + strSum(aprtMain.nookEnc(username, password)));
        public static string[] lines { get; } = [
            "Gordon doesn't need to hear this, he's a highly trained professional!",
            "Good morning and welcome to the Black Mesa Transit System.",
            "Wisely done, Mr. Freeman",
            "About that beer i owe ya",
            "We have assured the Administrator that nothing will go wrong",
            "Tell me, Dr. Freeman, if you can. You have destroyed so much. What is it, exactly, that you have created? Can you name even one thing? I thought not.",
            "Pick up that can",
            "Man of few words, aren't you?",
            "Hello! This is the part where i kill you!",
            "That is not a panel. That's a crusher. We sell them too.",
            "Cave Johnson here. Introducing the consumer version of our most popular military-grade product: the turret.",
            "Gentlemen, I give you the Long Fall Boot. Think of it as foot-based suit of armor for the Portal Device. I'm not gonna lie to you, it's expensive as hell. But check this out: we told this Test Subject to just go ahead and try to land on her head. Heh heh! She can't do it! Good work, boots.",
            "Science isn't about WHY. It's about WHY NOT. Why is so much of our science dangerous? Why not marry safe science if you love it so much. In fact, why not invent a special safety door that won't hit you on the butt on the way out, because you are fired.",
            "Dr. Freeman to Anomalous Materials test laboratory immediately."
        ];
        public static string[] linesAttr { get; } = [
            "-Cave Johnson",
            "-G-man",
            "-Dr. Breen",
            "-Alyx Vance",
            "-Fucking Wheatley",
            "-Socrates",
            "-Aristotle",
            "-Sun Tzu"
        ];
        public static string[] linesBooks { get; } = [
            "How to fire test subjects",
            "How to ruin a science lab",
            "How to blame Black Mesa for issues you've had",
            "On being a mysterious character",
            "How to hire Gordon Freeman's",
            "How to be a silent character",
            "How to defeat the Combine with a crowbar",
            "How to be a side character featured mainly in 1 VR game",
            "How to Be a Moron: The Definitive Guide",
            "On the Art of Blowing Shit Up",
            "How to blame Aperture Science for issues you've had"
        ];

        public static string jebMsg { get; } = """
            Jebediah Kerman did not die
            He survived the Shitfuck 15 mission.
            Press K to celebrate.
            Props to Jeb.
            Good job.

            Also, i like bacon-flavored Shapez. Maybe you could use that for a command?
            """;
        public static string sysspecs => $"""
            CPU: Intel 486DX C-Step@50MHz
            RAM: 640KB conventional, 384KB shadow, 15360KB extended
            Drives: A: (720KB FD), B: (720KB FD), C: (os drive, 614400KB)
            OS: NTOSKRNL v4.3, NT-DOS v2.2, running on Console Subsystem.
            Video: Citrus GT-6500 ISA
            Sound: PC beeper, SB1.0
            Network: Networked Microsystems 14400bps. Connected: {mConnected}
            Unknown: STANDARD ISA16 PERIPHERAL hooked onto int 5Fh.
            """;
        public static string luaReadme => """
            Borktorial mods folder.

            You can put normal Lua in here, run the name of the file as a command (e.g "test.lua" would mean the command "test" would correspond to that), and it'll run it as if it was a command!

            The important variables:

            Sys: table describing everything in the Program class
            Args: what args said command was run with
            ArgsRaw: The raw args
            ArgsNoSkip: The args with commin[0] not skipped
            ArgsRawNoSkip: The raw args with commin[0] not skipped

            initmods.lua runs at boot time to initialize mods.

            CLDMSG.TXT:

            This is custom load messages (the ones you see like "Insulting Dr. Breen..." and shit like that that appear after the BIOS screen)
            Syntax:

            Normal line: load message
            if line 0 equals "[NOSTOCKLDMSG]": remove all stock loading messages (it must equal this EXACTLY!)
            if line begins with "//": It's a comment and will be ignored
            if line begins with "[REMOVE] ": remove a stock load message

            CSPLASH.TXT:
            This is custom splash texts (the ones you see in the titlebar)

            Weights (have to start any splash):

            "(c) ": Common
            "(u) ": Uncommon
            "(r) ": Rare
            "(e) ": Comment
            "(m) ": Only on marsDay/spaceDay
            "(s) ": Only on Snapshot Day (wednesday)
            Other tag or no tag: Comment
            Note that it MUST be like this. All splashes must begin with this

            Syntax:

            normal line: This is a splash
            if line 0 equals "[NOSTOCKSPLASH]": remove all stock splashes (it must equal this EXACTLY!). This does not need a tag
            if line begins with "//": it's a comment and will be ignored. This does not NEED a tag but usually commented out splashes will have tags
            if line begins with "[REMOVE] ": remove a specific stock splash (must have the tag in the bit after the "[REMOVE] " bit).

            To find out which specific stock splashes have which tags, you can check the file that contains them all, assets\splashes.txt (In the same folder you put the EXE). If you want, you can even just modify this file direct (not recommended)
            That's basically it. Read the fucking source code
            """;
        public static List<(string cmd1, string[] cmd2)> aliases { get; set; } = [];
        public static List<(string username, string command, string message, string sid)> usedGifts = [];
        public static void publicMain(string[] mArgs)
        {
            if (virused)
            {
                mArgs = [.. mArgs, "__virused"];
            }
            resetState();
            Main(mArgs);
        }
        static void Main(string[] args)
        {
            if (rbt0)
            {
                Thread.Sleep(200); // wait for everything to settle the fuck down
                rbt0 = false;
            }
            Console.Title = $"borktorial: {splashPick()}";
            if (rand.Next(0, 69) == 0) // 1 in 69
            {
                Console.Title = $"broktorial: {splashPick()}";
                if (rand.Next(0, 42+File.ReadAllLines("assets\\splashes.txt").Length) == 0)
                {
                    spoopMode = true;
                }
            }
            // hide the init time away
            AnsiConsole.MarkupLine("[rgb(255,255,0)]Citrus[/] Emerald Sneak VGA BIOS...");
            Thread.Sleep(2000);
            AnsiConsole.MarkupLine("8192KB [green]OK[/]");
            AnsiConsole.MarkupLine("Card: [rgb(255,255,0)]Citrus[/] GT-6500 ISA");
            AnsiConsole.MarkupLine("Modes: CGA (T), CGA (G), EGA (T), EGA (G), VGA (T), VGA (G), [rgb(255,255,0)]Citrus[/] extensions");
            AnsiConsole.MarkupLine("");
            if (!Directory.Exists("mods"))
            {
                Directory.CreateDirectory("mods");
                File.WriteAllText(Path.Combine("mods", "initmods.lua"), "");
                File.WriteAllText(Path.Combine("mods", "README.TXT"), luaReadme);
            }
            Thread.Sleep(5000);
            if (aprtMain.aprtVer != 465)
            {
                shitLog.createEntry("BOOT", $"APRT version mismatch. Expected 0.4.4a, got {aprtMain.aprtVer}", logType.Warn);
            }
            shitLog.createEntry("BOOT", $"Random seed is 0x{rSeed:X8}", logType.Info);
            impulse(5000);
            Stopwatch bootSw = new();
            bootSw.Start();
            Debug.WriteLine("tada!");
            if (args.Length >= 2 && args[0] == "bktint:delayStart")
            {
                Thread.Sleep(int.Parse(args[1]));
            }
            if (!s5a85)
            {
                rand = new Random(0x4E54);
            }
            impulse(5000);
            impulse(5001);
            impulse(5002);
            try
            {
                if (File.Exists(cfgFn)) // Semicolon Separated Config
                {
                    shitLog.createEntry("CFGLDR", $"Loading {cfgFn}...", logType.Info);
                    string configC = File.ReadAllText(cfgFn);
                    string[] cfgR = configC.Split(";");
                    int[] cfgP = new int[256];
                    int iteration = 0;
                    foreach (string item in cfgR)
                    {
                        int itemI = int.Parse(item);
                        cfgP[iteration] = itemI;
                        iteration++;
                    }
                    cfg = cfgP;
                    if (cfg[3] != getBuildNum())
                    {
                        shitLog.createEntry("CFGLDR", $"{cfgFn} has wrong version", logType.Warn);
                    }
                    shitLog.createEntry("CFGLDR", "Config loading success!", logType.Info);
                }
                else
                {
                    shitLog.createEntry("CFGLDR", "No config found! Using default!", logType.Warn);
                    forceDefaultCfg();
                }
            }
            catch (Exception ex)
            {
                shitLog.createEntry("CFGLDR", $"Config error: {ex.Message} {ex.StackTrace}", logType.Err);
                forceDefaultCfg();
            }

            if (args.Length >= 1 && args[0] == "/waluigi")
            {
                sf59("waluigi");
            }
            if (args.Length >= 1 && args[0] == "/igiulaw")
            {
                sf59("igiulaw");
            }
            if (args.Length >= 1 && args[0] == "/version")
            {
                Console.WriteLine($"Borktorial verison {getBuildNum()}");
                return;
            }
            if (args.Length >= 1 && args[0] == "/dev")
            {
                Console.WriteLine("DEVELOPERS DEVELOPERS DEVELOPERS\r\n" +
                    "-Steve Ballmer while running around on stage\r\n" +
                    "actively sweating and probably\r\n" +
                    "needing vocal cord surgery");
                ballmerMode = true;
                if (args.Length >= 2 && string.Join(' ', args).Contains("FORCENOBOOT"))
                {
                    forceNoBoot = true;
                }
                if (args.Length >= 2 && string.Join(' ', args).Contains("FORCEMODEM"))
                {
                    mSpeed = 8192;
                    mConnected = true;
                    mCt = mConnectTypes.dbg;
                }
                if (args.Length >= 2 && string.Join(' ', args).Contains("NOBMPAR"))
                {
                    ballmerMode = false;
                }
                if (args.Length >= 2 && string.Join(' ', args).Contains("FORCEGORDON"))
                {
                    gordonSummoned = true;
                }
                if (args.Length >= 2 && string.Join(' ', args).Contains("HALTANDCATCHFIRE"))
                {
                    Exception inException1 = new("A");
                    Exception inException2 = new("B", inException1);
                    Exception inException3 = new("C", inException2);
                    throw new Exception("D", inException3);
                }
            }
            if (!forceNoBoot)
            {
                if (!ballmerMode)
                {
                    Console.Clear();
                }
                bootSw.Stop();
                if (File.Exists(Path.Combine("mods", "initmods.lua")))
                {
                    using NLua.Lua lua = new();
                    lua.LoadCLRPackage();

                    try
                    {
                        string? luAsm = Assembly.GetExecutingAssembly().GetName().Name;

                        string? lut = typeof(bktMain).FullName;

                        lua.DoString($@"
                                            luanet.load_assembly('{luAsm}')
                                            Sys = luanet.import_type('{lut}')
                                        ");

                        if (lua["Sys"] == null)
                        {
                            shitLog.createEntry("LUALDR", "Failed to load ASM.", logType.Err);
                        }
                        else
                        {
                            lua.DoFile(Path.Combine("mods", "initmods.lua"));
                        }
                    }
                    catch (Exception ex)
                    {
                        shitLog.createEntry("LUALDR", ex.ToString(), logType.Err);
                        throw;
                    }
                }
                else
                {
                    File.WriteAllText(Path.Combine("mods", "initmods.lua"), "");
                }
                shitLog.createEntry("BOOT", $"Init took {bootSw.ElapsedMilliseconds}ms!", logType.Info);
                Console.Clear();
                AnsiConsole.MarkupLine($"[bold][green]Ker[/]BIOS[/] 3.14 Revision 159 (build {getBuildNum()})");
                AnsiConsole.MarkupLine("(C) [lime]KSC[/] Computer Division 1987-1994");
                Console.WriteLine();
                Console.Write("Memory test...");
                if (args.Length >= 2 && args[0] == "vs" && args[1] == "49")
                {
                    Thread.Sleep(800);
                    AnsiConsole.Markup("16384kb [green]ok[/]\r\n");
                }
                else
                {
                    Thread.Sleep(2000);
                    AnsiConsole.Markup("16384kb [green]ok[/]\r\n");
                }
                AnsiConsole.MarkupLine("Press [white]F11[/] to enter SETUP...");
                Thread.Sleep(3000);
                Console.Write("Primary Master...");
                Thread.Sleep(500);
                Console.Write("Landgate Xtreme ATA Drive [4096MB]\r\n");
                Thread.Sleep(rand.Next(500, 1000));
                Console.Write("Primary Slave...");
                Thread.Sleep(500);
                Console.Write("Pholops D.I.C.K 8x XD-ROM drive\r\n");
                Thread.Sleep(rand.Next(500, 1000));
                Console.Write("Secondary Master...");
                Thread.Sleep(500);
                Console.Write("None\r\n");
                Thread.Sleep(rand.Next(500, 1000));
                Console.Write("Secondary Slave...");
                Thread.Sleep(500);
                Console.Write("None\r\n\r\n");
                Thread.Sleep(rand.Next(500, 1000));
                Console.Write("Booting from FDD...");
                Thread.Sleep(rand.Next(500, 1000));
                AnsiConsole.Markup("[red]fail[/]\r\n");
                Thread.Sleep(rand.Next(500, 750));
                Console.Write("Booting from CD-ROM...");
                Thread.Sleep(rand.Next(500, 1000));
                AnsiConsole.Markup("[red]fail[/]\r\n");
                Thread.Sleep(rand.Next(500, 750));
                Console.Write("Booting from HDD...");
                Thread.Sleep(rand.Next(250, 750));
                if (args.Contains("__virused") || args.Contains("frmt"))
                {
                    AnsiConsole.Markup("[red]fail[/]\r\n\r\n");
                    AnsiConsole.Markup("No boot devices found. F1 to reboot.\r\n");
                    while (true)
                    {
                        ConsoleKey ck = Console.ReadKey().Key;
                        if (ck == ConsoleKey.F1)
                        {
                            publicMain(args);
                        }
                    }
                }
                AnsiConsole.Markup("[green]ok![/]\r\n");

                Console.WriteLine("\r\nStarting NT-DOS...\r\n");
                Thread.Sleep(4500);
                Console.WriteLine("NTXMEM is checking extended memory...\r\n");
                if (compi.AvailablePhysicalMemory < 16777216 || !OperatingSystem.IsWindows())
                {
                    Console.WriteLine("NT-DOS requires at least 16MB of extended memory.");
                    while (true)
                    {
                        Thread.Sleep(int.MaxValue);
                    }
                }
                Thread.Sleep(1250);
                if (gordonSummoned || (!s5a85 && rand.Next(1, 5) == 0))
                {
                    Console.WriteLine("[WARN] 128 byte memory hole detected at 0x8086!");
                    Thread.Sleep(500);
                    keBugCheck(0xBD31052, new(1995, 12, 31, 12, 59, 59, 999, 999));
                }
                int[] wrongCfg =
                                ['K', 'E', 'R', 'B', 'A', 'L',
                                'S', 'P', 'A', 'C', 'E',
                                'C', 'E', 'N', 'T', 'E', 'R'];
                if (cfg == wrongCfg)
                {
                    keBugCheck(0xDEADBABE, new DateTime(1956, 10, 4));
                }
                Thread.Sleep(1250);
                if (cfg[3] == 1)
                {
                    Console.WriteLine("CRITICAL: Cannot find NTGINA.DLL. System halted");
                    while (true)
                    {
                        Thread.Sleep(int.MaxValue);
                    }
                }
                string[] loadMsgs = [
                    "Processing...",
                    "Doing big math...",
                    "Importing processing framework...",
                    "Waiting for HL3...",
                    "Stealing gmod loading screen ideas...",
                    "Wasting time...",
                    "Doing nothing...",
                    "Playing KSP...",
                    "i use WinNT btw",
                    "Wasting your time...",
                    "Loading...",
                    "Welcome to Zombo.com",
                    "Stealing Gordon Freeman's crowbar...",
                    "Participating in the 7-hour war...",
                    "Getting colonized by the British...",
                    "Doing small math...",
                    "EMERGENCY: THERE IS A FIRE AT THE WALRUS FACTORY!!!",
                    "Settling Arguement of Periapsis...",
                    "Merging into `main`...",
                    "Sending Val to Vall...",
                    "Doing important things...",
                    "Slapping 'AI' onto everything...",
                    "Trick XOR treating...",
                    "Piss.",
                    "Coupling engines...",
                    "Decoupling decoupler...",
                    "Installing KSRSS...",
                    "Calculating friction coefficient of sand...",
                    "Calling all stations...",
                    "Releasing HL3...",
                    "Installing Aperture Science Advanced Multi-Iteration Addition Processors (known in some circles as 'multipliers')",
                    "Discarding Zen of Python...",
                    "Breathing oxygen...",
                    "Segmentation fault.",
                    "Insulting Dr. Breen...",
                    "Re-entering atmosphere...",
                    "Fine-tuning universal constants...",
                    "Stabilizing Higgs Field...",
                    "Running away from true vacuum...",
                    "Adding more hydrogen...",
                    "Tuning matter-antimatter ratio...",
                    "Adding moar boosters..."
                    ];
                if (File.Exists(Path.Combine("mods", "cldmsg.txt")))
                {
                    List<string> moreLines = [.. File.ReadAllLines(Path.Combine("mods", "cldmsg.txt"))];
                    List<string> fullLines = [.. loadMsgs];
                    if (moreLines.Count > 0 && moreLines[0] == "[NOSTOCKLDMSG]")
                    {
                        fullLines = [];
                    }
                    foreach (string item in moreLines)
                    {
                        if (item.StartsWith("[REMOVE] "))
                        {
                            fullLines.Remove(item[9..]);
                            fullLines.Remove(item); // just to be surely sure
                            continue; // skip it
                        }
                        if (item.StartsWith("//"))
                        {
                            continue; // skip this too
                        }
                        fullLines.Add(item);
                    }
                    loadMsgs = [.. fullLines];
                }
                if (cfg[5] == 0)
                {
                    for (int i = 0; i < rand.Next(5, 16); i++)
                    {
                        Console.Clear();
                        Console.WriteLine(loadMsgs[rand.Next(0, loadMsgs.Length)]);
                        Thread.Sleep(rand.Next(500, 801));
                    }
                }
                Console.Clear();
            }
            if (cfg[6] == 0)
            {
                while (string.IsNullOrWhiteSpace(username))
                {
                    Console.Write("Username (\"<default>\" to generate one): ");
                    username = Console.ReadLine()?.Trim() ?? "";
                    if (username == "<default>")
                    {
                        username = aprtMain.mkShitUsername(rand);
                    }
                }

                while (string.IsNullOrWhiteSpace(password))
                {
                    Console.Write("Password: ");
                    password = Console.ReadLine() ?? "";
                }
                if (username == "SYSTEM" && rand.Next(0, 37) == 0)
                {
                    root = true;
                }
            }
            else
            {
                username = aprtMain.mkShitUsername(rand);
                password = aprtMain.genHexStr(16);
            }
            Thread timeThread = new(() =>
            {
                timeLoop(cfg[0], cfg[1]);
            });
            timeThread.Start();
            if (!s5a85 || specialDays.aprilfool)
            {
                Thread s58858g = new(s49291);
                s58858g.Start();
            }
            Thread msVarier = new(interspeed);
            msVarier.Start();
            Console.WriteLine("NT-DOS is loading shell \"TW8000.EXE\"...");
            Thread.Sleep(rand.Next(750, 1500));
            Console.WriteLine("\r\nWelcome to the Time-Waster 8000!");
            if (specialDays.bktDay)
            {
                Console.WriteLine("Happy Borktorial Day!");
            }
            // initialize news feed
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    addNews(newsGen.generateNws());
                }
            }
            catch (IndexOutOfRangeException) { currNews.Clear(); }
            impulse(5002);
            rand = new(rSeed);

            // note: Ctrl+C being input somehow makes this break. i dunno how.
            // i don't wanna KNOW how
            // Console.TreatControlCAsInput = true;
            while (true)
            {
                Console.Write($"C:{fs.workingPath}>");
                string rawCommin = Console.ReadLine() ?? "";
                try
                {
                    rawCommin = parseBorkTag(rawCommin);
                }
                catch (Exception ex)
                {
                    shitLog.createEntry("cmdhndlr", ex.ToString(), logType.Warn);
                    Console.WriteLine("Command Parser Error 49 (pbt fail)");
                    continue;
                }
                string[] commin = rawCommin.ToLower().Split(' ');
                if (commin.Length > 0)
                {
                    for (int i = 0; i < aliases.Count; i++)
                    {
                        (string cmd1, string[] cmd2) = aliases[i];
                        if (commin[0] == cmd1)
                        {
                            commin = cmd2;
                            rawCommin = string.Join(' ', cmd2);
                        }
                    }
                }
                if (strSum(rawCommin) % 8 == 0 && strSum(rawCommin) > 0)
                {
                    if (rand.Next(0, 12 + cmdFErrCount) == 0)
                    {
                        Console.WriteLine("Error: failed to execute command");
                        commin = ["\xDE\xAD\xBA\xBE_bktignorecmd::0", "cmdFErr"];
                        cmdFErrCount += 3; // gets rarer every time
                        if (cmdFErrCount > 12)
                        {
                            cmdFErrCount += (byte)(cmdFErrCount * 0.05);
                        }
                    }
                }
                try
                {
                    switch (commin[0])
                    {
                        case "\xDE\xAD\xBA\xBE_bktignorecmd::0":
                            try
                            {
                                if (commin.Length > 1)
                                {
                                    Thread.Sleep(int.Parse(commin[1]));
                                }
                            }
                            catch
                            {
                                break;
                            }
                            break;
                        case "echo":
                            if (commin.Length > 1)
                            {
                                Console.WriteLine(string.Join(" ", rawCommin.Split(' ').Skip(1)));
                            }

                            break;
                        case "quoteoftheday":
                            string quote = lines[rand.Next(0, lines.Length)];
                            string attr = linesAttr[rand.Next(0, linesAttr.Length)];
                            string qsrc = linesBooks[rand.Next(0, linesBooks.Length)];
                            Console.WriteLine(quote);
                            Console.WriteLine($"\r\n{attr}, {qsrc}");
                            if (quote == lines[8])
                            {
                                if (rand.Next(1, 1000) == 500)
                                {
                                    keBugCheck(0x391AB32, new(2009, 5, 15));
                                }
                            }
                            break;
                        case "hl2ep3":
                        case "hl3":
                            Console.WriteLine("HALF-LIFE 3 CONFIRMED");
                            break;
                        case "type":
                            if (commin.Length >= 2)
                            {
                                (List<vFile> files, List<vDir> dirs)? wpCons = fs.getDirContents(fs.workingPath);
                                if (wpCons == null)
                                {
                                    Console.WriteLine("Invalid path");
                                    break;
                                }
                                foreach (vFile f in wpCons.Value.files)
                                {
                                    if (f.name == rawCommin.Split(' ')[1])
                                    {
                                        Console.WriteLine(aprtMain.ba2Str(f.contents));
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Parameter error");
                            }
                            break;
                        case "dir":
                            Console.WriteLine($"Volume Serial Number is 4594-8435");
                            Console.WriteLine($"Directory listing of C:{fs.workingPath}");
                            Console.WriteLine();

                            (List<vFile> files, List<vDir> dirs)? dirContents = fs.getDirContents(fs.workingPath);
                            if (dirContents == null)
                            {
                                Console.WriteLine("Invalid path");
                                break;
                            }

                            foreach (vDir dir in dirContents.Value.dirs)
                            {
                                bool isntHidden = dir.attribs.Contains(fileAttrib.System) || dir.attribs.Contains(fileAttrib.Hidden);
                                if (!isntHidden)
                                {
                                    Console.WriteLine($"    <DIR>  {dir.name}");
                                }
                            }

                            foreach (vFile file in dirContents.Value.files)
                            {
                                bool isntHidden = file.attribs.Contains(fileAttrib.System) || file.attribs.Contains(fileAttrib.Hidden);
                                if (!isntHidden)
                                {
                                    Console.WriteLine($"    {file.name} - {file.contents.Length} bytes");
                                }
                            }

                            Console.WriteLine();
                            break;
                        case "cd":
                            if (rawCommin.Split(' ').Length < 2)
                            {
                                Console.WriteLine($"Current directory: {fs.workingPath}");
                            }
                            else
                            {
                                if (fs.changeDir(rawCommin.Split(' ')[1]))
                                {
                                    Console.WriteLine($"Changed to: {fs.workingPath}");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid path");
                                }
                            }
                            break;
                        case "create":
                            if (rawCommin.Split(' ').Length < 2)
                            {
                                Console.WriteLine("Usage: create <filename>");
                            }
                            else
                            {
                                if (fs.mkFile(rawCommin.Split(' ')[1]))
                                {
                                    Console.WriteLine($"Created: {rawCommin.Split(' ')[1]}");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to create file");
                                }
                            }
                            break;
                        case "del":
                            if (rawCommin.Split(' ').Length < 2)
                            {
                                Console.WriteLine("Usage: del <filename>");
                            }
                            else
                            {
                                if (fs.delFile(rawCommin.Split(' ')[1]))
                                {
                                    Console.WriteLine($"Deleted: {rawCommin.Split(' ')[1]}");
                                }
                                else
                                {
                                    Console.WriteLine("File not found");
                                }
                            }
                            break;

                        case "deltree":
                            if (rawCommin.Split(' ').Length < 2)
                            {
                                Console.WriteLine("Usage: deltree <directory>");
                            }
                            else
                            {
                                if (fs.delDir(rawCommin.Split(' ')[1]))
                                {
                                    Console.WriteLine($"Deleted directory tree: {rawCommin.Split(' ')[1]}");
                                }
                                else
                                {
                                    Console.WriteLine("Directory not found");
                                }
                            }
                            break;
                        case "cpp":
                            Console.WriteLine("Welcome to the Cookies++ interpreter!");
                            Console.WriteLine("+ to increment cookies\r\n" +
                                "- to decrement cookies\r\n" +
                                "g to print cookies val\r\n" +
                                "x to exit\r\n" +
                                "s to save\r\n" +
                                "l to load\r\n");
                            double cookies = 0;
                            bool stop = false;
                            while (!stop)
                            {
                                Console.Write("> ");
                                string cppI = Console.ReadLine() ?? ""
                                    .ToLowerInvariant()
                                    .Trim();
                                switch (cppI)
                                {
                                    case "+":
                                        cookies++;
                                        break;
                                    case "-":
                                        if (cookies < 0)
                                        {
                                            AnsiConsole.MarkupLine("[red]Error[/]: CKI001: Cookies cannot be negative");
                                            break;
                                        }
                                        cookies--;
                                        break;
                                    case "g":
                                        Console.WriteLine(cookies);
                                        break;
                                    case "x":
                                        stop = true;
                                        break;
                                    case "s":
                                        fs.mkFileChr("\\ck.dat", cookies.ToString().ToCharArray(), [fileAttrib.Hidden]);
                                        break;
                                    case "l":
                                        foreach (vFile item in fs.rootFiles)
                                        {
                                            if (item.name == "ck.dat")
                                            {
                                                cookies = double.Parse(item.contents);
                                            }
                                        }
                                        break;
                                    default:
                                        AnsiConsole.MarkupLine("[red]Error[/]: CKI002: Invalid statement");
                                        break;
                                }
                            }
                            break;
                        case "alias":
                            Console.WriteLine();
                            Console.Write("Enter alias name: ");
                            string aliasName = Console.ReadLine() ?? "";
                            if (aliasName.Contains(' '))
                            {
                                Console.WriteLine("Invalid alias name");
                                break;
                            }
                            Console.Write("Enter command for alias to refer to: ");
                            string tempCommand = Console.ReadLine() ?? "__bktignorenull";
                            string[] command = tempCommand.Split(' ');
                            if (command.Contains("__bktignorenull"))
                            {
                                Console.WriteLine("Null error.");
                                break;
                            }
                            aliases.Add((aliasName, command));
                            break;
                        case "pkgmngr":
                            if (commin.Length >= 3)
                            {
                                if (mConnected == true)
                                {
                                    if (commin[1] == "install")
                                    {
                                        try
                                        {
                                            switch (commin[2])
                                            {
                                                case "hl3":
                                                    Console.WriteLine("No.");
                                                    Console.WriteLine("\r\nDid you mean: pkgmngr install *hlvr*");
                                                    break;
                                                case "hlvr":
                                                    Console.WriteLine("Now redirecting to valve\\steam:32768");
                                                    Process.Start(new ProcessStartInfo
                                                    {
                                                        FileName = "https://store.steampowered.com/app/546560/HalfLife_Alyx/",
                                                        UseShellExecute = true
                                                    });
                                                    break;
                                                case "totally_not_a_virus_trust_me_im_a_dolphin":
                                                    Console.WriteLine("Installing 253291B package...");
                                                    Thread.Sleep(Math.Clamp((2532291 / mSpeed) * 1000, 1, int.MaxValue));
                                                    virused = true;
                                                    Console.WriteLine("Installed!");
                                                    break;
                                                case "tokimla82":
                                                    Console.WriteLine("Installing 645592B package...");
                                                    Thread.Sleep(Math.Clamp((645592 / mSpeed) * 1000, 1, int.MaxValue));
                                                    Console.WriteLine("Installed!");
                                                    break;
                                                default:
                                                    int pkgSize = rand.Next(16384, 1048576);
                                                    Console.WriteLine($"Installing {pkgSize}B package...");
                                                    Thread.Sleep(Math.Clamp((pkgSize / mSpeed) * 1000, 1, int.MaxValue));
                                                    if (rand.Next(1, 256) == 255 && (!specialDays.spaceDay))
                                                    {
                                                        virused = true;
                                                    }
                                                    break;
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Download failed. Please try again");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(
                                            "penis"
                                            );
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please connect to the Internet.");
                                    Console.WriteLine("Found 1 ISP in isps.cfg file: Fuckston Communications Services. 1-800-intnet");
                                }
                            }
                            break;
                        case "reboot":
                            rbt0 = true;
                            Console.Clear();
                            publicMain(["vs", "49"]);
                            break;
                        case "drdickhd":
                            Console.WriteLine("Dr. Dickhead is scanning for viruses...");
                            Console.WriteLine();
                            Thread.Sleep(rand.Next(15000, 30000));
                            if (virused == true)
                            {
                                Console.WriteLine("Dolphin Virus detected on computer. Removing...");
                                Thread.Sleep(rand.Next(5000, 10000));
                                virused = false;
                                Console.WriteLine("Dolphin Virus removed.");
                                break;
                            }
                            if (File.Exists("GORDON"))
                            {
                                if (rand.Next(0, 51) == 0)
                                {
                                    Console.WriteLine("Unknown struct potentially belonging to Dolphin Virus removed.");
                                    File.Delete("GORDON");
                                }
                                else
                                {
                                    Console.WriteLine("Dr. Dickhead detected an unknown struct in RAM at 0x8086. Potential Dolphin.");
                                }
                            }
                            if (virused == false)
                            {
                                Console.WriteLine("Computer is clean.");
                                break;
                            }
                            break;
                        case "flush":
                            crshChance = 5000;
                            munCycle = 0;
                            tick = 0;
                            Console.WriteLine("System flush successful.");
                            break;
                        case "win":
                        case "ntdetect":
                            Console.WriteLine("NTVDM not found. Cannot run 16-bit app");
                            break;
                        case "ntoskrnl":
                        case "smss":
                        case "csrss":
                            Console.WriteLine("Cannot run native binaries in NT-DOS subsystem");
                            break;
                        case "drdhtsr":
                            if (drdhtsr == null || !drdhtsr.IsAlive)
                            {
                                stopTsr = false; // RESET THE FUCKING STOP FLAG
                                drdhtsr = new Thread(drdickhead_tsr);
                                drdhtsr.Start();
                                Console.WriteLine("Dr. Dickhead TSR started!");
                            }
                            else
                            {
                                Console.WriteLine("Dr. Dickhead TSR is already running.");
                            }
                            break;
                        case "lotto":
                            Console.Write("Enter lotto numbers: ");
                            string userNums = Console.ReadLine() ?? ""
                                                     .Replace("-", "")
                                                     .ToUpper();
                            string actual;
                            do
                            {
                                actual = aprtMain.genHexStr(16, 4).Replace("-", "").ToUpper();
                            } while (userNums == actual);
                            Console.WriteLine($"Actual numbers were {actual}");
                            break;
                        case "shutdown":
                            Console.WriteLine("Shutting down...");
                            impulse(5001);
                            rbt0 = true;
                            Thread.Sleep(500); // make sure everything's functional
                            Console.WriteLine("It is now safe to close down Borktorial");
                            while (true)
                            {
                                Thread.Sleep(int.MaxValue);
                            }
                        case "dbg::virusedToggle":
                            virused = !virused;
                            break;
                        case "color":
                            if (commin.Length == 3)
                            {
                                Dictionary<string, int> colors = new()
                                {
                                    {"0", 0},
                                    {"1", 1},
                                    {"2", 2},
                                    {"3", 3},
                                    {"4", 4},
                                    {"5", 5},
                                    {"6", 6},
                                    {"7", 7},
                                    {"8", 8},
                                    {"9", 9},
                                    {"a", 10},
                                    {"b", 11},
                                    {"c", 12},
                                    {"d", 13},
                                    {"e", 14},
                                    {"f", 15}
                                };
                                if (colors.TryGetValue(commin[1], out int value) == true)
                                {
                                    Console.ForegroundColor = (ConsoleColor)value;
                                }
                                if (colors.TryGetValue(commin[2], out int bgvalue) == true)
                                {
                                    Console.BackgroundColor = (ConsoleColor)bgvalue;
                                }
                            }
                            break;
                        case "cls":
                            Console.Clear(); break; // SINGLE-LINE COMMAND!
                        case "jebkerman":
                            if (jebcounter < 16 && jmtrigger == false)
                            {
                                jebcounter++;
                                break;
                            }
                            if (jebcounter == 16)
                            {
                                Console.WriteLine(jebMsg);
                                jebcounter = 0;
                                jmtrigger = true;
                                break;
                            }
                            break;
                        case "kill":
                            if (commin.Length == 2)
                            {
                                switch (commin[1])
                                {
                                    case "ntuinit":
                                    case "winlogon":
                                    case "csrss":
                                    case "smss":
                                    case "ntoskrnl":
                                        keBugCheck(0xE00002c, new(1999, 12, 31));
                                        break;
                                    case "cmdshell":
                                        while (true)
                                        {
                                            Thread.Sleep(int.MaxValue);
                                        }
                                    case "drdhtsr":
                                        if (drdhtsr != null && drdhtsr.IsAlive)
                                        {
                                            stopTsr = true;
                                            drdhtsr.Join(); // Wait for clean stop
                                            Console.WriteLine("Process drdhtsr terminated successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("No process found.");
                                        }
                                        break;
                                    default:
                                        if (rand.Next(0, 255) == 0)
                                        {
                                            keBugCheck(0xE000002, new(5, 5, 5));
                                        }
                                        if (rand.Next(0, 255) == 127)
                                        {
                                            while (true)
                                            {
                                                Thread.Sleep(int.MaxValue);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Process {commin[1]} terminated successfully");
                                            break;
                                        }

                                }
                            }
                            break;
                        case "modernai":
                            while (true)
                            {
                                throw new Exception("fuck image gen ai and all the ones intended to replace writers or programmers or some shit", new Exception($"{errGen.generateErr()[0]} -- {errGen.generateErr()[1]}"));
                            }
                        case "baconflavoredshapez":
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Clear();
                            if (commin.Length == 1)
                            {
                                for (ulong i = 0; i < ulong.MaxValue; i++)
                                {
                                    // Thou thread shalt not sleep
                                    // Thread.Sleep(1);
                                    if (rand.Next(0, 5) == 0)
                                    {
                                        Console.Write("A");
                                    }
                                    if (rand.Next(0, 5) == 1)
                                    {
                                        Console.Write("R");
                                    }
                                    if (rand.Next(0, 5) == 2)
                                    {
                                        Console.Write("G");
                                    }
                                    if (rand.Next(0, 5) == 3)
                                    {
                                        Console.Write("H");
                                    }
                                    if (rand.Next(0, 5) == 4)
                                    {
                                        Console.Write("!");
                                    }
                                    if (rand.Next(0, 255) == 0)
                                    {
                                        Console.Write((char)rand.Next(0, 256));
                                    }
                                }
                            }
                            if (commin[1] == "--nonormalcyallowed" || specialDays.aprilfool)
                            {
                                for (ulong i = 0; i < ulong.MaxValue; i++)
                                {
                                    Console.Write((char)rand.Next(0, 256));
                                }
                            }
                            else
                            {
                                Console.WriteLine($"cannot find: {commin[0]}");
                                break;
                            }
                            break;
                        case "dbg::namegen":
                            Console.WriteLine(aprtMain.mkShitUsername(rand));
                            break;
                        case "help":
                            Console.WriteLine("Available commands:");
                            Console.WriteLine("  echo <text>               - Print text to the screen.");
                            Console.WriteLine("  dir                       - List files in the current directory.");
                            Console.WriteLine("  cd <dirname>              - Change current directory");
                            Console.WriteLine("  create <filename>         - Make file");
                            Console.WriteLine("  del <filename>            - Delete file");
                            Console.WriteLine("  deltree <dirname>         - Delete folder");
                            Console.WriteLine("  type <filename>           - Type file contents");
                            Console.WriteLine("  pkgmngr install <package> - Install a package (try 'hl3', 'totally_not_a_virus_trust_me_im_a_dolphin', or 'tokimla82').");
                            Console.WriteLine("  drdickhd                  - Scan for and remove viruses.");
                            Console.WriteLine("  drdhtsr                   - Start the Dr. Dickhead TSR (background virus monitor).");
                            Console.WriteLine("  kill <process>            - Terminate a process (try 'p32krnl', 'cmdshell', or 'drdhtsr').");
                            Console.WriteLine("  reboot                    - Reboot the system.");
                            Console.WriteLine("  shutdown                  - Shutdown the system.");
                            Console.WriteLine("  color <fg> <bg>           - Set text and background color (0-9, A-F).");
                            Console.WriteLine("  cls                       - Clear the screen.");
                            Console.WriteLine("  hl3                       - Confirm Half-Life 3.");
                            Console.WriteLine("  specs                     - Show system hardware");
                            Console.WriteLine("  atdt <number>             - Dialer");
                            Console.WriteLine("  drinkfood                 - The command line version of psychadelics");
                            Console.WriteLine("  satconnect                - Connect to satellite internet");
                            Console.WriteLine("  format                    - Format drive");
                            Console.WriteLine("  cmdmail                   - Make CommandMail(TM) codes to share with others");
                            Console.WriteLine("  cpp                       - Start the Cookies++ interpreter");
                            Console.WriteLine();
                            Console.WriteLine("For extra fun, try exploring on your own. Some secrets are hidden! e.g a very certain pilot kerbal. \r\n" +
                                "\r\nNote: Call 1-800-intnet for free internet");
                            break;
                        case "sudo":
                            if (root == true)
                            {
                                Console.WriteLine("This command does literally nothing.");
                            }
                            else
                            {
                                Console.WriteLine("You're not in sudoers. This incident will be reported to the FBI");
                            }
                            break;
                        case "recursion":
                            Console.WriteLine("Did you mean: recursion");
                            break;
                        case "format":
                            if (commin.Length >= 2)
                            {
                                switch (commin[1])
                                {
                                    case "a:":
                                        Console.WriteLine("Insert diskette into drive A: to format");
                                        Console.WriteLine("Waiting for diskette...");
                                        Thread.Sleep(rand.Next(2500, 3000));
                                        Console.Write("Found diskette! Format: 720KB\r\n");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey(false);
                                        Console.WriteLine();
                                        for (int i = 0; i < 1440; i++)
                                        {
                                            Console.Write($"Sector {i:D4}/1440...");
                                            Thread.Sleep(rand.Next(500, 1000));
                                            Console.Write("Done\r\n");
                                            Thread.Sleep(rand.Next(15, 50));
                                        }
                                        Console.WriteLine("Format successful. Returning to DOS");
                                        break;
                                    case "b:":
                                        Console.WriteLine("Insert diskette into drive B: to format");
                                        Console.WriteLine("Waiting for diskette...");
                                        Thread.Sleep(rand.Next(2500, 3000));
                                        Console.Write("Found diskette! Format: 720KB\r\n");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey(false);
                                        Console.WriteLine();
                                        for (int i = 0; i < 1440; i++)
                                        {
                                            Console.Write($"Sector {i:D4}/1440...");
                                            Thread.Sleep(rand.Next(500, 1000));
                                            Console.Write("Done\r\n");
                                            Thread.Sleep(rand.Next(15, 50));
                                        }
                                        Console.WriteLine("Format successful. Returning to DOS");
                                        break;
                                    case "c:":
                                        Console.WriteLine("WARNING! All data on non-removable disk C: will be erased!");
                                        Console.WriteLine("Are you sure you wanna continue (Y/N)? ");
                                        ConsoleKey fcChoice = Console.ReadKey(true).Key;
                                        switch (fcChoice)
                                        {
                                            case ConsoleKey.Y:
                                                Console.WriteLine();
                                                for (int i = 0; i < 1228800; i++)
                                                {
                                                    Console.Write($"Sector {i:D7}/1228800...");
                                                    Thread.Sleep(rand.Next(10, 20));
                                                    Console.Write("Done\r\n");
                                                    Thread.Sleep(rand.Next(5, 15));
                                                    if (i > 2880)
                                                    {
                                                        fs = new fileSys();
                                                        Console.WriteLine("[NTDOS] System error (NT_SUBSYS_EXITED)");
                                                        Console.WriteLine("Warning: OS is pretty much on life support");
                                                        Thread.Sleep(5000);
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case "specs":
                            Console.WriteLine(sysspecs);
                            break;
                        case "cat":
                            catGoBrr(100);
                            break;
                        case "slowcat":
                            catGoBrr(200);
                            break;
                        case "fastcat":
                            catGoBrr(50);
                            break;
                        case "halton":
                            rbt0 = true; // kill everything
                            ulong count = 0;
                            while (count < ulong.MaxValue)
                            {
                                Thread.Sleep(int.MaxValue);
                                count++;
                            }
                            rbt0 = false;
                            break;
                        case "check_unknown_ints":
                            Console.WriteLine("[INT 5Fh] Link to Kerbal Space Center success!");
                            jebconnect = true;
                            break;
                        case "whoami":
                            Console.WriteLine($"NTUSERS\\{username}");
                            break;
                        case "jebmail":
                            Console.WriteLine("Jebmail e-mail client connecting...");
                            if (jebconnect == true)
                            {
                                Console.WriteLine("Connected successfully!");
                                Console.WriteLine("Current email address: billKerman42@ksc.com");
                                Console.WriteLine("No new emails.");
                            }
                            else
                            {
                                Console.WriteLine("Not connected to KSC.");
                            }
                            break;
                        case "atdt":
                            if (commin.Length == 2)
                            {
                                switch (commin[1])
                                {
                                    case "1-800-intnet":
                                        Console.WriteLine("Dialing...");
                                        playModemSound();
                                        Console.WriteLine("Connected to Fuckston Communications Services!");
                                        mSpeed = 1800;
                                        mConnected = true;
                                        if (specialDays.spaceDay)
                                        {
                                            mSpeed += (mSpeed / 4);
                                        }
                                        mCt = mConnectTypes.dUp;
                                        break;
                                    case "1-800-fastnet":
                                        Console.WriteLine("Dialing...");
                                        playModemSound();
                                        Console.WriteLine("Connected to Aperture V.32bis-compressed");
                                        mSpeed = 2000; // 16000bps
                                        mConnected = true;
                                        if (specialDays.spaceDay)
                                        {
                                            mSpeed += (mSpeed / 4);
                                        }
                                        mCt = mConnectTypes.dupCompressed;
                                        break;
                                    default:
                                        Console.WriteLine("Dialing...");
                                        Thread.Sleep(rand.Next(38400, 76800));
                                        Console.WriteLine("Disconncted.");
                                        break;
                                }
                            }
                            break;
                        case "etherconnect":
                            Console.WriteLine("Connecting to ethernet...");
                            Thread.Sleep(rand.Next(2000, 3000));
                            mSpeed = 524288;
                            mConnected = true;
                            if (specialDays.spaceDay)
                            {
                                mSpeed += (mSpeed / 4);
                            }
                            mCt = mConnectTypes.etr;
                            Console.Write("Done!\r\n");
                            break;
                        case "satconnect":
                            Console.Write("\r\n");
                            Console.Write("Finding optimal satellite cluster...");
                            Thread.Sleep(rand.Next(5300, 8800));
                            Console.Write($"Found sat group: {aprtMain.genHexStr(8, 4)}\r\n");
                            Thread.Sleep(rand.Next(1000, 2001));
                            mSpeed = rand.Next(51200, 153601);
                            mConnected = true;
                            if (specialDays.spaceDay)
                            {
                                mSpeed += (mSpeed / 4);
                            }
                            mCt = mConnectTypes.sat;
                            Console.Write($"Connected! Speed: {(float)mSpeed / (float)1024:F2}KB/s\r\n");
                            break;
                        case "This_command_is_not_actually_accessible_under_NORMAL_Cir**CUM**stances_**LOL**":
                            File.Create("GORDON").Dispose();
                            keBugCheck(0xCAFEBABE, new(2022, 2, 22));
                            break;
                        case "drinkfood":
                            Console.TreatControlCAsInput = true;
                            // just like real psychadelics, it's fun for a bit
                            // and then it fucking obiliterates everything
                            cfg[4] = 1;
                            saveCfg();
                            while (true)
                            {
                                Console.SetCursorPosition(rand.Next(0, Console.BufferWidth), rand.Next(0, Console.BufferHeight));
                                AnsiConsole.Markup($"[rgb({rand.Next(0, 256)},{rand.Next(0, 256)},{rand.Next(0, 256)}) on rgb({rand.Next(0, 256)},{rand.Next(0, 256)},{rand.Next(0, 256)})][blink][bold]?[/][/][/]");
                            }
                        case "lambda":
                            DateTime rightFuckingNow = DateTime.UtcNow;
                            bool hlDay = false;
                            if (rightFuckingNow.Month == 11)
                            {
                                if (rightFuckingNow.Day == 19)
                                {
                                    hlDay = true;
                                }
                            }
                            if (hlDay == true)
                            {
                                Console.Clear();
                                if (File.Exists(@"C:\Program Files (x86)\Steam\steamapps\music\Half-Life Soundtrack\01 Adrenaline Horror.mp3"))
                                {
                                    new Thread(() => mp3PlayLoop(@"C:\Program Files (x86)\Steam\steamapps\music\Half-Life Soundtrack\01 Adrenaline Horror.mp3"))
                                    {
                                        IsBackground = true
                                    }.Start();
                                }
                                else
                                {
                                    throw new Exception("The HL soundtrack is FREE!!! and you don't have it?");
                                }
                                while (true)
                                {
                                    Console.Write('λ');
                                }
                            }
                            else
                            {
                                Console.WriteLine($"cannot find: {commin[0]}");
                            }
                            break;
                        case "lgr":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            Console.WriteLine("WOODGRAIN!");
                            break;
                        case "vi":
                            Console.WriteLine("did you mean: emacs");
                            break;
                        case "emacs":
                            Console.WriteLine("did you mean: vi");
                            break;
                        case "nano":
                            Console.WriteLine("NOPE NOPE NOPE NOPE NOPE");
                            break;
                        case "atat":
                            Console.WriteLine("CAAAASHIES!!!");
                            break;
                        case "simd":
                            Console.WriteLine("Multiple Instruction Single Data (MISD)");
                            break;
                        case "saturn5":
                            Console.WriteLine("Did you mean: n1");
                            break;
                        case "n1":
                            Console.WriteLine("Did you mean: kaboom");
                            break;
                        case "aperture":
                            Console.WriteLine("REMEMBER! If a future you tries to warn you about this test, DON'T LISTEN!");
                            break;
                        case "kaboom":
                            Console.WriteLine("Did you mean: n1");
                            break;
                        case "news":
                            try
                            {
                                int newsSizeSum = 1;
                                foreach (string item in currNews)
                                {
                                    newsSizeSum += item.Length;
                                }
                                Thread.Sleep(rand.Next(350, 500));
                                if (mConnected)
                                {
                                    Console.WriteLine($"Fetching news (size: {aprtMain.byteFormat((ulong)newsSizeSum)})...");
                                }
                                else
                                {
                                    Console.WriteLine($"Fetching news (size: unknown)...");
                                }
                                if (!mConnected)
                                {
                                    Console.WriteLine("Please connect to the Internet.");
                                    break;
                                }
                                Thread.Sleep(newsSizeSum / mSpeed);
                                foreach (string item in currNews)
                                {
                                    if (!item.StartsWith("::p_"))
                                    {
                                        Console.WriteLine($"NEWS: {item}");
                                    }
                                    Thread.Sleep(150);
                                }
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("A error occurred while fetching the news.");
                                shitLog.createEntry("NEWS", $"{ex.Message} {ex.StackTrace}", logType.Err);
                                break;
                            }
                        case "radio":
                            if (!specialDays.spaceDay)
                            {
                                Console.WriteLine("Now listening: 89.25MHz. The Human Music");
                            }
                            else
                            {
                                Console.WriteLine("Now listening: 195.25MHz. Duna Radio Broadcasting");
                            }
                            SoundPlayer rSp = new("assets\\rd_canyon.wav");
                            if (Directory.Exists("assets\\customRadioSongs"))
                            {
                                string[] songs = Directory.GetFiles("assets\\customRadioSongs");
                                rSp.SoundLocation = songs[rand.Next(songs.Length - 1)];
                            }
                            break;
                        case "dumpsysstate":
                            aprtMain.dumpState<bktMain>();
                            break;
                        case "netinfo":
                            Console.WriteLine("nUtils Utility Pack 1.4 - NETINFO");
                            Console.WriteLine("Developed by Oog and Grug. (C) 1993,94,95");
                            Console.WriteLine("Published and supported by Clickcookie. Inc (C) 1994,95\r\n");
                            Console.WriteLine("Now detecting...");
                            int iMspeed = mSpeed;
                            iMspeed += rand.Next(-500, 501);
                            iMspeed -= rand.Next(-500, 501);
                            string mCtStr = "none";
                            if (mCt == mConnectTypes.Null)
                            {
                                mCtStr = "none";
                            }
                            else if (mCt == mConnectTypes.dUp)
                            {
                                mCtStr = "dialup";
                            }
                            else if (mCt == mConnectTypes.dupCompressed)
                            {
                                mCtStr = "compressed dialup";
                            }
                            else if (mCt == mConnectTypes.sat)
                            {
                                mCtStr = "satellite";
                            }
                            else
                            {
                                mCtStr = "unknown";
                            }
                            Thread.Sleep(rand.Next(1250, 5000));
                            if (!mConnected)
                            {
                                Console.WriteLine("connection type: none\r\n" +
                                    "speed: 0B/s\r\n" +
                                    "connected: no\r\n" +
                                    "variance: ???");
                            }
                            else
                            {
                                Console.WriteLine($"connection type: {mCtStr}\r\n" +
                                    $"speed: {aprtMain.byteFormat((UInt128)iMspeed)}/s\r\n" +
                                    $"connected: yes\r\n" +
                                    $"variance range: -{aprtMain.byteFormat((UInt128)(iMspeed / 4.5))}/s to {aprtMain.byteFormat((UInt128)(iMspeed / 4.5))}/s"
                                    );
                            }
                            break;
                        case "impulse":
                            try
                            {
                                if (commin.Length >= 2)
                                {
                                    impulse(int.Parse(commin[1]));
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "NO HOPIUM LEFT!!!")
                                {
                                    infLoop();
                                    static void infLoop()
                                    {
                                        infLoop(); // try to nuke the stack
                                    }
                                }
                                continue; // do nothing
                            }
                            break;
                        case "clock":
                            // NOTE: This uses the Borktorial Internal Clock.
                            // The BIC is synced with **UTC TIME**
                            // This will not be correct for a lot of people
                            // And i don't fucking care.
                            Console.WriteLine($"{DateTime.UtcNow:R} BT:{tick}-BMC:{munCycle}");
                            break;
                        case "cmdmail":
                            try
                            {
                                Console.WriteLine($"Current amount of paper: {giftCount}");
                                if (giftCount == 0)
                                {
                                    Console.WriteLine("You're all out of paper!");
                                    Console.WriteLine("To get more paper, you must use mail codes");
                                    break;
                                }
                                Console.Write("Command to send: ");
                                string cmd = Console.ReadLine() ?? "";
                                Console.Write("Message to bundle in (keep it nice!): ");
                                string msg = Console.ReadLine() ?? "";
                                Console.Write("Days until expiry: ");
                                int due = 0;
                                try
                                {
                                    due = int.Parse(Console.ReadLine() ?? "");
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid, defaulting to 3 days!");
                                    due = 3;
                                }
                                if (cmd.Contains('\0'))
                                {
                                    Console.WriteLine("Error: invalid command");
                                    break;
                                }
                                if (cmd == "\x01")
                                {
                                    Console.WriteLine("Error: invalid command");
                                    break;
                                }
                                if (msg.Contains('\0'))
                                {
                                    Console.WriteLine("Error: invalid message");
                                    break;
                                }
                                if (username.Contains('\0'))
                                {
                                    Console.WriteLine("Error: invalid username");
                                    break;
                                }
                                if (due > 14)
                                {
                                    Console.WriteLine("Error: cannot last more than 14 days!");
                                    break;
                                }
                                if (due < 1) 
                                {
                                    Console.WriteLine("Error: cannot last less than 1 day");
                                }
                                Console.WriteLine($"Your CommandMail(TM) code: {cmdMailEnc(cmd, msg, due)}");
                                Console.WriteLine($"This code will expire after {due} days");
                                Console.WriteLine("To use, simply type in the code directly into the command prompt");
                                Console.WriteLine("Copy it so you can share it with your friends!");
                                giftCount--;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error.");
                                shitLog.createEntry("CMDMAIL", ex.ToString(), logType.Err);
                                break;
                            }
                            break;
                        default:
                            if (string.IsNullOrWhiteSpace(string.Join(" ", commin)))
                            {
                                break; // do nothing.
                            }
                            try
                            {
                                if (rawCommin.StartsWith("mail."))
                                {
                                    if (usedGifts.Count > 5)
                                    {
                                        usedGifts.RemoveAt(0);
                                    }
                                    (string username, string command, string message, string sid) mail = cmdMailDec(rawCommin);
                                    if (mail == ("\x00", "\x01", "\x02", "0"))
                                    {
                                        Console.WriteLine("This mail code has expired.");
                                        break;
                                    }
                                    if (mail == ("\x02", "\x01", "\x00", "0"))
                                    {
                                        Console.WriteLine("This mail code is from the future.");
                                        break;
                                    }
                                    if (mail == ("\x06", "\x06", "\x06", "0"))
                                    {
                                        Console.WriteLine("Mail integrity error.");
                                        break;
                                    }
                                    if (mail == ("\x07", "\x07", "\x07", "0"))
                                    {
                                        Console.WriteLine("Format error.");
                                        break;
                                    }
                                    if (usedGifts.Contains(mail))
                                    {
                                        Console.WriteLine("You already used this code.");
                                        break;
                                    }
                                    usedGifts.Add(mail);
                                    if (giftCount < 5)
                                    {
                                        giftCount++;
                                    }
                                    Console.WriteLine("=== You've got mail! ===");
                                    AnsiConsole.MarkupLine($"From: {mail.username} (sid: {mail.sid})");
                                    Console.WriteLine("To: you");
                                    if (mail.command != "")
                                    {
                                        AnsiConsole.MarkupLine($"Command to try out: {mail.command}");
                                    }
                                    if (mail.message != "")
                                    {
                                        AnsiConsole.MarkupLine($"Message: {parseBorkTag(mail.message)}");
                                    }
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Hmmm, something seems wrong with your CommandMail(TM) code");
                                shitLog.createEntry("CMDMAIL", ex.ToString(), logType.Err);
                                break;
                            }
                            string scriptName = Path.Combine("mods", commin[0] + ".lua");
                            if (commin[0].StartsWith("dbg::"))
                            {
                                scriptName = Path.Combine("mods", "debug", commin[0] + ".lua");
                            }
                            if (File.Exists(scriptName))
                            {
                                using Lua lua = new();
                                lua.LoadCLRPackage();

                                try
                                {
                                    string? luAsm = Assembly.GetExecutingAssembly().GetName().Name;
                                    string? lut = typeof(bktMain).FullName;

                                    lua.DoString($@"
                                            luanet.load_assembly('{luAsm}')
                                            Sys = luanet.import_type('{lut}')
                                        ");

                                    if (lua["Sys"] == null)
                                    {
                                        shitLog.createEntry("LUALDR", "Failed to load ASM. Sys was null.", logType.Err);
                                        Console.WriteLine("Error: Sys was equal to null");
                                        break;
                                    }
                                    else
                                    {
                                        lua["Args"] = string.Join(" ", commin.Skip(1));
                                        lua["ArgsRaw"] = string.Join(" ", rawCommin.Split(" ").Skip(1)); // listen man if it works.
                                        lua["ArgsNoSkip"] = string.Join(" ", commin);
                                        lua["ArgsRawNoSkip"] = rawCommin;
                                        lua.DoFile(scriptName);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    shitLog.createEntry("LUALDR", ex.ToString(), logType.Err);
                                    throw;
                                }
                                break;
                            }
                            if (ballmerMode)
                            {
                                if (string.Join(' ', commin) ==
                                    System.Text.Encoding.UTF8.GetString(
                                        System.Convert.FromBase64String(
                                            "aGVsbG8sIHRoaXMgaXMgdGhlIHBhcnQgd2hlcmUgaSBraWxsIHlvdSE=")))
                                {
                                    Console.WriteLine("achievement unlocked: well that was pointless\r\n" +
                                        "Description: You somehow found this secret i went out of my way to hide\r\n" +
                                        "Are you proud of yourself?");
                                }
                            }
                            shitLog.createEntry("CMDHNDLR", $"cannot find: {commin[0]}", logType.Err);
                            Console.WriteLine($"cannot find: {commin[0]}");
                            break;
                    }
                    shitLog.createEntry("cmdhndlr", $"User entered command: {rawCommin}", logType.Info);
                }
                // MInor SPecialized EXception service
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("[Mispex] Null reference exception. Perhaps invalid input?");
                    shitLog.createEntry("Mispex", $"Null ref exception. {ex.StackTrace}", logType.Warn);
                    continue; // It'll be all FIIINE i'm sure of it
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("[Mispex] Divide By Zero intercepted! X to throw.");
                    shitLog.createEntry("Mispex", $"Division by zero! {ex.StackTrace}", logType.Err);
                    ConsoleKey dbzEKy = Console.ReadKey(true).Key;
                    switch (dbzEKy)
                    {
                        case ConsoleKey.X:
                            throw;
                    }
                }
                // General hndlr
                catch (Exception ex)
                {
                    shitLog.createEntry("EXCPTHN", $"ERROR: {ex.Message} - {ex.StackTrace}\r\n", logType.Err);
                    Console.WriteLine("A fatal error has occurred and NT-DOS cannot continue");
                    Console.WriteLine("This error has been logged\r\n");
                    Console.WriteLine(ex);
                    Console.WriteLine("Press SPACE to continue or any other key to throw");
                    ConsoleKeyInfo ck = Console.ReadKey(true);
                    if (ck.Key == ConsoleKey.Spacebar)
                    {
                        Console.WriteLine("WARNING: might be unstable!");
                    }
                    else
                    {
                        throw;
                    }
                }

                if (rand.Next(0, crshChance) == 0)
                {
                    string[] errG = errGen.generateErr();
                    int errCode = rand.Next(); // DONTFIXME: The values this shit returns are probably gonna be pretty fuckin' funny
                    keBugCheck((uint)errCode);
                }
            }
        }
        public static bool stopTsr = false;

        public static void drdickhead_tsr()
        {
            while (!stopTsr)
            {
                Thread.Sleep(30000);
                if (virused)
                {
                    Console.WriteLine("ALERT! Potential virus detected. Run drdickhd now!");
                }
                else if (rand.Next(0, 255) == 0)
                {
                    Console.WriteLine("ALERT! Possible virus activity. Run drdickhd now!");
                }
            }

            Console.WriteLine("Dr. Dickhead TSR shutting down...");
        }
        /// <summary>
        /// Modernization of ftlCrash()
        /// </summary>
        /// <param name="errCode">error code</param>
        /// <param name="dt">datetime</param>
        public static void keBugCheck(uint errCode, DateTime dt = new())
        {
            // this funkiness is because you can't have DateTime as default param so we have to do this
            if (dt == new DateTime())
            {
                dt = DateTime.UtcNow;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            string errName = errGen.genCustomTemplate(
                errGen.templates[
                    new Random((int)errCode + strSum(dt.ToString("R"))).Next(
                        errGen.templates.Length)])[0];
            string pName = errGen.genCustomTemplate(
                errGen.templates[
                    new Random((int)errCode + strSum(dt.ToString("R"))).Next(
                        errGen.templates.Length)])[1];
            if (rand.Next(0, int.MaxValue) == 0 && iRnd == 0)
            {
                Debug.WriteLine("immortal tiger");
            }
            writeFullLine($"*** STOP: 0x{errCode:X8} ({errName})");
            writeEmptyLine();
            writeFullLine($"A problem has been detected and NT-DOS has been shut down to prevent damage to your computer");
            writeEmptyLine();
            writeFullLine($"The problem seems to be caused by the following process: {pName}");
            writeEmptyLine();
            writeFullLine($"If this is the first time you've seen this Stop error screen, restart your computer. If this screen appears again, follow these steps: ");
            writeEmptyLine();
            writeFullLine($"Check to make sure any new hardware or software is properly installed. If this is a new installation, ask your hardware or software manufacturer for any NT-DOS updates you might need.");
            writeEmptyLine();
            writeFullLine(
                $"If problems continue, disable or remove any newly installed hardware or software. Disable BIOS memory options such as caching or shadowing. If you need to use Safe Mode to remove or disable components, restart your computer, press F8 to select Advanced Startup Options, and then select Safe Mode.");
            writeEmptyLine();
            writeFullLine($"Technical information:");
            writeEmptyLine();
            writeFullLine($"*** STOP: 0x{errCode:X8} (0x{rand.Next(int.MaxValue):X8}, 0x{rand.Next(int.MaxValue):X8}, 0x{rand.Next(int.MaxValue):X8}, 0x{rand.Next(int.MaxValue):X8})");
            writeEmptyLine();
            for (int i = 0; i < rand.Next(8, 12); i++)
            {
                string pName2 = errGen.genCustomTemplate(
                errGen.templates[
                    new Random((int)errCode + strSum(dt.ToString("R"))).Next(
                        errGen.templates.Length)])[1];
                if (i == 0)
                {
                    writeFullLine($"***       {pName}  -  Address 0x{rand.Next(int.MaxValue):X8} base at 0x{rand.Next(int.MaxValue):X8}, DateStamp 0x{rand.Next(int.MaxValue):X8}");
                }
                Thread.Sleep(250);
                writeFullLine($"***       {pName2}  -  Address 0x{rand.Next(int.MaxValue):X8} base at 0x{rand.Next(int.MaxValue):X8}, DateStamp 0x{rand.Next(int.MaxValue):X8}");
            }
            writeEmptyLine();
            writeFullLine($"Beginning dump of physical memory...");
            writeFullLine($"Physical memory dump initializing: {pName} at fault");
            writeEmptyLine();

            for (int i = 0; i < 6; i++)
            {
                writeFullLine($"  0x{rand.Next(int.MaxValue):X8}  {aprtMain.genHexStr(8, 0, ' ')} {aprtMain.genHexStr(8, 0, ' ')} {aprtMain.genHexStr(8, 0, ' ')} {aprtMain.genHexStr(8, 0, ' ')}");
            }

            writeEmptyLine();
            writeFullLine($"Dumping physical memory to disk...");

            for (int pct = 0; pct <= 100;)
            {
                Console.Write($"\rPhysical memory dump: {Math.Min(pct, 100)}% complete    ");
                if (pct > 90)
                {
                    Thread.Sleep(rand.Next(150, 200));
                }
                Thread.Sleep(rand.Next(50, 300));
                pct += rand.Next(1, 8);
                if (pct > 95 && pct < 100)
                {
                    pct = 100;
                }
            }

            writeEmptyLine();
            writeFullLine($"Physical memory dump complete.");
            writeEmptyLine();
            writeFullLine($"Contact your system administrator or technical support group for further assistance.");
            writeEmptyLine();
            int rdSize = rand.Next(128, 524288);
            writeFullLine($"Memory dumped: {rdSize} KB");
            writeFullLine($"Dump file: C:\\WINNT\\MEMORY.DMP");
            fs.mkFile("\\WINNT\\MEMORY.DMP", aprtMain.mkRndByteArray(rdSize / 8));
            impulse(5001); // save fs
            writeFullLine($"Report ID: {aprtMain.genHexStr(8, 4)}-{aprtMain.genHexStr(12, 4)}-{aprtMain.genHexStr(8, 4)}");
            writeEmptyLine();
            writeFullLine($"*** Fatal System Error: 0x{errCode:X8} ({errName})");
            writeFullLine($"*** Process: {pName} (PID: {rand.Next(1, 65536)})");
            writeEmptyLine();
            writeFullLine($"The system has been halted.");
            rbt0 = true;
            while (true)
            {
                Thread.Sleep(int.MaxValue);
            }
        }
        /// <summary>
        /// Helper for the crash screen
        /// </summary>
        /// <param name="text">What to say</param>
        public static void writeFullLine(string text)
        {
            Console.WriteLine(text.PadRight(Console.WindowWidth));
        }
        /// <summary>
        /// Another helper
        /// </summary>
        public static void writeEmptyLine()
        {
            Console.WriteLine(new string(' ', Console.WindowWidth));
        }
        public static int strSum(string inp)
        {
            char[] inp2 = inp.ToCharArray();
            int accu = 0;
            foreach (char item in inp2)
            {
                accu += item;
            }
            return accu;
        }
        public static void sf59(string code)
        {
            Dictionary<string, (string resource, string filename)> secrets = new()
            {
                ["waluigi"] = ("borktorial.rsrc.screenshot16.png", "the mun awaits.png"),
                ["igiulaw"] = ("borktorial.rsrc.eula.txt", "eula.txt")
            };

            if (!secrets.TryGetValue(code, out (string resource, string filename) secret))
            {
                return;
            }

            using Stream? stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(secret.resource) ?? throw new Exception("Err in sf15: Stream was null");
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            File.WriteAllBytes(secret.filename, ms.ToArray());
            if (!code.StartsWith("msc_"))
            {
                File.SetCreationTime(secret.filename, DateTime.UnixEpoch);
                File.SetLastWriteTime(secret.filename, DateTime.UnixEpoch);
                File.SetLastAccessTime(secret.filename, DateTime.UnixEpoch);

                Environment.Exit(69); // nice
            }
        }
        /// <summary>
        /// SOURCE! SOURCE!
        /// </summary>
        /// <param name="num">number</param>
        /// <exception cref="Exception">HOPIUM ADMINISTERED</exception>
        public static void impulse(int num)
        {
            switch (num)
            {
                case 0: // i0: standard bkt ticker
                    tick++;
                    break;
                case 101:
                    Console.WriteLine("HOPIUM ADMINISTERED");
                    break;
                case 202:
                    string fjbjB = "HALF-LIFE 3 IS COMING SOON! ";
                    string fjbjB2 = "ALERT: OUT OF HOPIUM! ";
                    string fjbjB3 = "HOPIUM LEVELS CRITICAL! ";
                    double hopium = 5;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.Black;
                    for (int i = 0; i < 512; i++)
                    {
                        for (int j = 0; j < fjbjB.Length; j++)
                        {
                            hopium -= 0.005;
                            Console.Write(fjbjB[j]);
                            Thread.Sleep(20);
                            if (hopium <= 0)
                            {
                                throw new Exception("NO HOPIUM LEFT!!!");
                            }
                            if (hopium < 3)
                            {
                                for (int k = 0; k < fjbjB3.Length; k++)
                                {
                                    hopium -= 0.1;
                                    Console.Write(fjbjB3[k]);
                                    Thread.Sleep(20);
                                }
                            }
                            if ((rand.Next(0, 7) == 0) && (j == fjbjB.Length - 1))
                            {
                                for (int k = 0; k < fjbjB2.Length; k++)
                                {
                                    hopium -= 0.05;
                                    Console.Write(fjbjB2[k]);
                                    Thread.Sleep(20);
                                }
                            }
                        }
                    }
                    break;
                case 404:
                    string f22Raptor = "DWARVES! ";
                    for (int i = 0; i < 4096; i++)
                    {
                        void annoy1()
                        {
                            f22Raptor += "DWARVES! ";
                            DialogResult ffmf = MessageBox.Show("PLAY DWARF FORTRESS NOW!!!",
                                "borktorial",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                0,
                                false);
                            if (ffmf != DialogResult.Yes)
                            {
                                ffmf = MessageBox.Show("PLAY DWARF FORTRESS NOW!!!",
                                    "borktorial",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1,
                                    0,
                                    false);
                            }
                        }
                        for (int k = 0; k < f22Raptor.Length; k++)
                        {
                            Console.Write(f22Raptor[k]);
                            Thread.Sleep(20 + (int)(Math.Log10(f22Raptor.Length)));

                        }
                        new Thread(annoy1).Start();
                    }

                    break;
                case 405:
                    string[] frames = [
                        "=--\0---\0---",
                        "-=-\0---\0---",
                        "--=\0---\0---",
                        "---\0--=\0---",
                        "---\0-=-\0---",
                        "---\0=--\0---",
                        "---\0---\0=--",
                        "---\0---\0-=-",
                        "---\0---\0--=",
                    ];
                    playAnim(frames, 500);
                    Console.WriteLine("WOOAH LOOK AT THAT = GO!");
                    break;
                case 1033:
                    sttw();
                    break;
                case 5000:
                    char[] bootiniContents = """
                        [bootldr]
                        ; ntdos bootloader 1.3
                        default IDE0:\part0\WINNT\NTOSKRNL.EXE /SRLOUTONLY
                        """.ToCharArray();
                    // Root directories
                    fs.mkDir("WINNT");

                    // WINNT structure
                    fs.mkDir("WINNT\\System32");
                    fs.mkDir("WINNT\\System32\\config");
                    fs.mkDir("WINNT\\System32\\drivers");
                    fs.mkDir("WINNT\\System32\\spool");
                    fs.mkDir("WINNT\\System32\\spool\\printers");
                    fs.mkDir("WINNT\\System");
                    fs.mkDir("WINNT\\Temp");
                    fs.mkDir("WINNT\\Fonts");
                    fs.mkDir("WINNT\\Help");

                    // User profiles
                    fs.mkDir("WINNT\\Profiles");
                    fs.mkDir("WINNT\\Profiles\\Administrator");
                    fs.mkDir("WINNT\\Profiles\\Administrator\\Desktop");
                    fs.mkDir("WINNT\\Profiles\\Administrator\\Start Menu");
                    fs.mkDir("WINNT\\Profiles\\Administrator\\Start Menu\\Programs");
                    fs.mkDir("WINNT\\Profiles\\Administrator\\Personal");
                    fs.mkDir("WINNT\\Profiles\\Default User");
                    fs.mkDir("WINNT\\Profiles\\Default User\\Desktop");
                    fs.mkDir("WINNT\\Profiles\\Default User\\Start Menu");

                    // System files
                    fs.mkFile("WINNT\\System32\\ntoskrnl.exe", aprtMain.mkRndByteArray(32753));
                    fs.mkFile("WINNT\\System32\\hal.dll", aprtMain.mkRndByteArray(19285));
                    fs.mkFile("WINNT\\System32\\ntdll.dll", aprtMain.mkRndByteArray(25932));
                    fs.mkFile("WINNT\\System32\\kernel32.dll", aprtMain.mkRndByteArray(49521));
                    fs.mkFile("WINNT\\System32\\user32.dll", aprtMain.mkRndByteArray(19564));
                    fs.mkFile("WINNT\\System32\\gdi32.dll", aprtMain.mkRndByteArray(45943));
                    fs.mkFile("WINNT\\System32\\smss.exe", aprtMain.mkRndByteArray(19532));
                    fs.mkFile("WINNT\\System32\\csrss.exe", aprtMain.mkRndByteArray(25316));


                    // Registry hives
                    fs.mkFile("WINNT\\System32\\config\\SAM", aprtMain.mkRndByteArray(32768));
                    fs.mkFile("WINNT\\System32\\config\\SECURITY", aprtMain.mkRndByteArray(32768));
                    fs.mkFile("WINNT\\System32\\config\\SOFTWARE", aprtMain.mkRndByteArray(32768));
                    fs.mkFile("WINNT\\System32\\config\\SYSTEM", aprtMain.mkRndByteArray(32768));
                    fs.mkFile("WINNT\\System32\\config\\DEFAULT", aprtMain.mkRndByteArray(32768));

                    // Boot files
                    fs.mkFileChr("boot.ini", bootiniContents);
                    fs.mkFile("ntldr", aprtMain.mkRndByteArray(5942));
                    fs.mkFile("ntdetect.com", aprtMain.mkRndByteArray(2585));

                    // EGG
                    fs.mkFileChr("WINNT\\System32\\drivers\\README.TXT", "You just lost the game.".ToCharArray());
                    break;
                case 5001:
                    File.WriteAllBytes("bktfs", fs.toBinary());
                    break;
                case 5002:
                    if (File.Exists("bktfs"))
                    {
                        // load
                        fs = fileSys.fromBinary(File.ReadAllBytes("bktfs"));
                    }
                    else
                    {
                        // don't do shit
                        break;
                    }
                    break;
                default:
                    // note: iRnd is a value decided at start-time
                    // that equals to a random value from 0 to 12
                    // or something i forgor
                    if (iRnd == 4)
                    {
                        char[] ns = num.ToString().ToCharArray();
                        if (ns.Length < 4)
                        {
                            return;
                        }
                        int sum = 1;
                        foreach (char item in ns)
                        {
                            sum += item;
                        }
                        if (sum % 7 == 0)
                        {
                            Console.WriteLine("CONGO RATS!!! You found The Secret!");
                            Console.WriteLine("Now processing the reward...");
                            Thread.Sleep(500);
                            Console.WriteLine("Contacting bkt://do_not_look_very_hidden/secret...");
                            Thread.Sleep(1500);
                            Console.WriteLine("Success. Your reward is...");
                            File.Create("GORDON").Dispose();
                            cfg =
                                ['K', 'E', 'R', 'B', 'A', 'L',
                                'S', 'P', 'A', 'C', 'E',
                                'C', 'E', 'N', 'T', 'E', 'R'];
                            saveCfg();
                            Console.Write("ERRORS!\r\n");
                            Console.WriteLine("Get pranked dingus.");
                            Thread.Sleep(1500);
                            publicMain(["baboons!"]);
                        }
                    }
                    int d4 = rand.Next(0, 3);
                    if (d4 == 0)
                    {
                        impulse(rand.Next(0, 65535));
                    }
                    else if (d4 == 1)
                    {
                        Console.WriteLine($"No function found at idx {num}");
                    }
                    else if (d4 == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("CPU triple fault detected!");
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        keBugCheck(0xF0, new(4, 1, 2005));
                    }
                    break;
            }
            return;
        }
        public static void mp3PlayLoop(string path)
        {
            while (true)
            {
                if (rbt0)
                {
                    return;
                }
                using Mp3FileReader mp3Reader = new(path);
                using WaveOutEvent waveOut = new();
                waveOut.Init(mp3Reader);
                waveOut.Play();
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }
        public static void timeLoop(int tl, int mcl)
        {
            DateTime lastSdDlt = DateTime.UtcNow;
            int entropyAcc = 0;
            double sC;
            double ht0 = 0.01;
            double[] ht0Grph = [0.01, 0.1, 0.2, 0.3, 0.4,
            0.5, 0.6, 0.7, 0.8, 0.9,
            1.0, 1.05, 1.1, 1.15, 1.2, 1.25, 1.3,
            1.35, 1.4, 1.45, 1.5, 1.55, 1.6, 1.65,
            1.7, 1.75, 1.8, 1.85, 1.9, 1.95, 2.0,
            1.95, 1.9, 1.85, 1.8, 1.75, 1.7,
            1.65, 1.6, 1.55, 1.5, 1.45, 1.4,
            1.35, 1.3, 1.25, 1.2, 1.15, 1.1, 1.05, 1.0,
            0.9, 0.8, 0.7, 0.6, 0.5,
            0.4, 0.3, 0.2, 0.1, 0.01];
            int ht0Idx = 0;
            int lastNewsSecond = -1;
            int lastFsSaveSecond = -1;
            double spCnChnMl = 1;
            Stopwatch ptTrck = new();
            ptTrck.Start();
            while (true)
            {
                try
                {
                    Thread.Sleep(tl);

                    if (rbt0)
                    {
                        return;
                    }

                    sC = Math.Log10(schonite * 0.35);
                    sysstab = Math.Clamp(sC, 0.01, 50);
                    if (tick % mcl == 0)
                    {
                        munCycle += ht0;
                    }
                    if (tick % 10000 == 0 && !specialDays.bktDay)
                    {
                        entropyAcc++;
                    }
                    if (tick % 25000 == 0 && specialDays.bktDay)
                    {
                        entropyAcc++;
                    }
                    if (entropyAcc % 10 == 0 && rand.Next(0, 10) == 0)
                    {
                        entropyAcc -= rand.Next(0, 10);
                    }
                    if (lastSdDlt.Date != DateTime.UtcNow.Date)
                    {
                        specialDays.update();
                        lastSdDlt = DateTime.UtcNow;
                    }
                    if (tick % (tl * 60000) == 0)
                    {
                        specialDays.update();
                        lastSdDlt = DateTime.UtcNow;
                    }
                    if (tick > 6746518849) // note: each tick is usually 15ms
                    {
                        // meaning this would take roughly
                        // 3 years to reach.
                        // that's a long fucking time.
                        if (rand.Next(0, 8) == 0) // 1 in 8 chance 
                        {
                            Console.WriteLine("Howdy! Just checking in to see if you're mentally sane " +
                                "Judging by the fact that you've played a dumbass DOS sim for " +
                                $"roughly {(tick * tl) / 1000 / 60 / 60 / 24 / 7} weeks!");
                            addNews("Local person types commands in a terminal over and over for 3 years in a futile attempt to escape boredom!");
                            Console.WriteLine("Look, you're even on the news!. Check the news command.");
                            Console.WriteLine("P.S. I feel like the sim should've crashed before this message ever pops up due to a bunch of thread safety bugs");
                            Thread.Sleep(5000);
                        }
                    }
                    if (tick % 0xBAD1 == 0)
                    {
                        if (rand.Next(0, 1000) == 420)
                        {
                            Console.WriteLine("Oh noes! A terrible error has occurred!");
                            Console.WriteLine("Report this code to a support person: 0x2F282E2F");
                            AnsiConsole.MarkupLine("Occurred at: [green]BBCR[/]_403.DLL:9532");
                            Console.WriteLine("(technical: BADSUM.CHK. Oh wait did i swap the last 2 bits? I meant BADCHK.SUM. Shit)");
                            rbt0 = true;
                            while (true)
                            {
                                Console.TreatControlCAsInput = true;
                                Console.SetOut(TextWriter.Null);
                                Console.SetError(TextWriter.Null);
                                Thread.Sleep(2147);
                            }
                        }
                    }
                    if ((int)ptTrck.Elapsed.TotalSeconds % 120 == 0)
                    {
                        shitLog.createEntry("TICKER", $"Playtime is {(double)ptTrck.ElapsedMilliseconds / 1000:F2}s (i: {tick * tl / 1000}). Tick is {tick}. munCycle is {munCycle}. ht0 is {ht0}.", logType.Info);
                    }
                    if (DateTime.UtcNow.Second % cfg[2] == 0 &&
                        DateTime.UtcNow.Second != lastFsSaveSecond)
                    {
                        impulse(5001);
                        lastFsSaveSecond = DateTime.UtcNow.Second;
                    }
                    if (DateTime.UtcNow.Second % 10 == 0 &&
                        DateTime.UtcNow.Second != lastNewsSecond &&
                        rand.Next(0, 5) == 0)
                    {
                        addNews(newsGen.generateNws());
                        if (rand.Next(0, 98) == 0) // 1 in 99
                        {
                            // Schonite dust collector
                            schonite += Math.Clamp(rand.NextSingle(), 0.001, 500);
                            if (!specialDays.spaceDay)
                            {
                                schonite -= Math.Clamp(rand.NextSingle(), 0.001, 500);
                            }
                            else
                            {
                                schonite -= Math.Clamp(rand.NextSingle() * 0.1, 0.001, 100);
                            }
                        }
                        if (rand.Next(0, (int)Math.Clamp(10 * spCnChnMl, 10, 30)) == 0)
                        {
                            Console.Title = $"borktorial: {splashPick()}";
                            if (rand.Next(0, 69) == 0) // 1 in 69
                            {
                                Console.Title = $"broktorial: {splashPick()}";
                            }
                            if (spoopMode)
                            {
                                if (rand.Next(0, (int)Math.Ceiling(File.ReadAllLines("assets\\splashes.txt").Length * 2.5)) == 0)
                                {
                                    Console.Title = $"{username.ToUpper()} IS YOU!";
                                }
                            }
                            spCnChnMl += Math.Clamp(rand.NextDouble(), 0, 0.5);
                        }
                        if (tick % 2 == 0)
                        {
                            if (ht0Idx >= ht0Grph.Length - 1)
                            {
                                ht0Idx = 0;
                            }
                            ht0Idx++;
                            ht0 = (ht0Grph[ht0Idx] * 0.95) + (Math.Sin(ht0Idx) * 0.04) + (rand.NextDouble() * 0.01);
                        }
                        lastNewsSecond = DateTime.UtcNow.Second;
                    }
                    int baseValue = 5000;      // Starting risk
                    double scaleFactor = 50 + ((munCycle + 1) * 2) + entropyAcc;
                    if (specialDays.spaceDay)
                    {
                        scaleFactor = 35 + ((munCycle + 1) * 2) + (entropyAcc / 4);
                    }
                    double effectiveTick = tick * munCycle;

                    // note: the higher crshChance is the LOWER the chance of it crashing is
                    // due to me being horrible at coding
                    crshChance = Math.Max(10, baseValue - (int)(Math.Log10(effectiveTick + 1) * scaleFactor)) * (int)Math.Ceiling(sysstab);
                    tick++; // equivelant to i0
                }
                catch (Exception ex)
                {
                    shitLog.createEntry("TICKER", ex.ToString(), logType.Err);
                    // do a nice message that hides the exception just a lil' bit! make it seem in-universe!!!
                    Console.WriteLine($"SERVICES: Service \"bktTs.dll\" crashed. Message: {ex.Message}");
                    Thread.Sleep(250);
                    Console.WriteLine("SERVICES: Restarted Borktorial Ticker Service");
                    timeLoop(tl, mcl);
                }
            }
        }
        public static void interspeed()
        {
            while (true)
            {
                if (rbt0)
                {
                    return;
                }
                if (mConnected == true)
                {
                    if (!specialDays.spaceDay)
                    {
                        mSpeed += rand.Next(-mSpeed / 4, mSpeed / 4);
                        mSpeed -= rand.Next(-mSpeed / 4, mSpeed / 4);
                    }
                    else
                    {
                        mSpeed += rand.Next(-(mSpeed / 5), mSpeed / 5);
                        mSpeed -= rand.Next(-(mSpeed / 5), mSpeed / 5);
                    }
                    Thread.Sleep(rand.Next(650, 6000));
                    if (mSpeed <= 0)
                    {
                        mSpeed = 1;
                    }
                }
                else
                {
                    Thread.Sleep(8192);
                }
            }
        }
        public static void s49291()
        {
            while (true)
            {
                if (rbt0)
                {
                    return;
                }
                Thread.Sleep(rand.Next(5000, 30000));
                for (int i = 0; i < rand.Next(5, 50); i++)
                {
                    Thread.Sleep(50);
                    Console.SetCursorPosition(rand.Next(0, Console.BufferWidth), rand.Next(0, Console.BufferHeight));
                    Console.ForegroundColor = (ConsoleColor)rand.Next(0, 16);  // Random color each time
                    Console.BackgroundColor = (ConsoleColor)rand.Next(0, 16);  // Random color each time
                    AnsiConsole.Markup("[underline][blink][bold]NTISBTR[/][/][/]");
                }
                Console.ResetColor();
            }
        }
        public static void addNews(string news)
        {
            currNews.Add(news);

            if (currNews.Count > 5)
            {
                currNews.RemoveAt(0);
            }
        }
        public static void saveCfg()
        {
            string cfgS = "";
            foreach (int item in cfg)
            {
                cfgS += ";" + item;
            }
            cfgS = cfgS[1..];
            try
            {
                File.Delete(cfgFn);
            }
            catch (Exception ex)
            {
                shitLog.createEntry("SAVECFG", $"Error: {ex.Message} {ex.StackTrace}", logType.Err);
            }
            File.AppendAllText(cfgFn, cfgS);
        }
        public static string cmdMailEnc(string command, string message, int expDays)
        {
            string cmdMail = "C\x00"; // header (version C)
            cmdMail += $"{parseBorkTag(command)}\x00"; // command
            cmdMail += $"{username}\x00"; // username
            cmdMail += $"{DateTime.UtcNow.Date.Year:D4}";
            cmdMail += $"{DateTime.UtcNow.Date.Month:D2}";
            cmdMail += $"{DateTime.UtcNow.Date.Day:D2}\x00"; // datestamp
            cmdMail += $"{message}\x00"; // message
            cmdMail += $"{(ushort)(rSeed ^ DateTime.UtcNow.Day + DateTime.UtcNow.Month + (DateTime.UtcNow.Year / 100) + strSum(username + password + aprtMain.mkShitUsername(new Random(strSum("ACGCN.TOMNOOK_REDD_SAHARAH_JOAN_RESETTI")))))}";
            cmdMail += $"{(byte)rSeed & (9 + rSeed * 2) ^ 13}\x00"; // sid
            cmdMail += $"{expDays}\x00"; // expiry days
            cmdMail += $"{aprtMain.md5(aprtMain.str2Ba(cmdMail))}";
            return $"mail.{aprtMain.toB64(cmdMail)}";
        }
        public static DateTime ymd2Dt(string s)
        {
            int y = int.Parse(string.Join(string.Empty, s[0], s[1], s[2], s[3])); // beware of the year 10,000!
            int m = int.Parse(string.Join(string.Empty, s[4], s[5]));
            int d = int.Parse(string.Join(string.Empty, s[6], s[7]));
            return new DateTime(y, m, d);
        }
        public static (string username, string command, string message, string sid) cmdMailDec(string cmdMail)
        {
            cmdMail = cmdMail[5..];
            cmdMail = aprtMain.fromB64(cmdMail);
            string[] cmdMI = cmdMail.Split("\x00");
            if (cmdMI.Length < 7)
            {
                return ("\x07", "\x07", "\x07", "0");
            }
            if (cmdMI[0] != "C")
            {
                return ("\x07", "\x07", "\x07", "0");
            }
            DateTime dt = ymd2Dt(cmdMI[3]);
            if (dt < DateTime.UtcNow.Subtract(new TimeSpan(int.Parse(cmdMI[6]), 0, 0, 0)))
            {
                return ("\x00", "\x01", "\x02", "0");
            }
            if (dt > DateTime.UtcNow)
            {
                return ("\x02", "\x01", "\x00", "0");
            }
            string crcCm = $"{cmdMI[0]}\0{cmdMI[1]}\0{cmdMI[2]}\0{cmdMI[3]}\0{cmdMI[4]}\0{cmdMI[5]}\0{cmdMI[6]}\x0";
            if (cmdMI[7] != aprtMain.md5(aprtMain.str2Ba(crcCm)))
            {
                return ("\x06", "\x06", "\x06", "0");
            }
            return (cmdMI[2], cmdMI[1], cmdMI[4], cmdMI[5]);
        }
        public static void playModemSound()
        {
            new SoundPlayer("assets\\modem.wav").PlaySync();
        }
        public static string splashPick()
        {
            int mdWeight = 0;
            int snapWeight = 0;
            if (specialDays.spaceDay)
            {
                mdWeight = 4;
            }
            if (specialDays.snapshotDay)
            {
                snapWeight = 4;
            }
            if (specialDays.crimbus)
            {
                return "Merry xmas";
            }
            if (specialDays.spooky)
            {
                return "Ooooo";
            }
            if (specialDays.aprilfool)
            {
                return "Your car is on fire.";
            }
            string[] lines = File.ReadAllText("assets\\splashes.txt")
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            List<string> lList = [.. lines];
            lList.Remove("(c) This splash won't ever appear despite being marked as common. Isn't that weird?");
            if (File.Exists(Path.Combine("mods", "csplash.txt")))
            {
                string[] extraLines = File.ReadAllLines(Path.Combine("mods", "csplash.txt"));
                if (extraLines.Length > 0 && extraLines[0] == "[NOSTOCKSPLASH]")
                {
                    lList = [];
                }
                foreach (string item in extraLines)
                {
                    if (item == "[NOSTOCKSPLASH]")
                    {
                        continue;
                    }
                    if (item.StartsWith("[REMOVE] "))
                    {
                        lList.Remove(item[9..]);
                        lList.Remove(item); // just to be surely sure
                        continue; // skip it
                    }
                    if (item.StartsWith("//"))
                    {
                        continue; // skip this too
                    }
                    lList.Add(item);
                }
            }
            lines = [.. lList];
            // Parse lines with rarity weights
            List<(string Text, int Weight)> splashes = [.. lines
                .Select(line => line switch
                {
                    _ when line.StartsWith("(c) ") => (Text: line[4..], Weight: 4),
                    _ when line.StartsWith("(u) ") => (Text: line[4..], Weight: 2),
                    _ when line.StartsWith("(r) ") => (Text: line[4..], Weight: 1),
                    _ when line.StartsWith("(e) ") => (Text: line[4..], Weight: 0),
                    _ when line.StartsWith("(m) ") => (Text: line[4..], Weight: mdWeight),
                    _ when line.StartsWith("(s) ") => (Text: line[4..], Weight: snapWeight),
                    _ => (Text: line, Weight: 0)
                })];

            if (splashes.Count == 0)
            {
                return "404 splash not found";
            }

            // Weighted random selection
            int totalWeight = splashes.Sum(s => s.Weight);
            int roll = rand.Next(totalWeight);

            int cumulative = 0;
            foreach ((string Text, int Weight) splash in splashes)
            {
                cumulative += splash.Weight;
                if (roll < cumulative)
                {
                    return parseBorkTag(splash.Text);
                }
            }

            return parseBorkTag(splashes[^1].Text);
        }
        public static int getBuildNum()
        {
            return bktver + aprtMain.aprtVer;
        }
        public static void sttw()
        {
            Console.WriteLine("Now loading STTW...");
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream("borktorial.rsrc.sttw.txt") ?? throw new Exception("Error: Could not start STTW");
            using StreamReader reader = new(stream);
            string[] lines = reader.ReadToEnd()
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Console.Clear();
            foreach (string ln in lines)
            {
                Console.WriteLine(ln);
                Thread.Sleep(150);
            }

        }
        static void catGoBrr(int delay = 100)
        {
            int consoleWidth = Console.WindowWidth;
            string[] catLines = cat.Split("\r\n");

            int catWidth = 0;
            foreach (string line in catLines)
            {
                if (line.Length > catWidth)
                {
                    catWidth = line.Length;
                }
            }

            int startPos = consoleWidth - catWidth;
            int endPos = 0;

            for (int pos = startPos; pos >= endPos; pos--)
            {
                for (int y = 0; y < catLines.Length; y++)
                {
                    Console.SetCursorPosition(Math.Max(pos, 0), y);
                    string lineToPrint = catLines[y];

                    if (pos < 0)
                    {
                        lineToPrint = lineToPrint[-pos..];
                    }

                    Console.Write(lineToPrint);
                }
                Thread.Sleep(delay);

                for (int y = 0; y < catLines.Length; y++)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write(new string(' ', consoleWidth));
                }
            }
            Thread.Sleep(3500);
            Console.Clear();
            return;
        }
        /// <summary>
        /// Reset the state.
        /// </summary>
        public static void resetState()
        {
            jebconnect = false;
            mConnected = false;
            forceNoBoot = false;
            mSpeed = 1800;
            crshChance = 10000;
            currNews.Clear();
            virused = false;
            ballmerMode = false;
            gordonSummoned = File.Exists("GORDON"); // re-check
            jebcounter = 0;
            munCycle = 0;
            tick = 0;
            schonite = 1;
            sysstab = 1;
            username = "";
            password = "";
            root = false;
            jmtrigger = false;
            specialDays.update();
        }
        public static void playAnim(string[] frames, int delay, bool clrCns = true, int bg = 0, int fg = 15)
        {

            Console.BackgroundColor = (ConsoleColor)bg;
            Console.ForegroundColor = (ConsoleColor)fg;
            if (clrCns)
            {
                Console.Clear();
            }
            foreach (string item in frames)
            {
                Console.WriteLine(item.Replace("\0", "\r\n"));
                Thread.Sleep(delay);
                Console.Clear();
            }
            return;
        }
        public static string parseBorkTag(string exp)
        {
            exp = aprtMain.pNrH(exp, rand);
            exp = errGen.genCustomTemplate(exp)[0];
            exp = newsGen.genCustomTemplate(exp);
            exp = exp.Replace("<newline>", "\r\n");
            exp = exp.Replace("<empty>", "");
            return exp;
        }
        public static void forceDefaultCfg()
        {
            cfg = new int[256];
            cfg[0] = 15; // Tick length in ms
            cfg[1] = 10000; // Mun cycle length in ticks
            cfg[2] = 15; // FS save interval in seconds
            cfg[3] = getBuildNum(); // Version
            cfg[4] = 0; // Fail NTGINA find
            cfg[5] = 0; // No fun pre-logon boot text
            cfg[6] = 0; // No asking for username and password
            saveCfg();
        }
        public static class specialDays
        {
            // i'm sure there's some far cleaner way to do this but ehh
            public static bool aprilfool = DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 1;
            public static bool crimbus = DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day >= 25;
            public static bool spooky = DateTime.UtcNow.Month == 10 && DateTime.UtcNow.Day >= 24;
            public static bool seecretFriday = DateTime.UtcNow.DayOfWeek == DayOfWeek.Friday && DateTime.UtcNow.Day == 9;
            public static bool bktDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday &&
                DateTime.UtcNow.Day == 27 &&
                DateTime.UtcNow.Month == 9;
            public static bool marsDay =
                DateTime.UtcNow.Month == 8 &&
                DateTime.UtcNow.Day == 6 &&
                DateTime.UtcNow.DayOfWeek == DayOfWeek.Monday &&
                (DateTime.UtcNow.Hour > 5 ||
                 (DateTime.UtcNow.Hour == 5 && DateTime.UtcNow.Minute >= 17));
            public static bool snapshotDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Wednesday && (DateTime.UtcNow.Year + DateTime.UtcNow.Month - 2009) % 5 == 0;
            public static bool sputnikDay = DateTime.UtcNow.Day == 4
                && DateTime.UtcNow.Month == 10
                && (DateTime.UtcNow.Year - 1957) % 10 == 0;
            public static bool spaceDay = marsDay || sputnikDay;


            public static void update()
            {
                aprilfool = DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 1;
                crimbus = DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day >= 25;
                spooky = DateTime.UtcNow.Month == 10 && DateTime.UtcNow.Day >= 1;
                seecretFriday = DateTime.UtcNow.DayOfWeek == DayOfWeek.Friday && DateTime.UtcNow.Day == 9;
                bktDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday &&
                    DateTime.UtcNow.Day == 27 &&
                    DateTime.UtcNow.Month == 9;
                marsDay = DateTime.UtcNow.Month == 8 &&
                DateTime.UtcNow.Day == 6 &&
                DateTime.UtcNow.DayOfWeek == DayOfWeek.Monday &&
                (DateTime.UtcNow.Hour > 5 ||
                 (DateTime.UtcNow.Hour == 5 && DateTime.UtcNow.Minute >= 17));
                snapshotDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Wednesday;
                sputnikDay = DateTime.UtcNow.Day == 4
                && DateTime.UtcNow.Month == 10
                && (DateTime.UtcNow.Year - 1957) % 5 == 0;
            }
        }
        public enum mConnectTypes
        {
            Null,
            dUp,
            dupCompressed,
            sat,
            etr,
            dbg
        }
    }
}