using Microsoft.Win32;
using NAudio.Wave;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Spectre.Console;
using Microsoft.VisualBasic.Devices;
using aperture;
using System.Media;
using System.Windows.Forms;
using Panel = Spectre.Console.Panel;
using KeraLua;
namespace borktorial
{
    public class Program
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

        public static (int maj, int min, int pch, char rv) bktver { get; set; } = (0, 5, 7, 'b');
        public static (int maj, int min, int pch, char rv) pubver { get; set; } = (1, 2, 1, 'a');

        public static bool jebconnect { get; set; } = false;
        public static bool mConnected { get; set; } = false;
        public static bool forceNoBoot { get; set; } = false;
        public static bool failIntaAlways { get; set; } = false;
        public static bool virused { get; set; } = false;
        public static bool ballmerMode { get; set; } = false;
        public static bool gordonSummoned { get; set; } = File.Exists("GORDON");
        public static bool radioStopped { get; set; } = true;
        public static bool jmtrigger { get; set; } = false;
        public static bool root { get; set; } = false;

        public static bool __5a85 { get; } = OperatingSystem.IsWindows();

        public static int mSpeed { get; set; } = 1800;
        public static int crshChance { get; set; } = 10000;
        public static int jebcounter { get; set; } = 0;
        public static int tick { get; set; } = 0;
        public static double munCycle { get; set; } = 0;
        public static double ninovium { get; set; } = 1;
        public static double schonite { get; set; } = 1;
        public static double sysstab { get; set; } = 1;

        public static mConnectTypes mCt { get; set; } = mConnectTypes.Null;

        public static Random rand { get; set; } = new();
        public static Thread drdhtsr { get; set; }
        public static ComputerInfo compi { get; set; } = new(); // Was readonly, but object state is mutable
        public static RegistryKey formatkey { get; } = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\bkt\srga\fC"); // RegistryKey itself shouldn't be swapped

        public static string cfgFn { get; set; } = "bktcfg.ssc";
        public static string username { get; set; } = "";
        public static string password { get; set; } = "";

        public static List<string> currNews { get; set; } = new(4096);

        public static int[] cfg { get; set; } = [15, 10000, 15, 2];

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

        public static string JEBMSG { get; } = """
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
            OS: NTOSKRNL v4.0, NT-DOS v2.2
            Video: Citrus GT-6500 ISA
            Sound: PC beeper, SB1.0
            Other devices: GLaDOS Link Peripheral, Networked Microsystems 14400bps
            Network: {mConnected}. Use NETINFO for futher info
            Unknown: STANDARD ISA16 PERIPHERAL hooked onto int 5Fh.
            """;
        public static void publicMain(string[] mArgs)
        {
            resetState();
            Main(mArgs);
        }
        static void Main(string[] args)
        {
            // hide the init time away
            AnsiConsole.MarkupLine("[rgb(255,255,0)]Citrus[/] Emerald Sneak VGA BIOS...");
            Thread.Sleep(2000);
            AnsiConsole.MarkupLine("8192KB [green]OK[/]");
            AnsiConsole.MarkupLine("Card: [rgb(255,255,0)]Citrus[/] GT-6500 ISA");
            AnsiConsole.MarkupLine("Modes: CGA (T), CGA (G), EGA (T), EGA (G), VGA (T), VGA (G), VESA (T), VESA (G), [rgb(255,255,0)]Citrus[/] extensions");
            AnsiConsole.MarkupLine("");
            Thread.Sleep(5000);
            if(bktLV.aprtVer != (0, 4, 3, 'a'))
            {
                shitLog.createEntry("BOOT", $"APRT version mismatch. Expected 0.4.3a, got {bktLV.aprtVer.maj}.{bktLV.aprtVer.min}.{bktLV.aprtVer.pch}{bktLV.aprtVer.rv}", logType.Warn);
            }
            Stopwatch bootSw = new Stopwatch();
            bootSw.Start();
            Debug.WriteLine("tada!");
            if (args.Length >= 2 && args[0] == "bktint:delayStart")
            {
                Thread.Sleep(int.Parse(args[1]));
            }
            if (!__5a85)
            {
                rand = new Random(0x4E54);
            }
            try
            {
                if (File.Exists(cfgFn)) // Semicolon Separated Config
                {
                    shitLog.createEntry("CFGLDR", $"Loading {cfgFn}...", logType.Info);
                    string configC = File.ReadAllText(cfgFn);
                    string[] cfgR = configC.Split(";");
                    int[] cfgP = new int[256];
                    int iteration = 0;
                    foreach (var item in cfgR)
                    {
                        int itemI = int.Parse(item);
                        cfgP[iteration] = itemI;
                        iteration++;
                    }
                    cfg = cfgP;
                    if (cfg[3] != 2)
                    {
                        shitLog.createEntry("CFGLDR", $"{cfgFn} has wrong version", logType.Warn);
                    }
                    shitLog.createEntry("CFGLDR", "Config loading success!", logType.Info);
                }
                else
                {
                    shitLog.createEntry("CFGLDR", "No config found! Using default!", logType.Warn);
                    cfg = new int[256];
                    cfg[0] = 15; // Tick length in ms
                    cfg[1] = 10000; // Mun cycle length in ticks
                    cfg[2] = 15; // Unused (this used to be autosave interval but we don't have saves anymore)
                    cfg[3] = 2; // Version
                    cfg[4] = 0; // Fail NTGINA find
                    cfg[5] = 0; // No fun pre-logon boot text
                    cfg[6] = 0; // No asking for username and password
                    saveCfg();
                }
            }
            catch (Exception ex)
            {
                shitLog.createEntry("CFGLDR", $"Config error: {ex.Message} {ex.StackTrace}", logType.Err);
                File.AppendAllText(cfgFn, "5;100000;15;2");
            }

            if (args.Length >= 1 && args[0] == "/waluigi")
            {
                sf59("waluigi");
            }
            if (args.Length >= 1 && args[0] == "/igiulaw")
            {
                sf59("igiulaw");
            }
            if (args.Length >= 1 && args[0] == "/dev")
            {
                Console.WriteLine("DEVELOPERS DEVELOPERS DEVELOPERS\r\n" +
                    "-Steve Ballmer while running around on stage\r\n" +
                    "actively sweating and probably\r\n" +
                    "needing vocal cord surgery");
                ballmerMode = true;
                if(args.Length >= 2 && string.Join(' ', args).Contains("FORCENOBOOT"))
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
                if (args.Length >= 2 && string.Join(' ', args).Contains("FORCEINTAFAIL"))
                {
                    failIntaAlways = true;
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
            if (args.Length >= 1 && args[0] == "prop65")
            {
                int attemptsL = 0;
                while (true)
                {
                    Console.WriteLine("Please enter the code you obtained from DOHASHIDOSHAI\r\n");
                    Console.Write(">");
                    string theCode = Console.ReadLine();
                    if (theCode == "HU6UIRSPOU2UQQ2FJBDFMQKJIRLDIUSF")
                    {
                        sf59("luigi");
                    }
                    else
                    {
                        Console.WriteLine("Invalid code");
                        attemptsL++;
                    }
                    if (attemptsL == 5)
                    {
                        Console.WriteLine("HU6UIRSPOU2UQQ2FJBDFMQKJIRLDIUSF");
                    }
                }
            }
            if (args.Length >= 2 &&
                args[0] == "Twyndyllyngs" &&
                args[1] == "Euouae")
            {
                Console.WriteLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                Thread.Sleep(5000);
            }
            // Verification (and me just testing the lib)
            int[] avServer = bktLV.dallf();
            int[] avClient = new int[8];
            int[] avCv1 = [255, 127, 63, 31, 15, 7, 3, 2];
            int[] avCv2 = [1, 3, 7, 15, 31, 63, 127, 254];
            if (failIntaAlways)
            {
                avCv1[0] = int.MaxValue;
            }
            for (int i = 0; i < avClient.Length; i++)
            {
                avClient[i] = avCv1[i] + avCv2[i];
            }
            if (!avClient.SequenceEqual(avServer))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine("FAULT: The APERTURE system library is corrupted.");
                Console.WriteLine("Error code: 492. The INTA verification failed.");
                Console.WriteLine("This incident will be logged\r\n");
                Console.WriteLine("Potential reasons why you saw this error:");
                Console.WriteLine("  * You modified APERTURE.DLL in such a way that the verification failed");
                Console.WriteLine("  * The DLL is corrupted");
                Console.WriteLine("  * Your copy was tampered with somehow");
                Console.WriteLine("  * The program just hates you in particular\r\n");
                Console.WriteLine("Retry in 10 seconds.");
                shitLog.createEntry("INTA", "The INTA verification failed.", logType.Err);
                Thread.Sleep(10000);
                publicMain([]);
            }
            if (!forceNoBoot)
            {
                if (!ballmerMode) Console.Clear();
                Console.Title = $"borktorial: {splashPick()}";
                if(rand.Next(0, 69) == 0) // 1 in 69
                {
                    Console.Title = $"broktorial: {splashPick()}";
                }
                bootSw.Stop();
                shitLog.createEntry("Bootymcbootface", $"Init took {bootSw.ElapsedMilliseconds}ms!", logType.Info);
                Console.Clear();
                Console.WriteLine($"GLaBIOS 3.14 Revision 159 (build {getBuildNum()})");
                AnsiConsole.MarkupLine("(C) [lime]KSC[/] Computer Division and [blue]Aperture Science[/] 1984-1994");
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
                AnsiConsole.Markup("[green]ok![/]\r\n");
                if (File.Exists("temp_fcBA39-FA31.bin"))
                {
                    formatkey.SetValue("algl", "waluigi");
                    Console.Clear();
                    Console.WriteLine("\r\nNo boot device found.");
                    while (true)
                    {
                        Thread.Sleep(int.MaxValue);
                    }
                }
                if (formatkey.GetValue("algl") != null)
                {
                    if ((string)formatkey.GetValue("algl") == "waluigi")
                    {
                        Console.Clear();
                        Console.WriteLine("\r\nNo boot device found.");
                        while (true)
                        {
                            Thread.Sleep(int.MaxValue);
                        }
                    }
                }
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
                if (gordonSummoned || (!__5a85 && rand.Next(1, 5) == 0))
                {
                    Console.WriteLine("[WARN] 128 byte memory hole detected at 0x8086!");
                    Thread.Sleep(500);
                    ftlCrash(0x12345678, "MEM_DETECT_FAIL", "ntxmem", false);
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
                    "Re-entering atmosphere..."
                    ];
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
                if (cfg[6] == 0)
                {
                    while (string.IsNullOrWhiteSpace(username))
                    {
                        Console.Write("Username: ");
                        username = Console.ReadLine()?.Trim() ?? "";
                    }

                    while (string.IsNullOrWhiteSpace(password))
                    {
                        Console.Write("Password: ");
                        password = Console.ReadLine() ?? "";
                    }

                    //if (username == "root" && password == "Bacon532!") // this makes no sense on NT
                    //{
                    //    root = true;
                    //}
                    if (username == "SYSTEM" && rand.Next(0, 37) == 0)
                    {
                        root = true;
                    }
                }
                Thread timeThread = new(() =>
                {
                    timeLoop(cfg[0], cfg[1]);
                });
                timeThread.Start();
                if (!__5a85 || specialDays.aprilfool)
                {
                    Thread __58858g = new(__49291);
                    __58858g.Start();
                }
                Thread msVarier = new(interspeed);
                msVarier.Start();
                Console.WriteLine("NT-DOS is loading shell \"TW8000.EXE\"...");
                Thread.Sleep(rand.Next(750, 1500));
                Console.WriteLine("\r\nWelcome to the Time-Waster 8000!");
                if (specialDays.bktDay)
                {
                    Console.Write(" Happy Borktorial Day!\r\n");
                }
            }
            // initialize news feed
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    addNews(newsGen.Generate());
                }
            }
            catch(IndexOutOfRangeException) { currNews.Clear(); }

            while (true)
            {
                Console.Write("C:\\TW8000\\>");
                string rawCommin = Console.ReadLine() ?? "";
                string[] commin = rawCommin.ToLower().Split(' ');
                try
                {
                    switch (commin[0])
                    {
                        case "echo":
                            if (commin.Length > 1)
                                Console.WriteLine(string.Join(" ", rawCommin.Split(' ').Skip(1)));
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
                                    ftlCrash(0xDEADDEAD, "THE_PART_WHERE_HE_KILLS_YOU", "PORTAL2", false);
                                }
                            }
                            break;
                        case "hl3":
                            Console.WriteLine("HALF-LIFE 3 CONFIRMED");
                            break;
                        case "dir":
                            Console.WriteLine("Volume Serial Number is 4655-434B");
                            Console.WriteLine("Directory listing of C:");
                            for (int i = 0; i < rand.Next(4, 21); i++)
                            {
                                Console.WriteLine($"    {generateFile()} - {rand.Next(512, 65536)}");
                            }
                            Console.WriteLine();
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
                                                    if (rand.Next(1, 256) == 255 && (!specialDays.marsDay))
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
                            if (virused == false)
                            {
                                publicMain(["vs", "49"]);
                            }
                            if (virused == true)
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Non-system disk or disk error.");
                                    Console.WriteLine("Press any key to reboot.");
                                    Console.ReadKey(true);
                                }
                            }
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
                        case "win":
                            Console.WriteLine("Failed to load VMM32.VXD. You must reinstall Windows");
                            break;
                        case "dbg::spectre::console":
                            AnsiConsole.MarkupLine("[blue][bold]Test![/][/]");
                            AnsiConsole.Progress()
                                .Start(ctx =>
                                {
                                    var task = ctx.AddTask("[rgb(63,127,255)]Doing the thing...[/]");

                                    while (!task.IsFinished)
                                    {
                                        task.Increment(5);
                                        Thread.Sleep(100);
                                    }
                                });
                            AnsiConsole.MarkupLine("[rgb(255,255,255) on rgb(0,0,255)]Dingus[/]");
                            AnsiConsole.Live(new Panel("Starting..."))
                                .Start(ctx =>
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        ctx.UpdateTarget(new Panel($"[cyan]Tick: {i}[/]\n\nRandom: {rand.Next()}"));
                                        Thread.Sleep(500);
                                    }

                                    ctx.UpdateTarget(new Panel("[green]Done![/]"));
                                    Thread.Sleep(1000);
                                });
                            break;
                        case "dbg::perftest::errgen_sf15":
                            Stopwatch sow = new();
                            sow.Start();
                            for (int i = 0; i < 1024; i++)
                            {
                                string bigLoadOfString = errGen.sf15(1048576, 16, '-');
                            }
                            sow.Stop();
                            Console.WriteLine($"Operation took {sow.ElapsedMilliseconds}ms!");
                            break;
                        case "flush":
                            crshChance = 5000;
                            munCycle = 0;
                            tick = 0;
                            Console.WriteLine("System flush successful.");
                            break;
                        case "ninov":
                            Console.WriteLine("Ninovium is a resource used for system stabilization and minor performance gains\r\n" +
                                "Help:\r\n\r\n" +
                                "mine: Mine ninovium and add to bank\r\n" +
                                "dispose: Dispose all ninovium (WARNING: MAY LEAD TO SYSTEM INSTABILITY)\r\n" +
                                "reinsert: Remove and re-add ninovium\r\n" +
                                $"\r\n{ninovium} ninovium cubes in bank resulting in a stability factor of {sysstab:F2}!" +
                                "\r\nWARNING: Dingus Solutions. Inc is not responsible for any spontoneus human combustion from ninovium usage");
                            if(commin.Length >= 2)
                            {
                                switch (commin[1])
                                {
                                    case "mine":
                                        AnsiConsole.Progress()
                                        .Start(ctx =>
                                        {
                                            // Define tasks
                                            var task1 = ctx.AddTask("[green]Mining...[/]");
                                            var task2 = ctx.AddTask("[green]Inserting...[/]");

                                            while (!ctx.IsFinished)
                                            {
                                                Thread.Sleep(rand.Next(5, 15));
                                                task1.Increment(rand.Next(1, 3));
                                                Thread.Sleep(rand.Next(5, 15));
                                                task2.Increment(rand.Next(1, 3));
                                            }
                                        });
                                        ninovium += 1;
                                        break;
                                    case "dispose":
                                        AnsiConsole.Progress()
                                        .Start(ctx =>
                                        {
                                            // Define tasks
                                            var task1 = ctx.AddTask("[green]Disposing...[/]");

                                            while (!ctx.IsFinished)
                                            {
                                                Thread.Sleep(rand.Next(3, 5));
                                                task1.Increment(rand.Next(3, 5));
                                            }
                                        });
                                        ninovium = 0;
                                        AnsiConsole.Progress()
                                        .Start(ctx =>
                                        {
                                            // Define tasks
                                            var task1 = ctx.AddTask("[green]Resting to recover stability...[/]");

                                            while (!ctx.IsFinished)
                                            {
                                                Thread.Sleep(rand.Next(3, 5));
                                                task1.Increment(rand.Next(3, 5));
                                            }
                                        });
                                        break;
                                    case "reinsert":
                                        AnsiConsole.Progress()
                                        .Start(ctx =>
                                        {
                                            // Define tasks
                                            var task1 = ctx.AddTask("[green]Reinserting...[/]");

                                            while (!ctx.IsFinished)
                                            {
                                                Thread.Sleep(rand.Next(5, 15));
                                                task1.Increment(rand.Next(3, 5));
                                            }
                                        });
                                        break;
                                }
                            }
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
                            string userNums = Console.ReadLine().Replace("-", "").ToUpper() ?? "";
                            string actual;
                            do
                            {
                                actual = errGen.sf15(16, 4).Replace("-", "").ToUpper();
                            } while (userNums == actual);
                            Console.WriteLine($"Actual numbers were {actual}");
                            break;
                        case "shutdown":
                            Environment.Exit(0);
                            break; // this is unreachable code but the CSC needs it to compile
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
                                Console.WriteLine(JEBMSG);
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
                                    case "p32krnl":
                                        ftlCrash(0xE000002c, "FATAL_PROCESS_CRASHED", "P32KRNL", false);
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
                                            ftlCrash(0xE000002c, "FATAL_PROCESS_CRASHED", commin[1], true);
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
                                throw new Exception("fuck image gen ai and all the ones intended to replace writers or programmers or some shit", new Exception($"{errGen.Generate()[0]} -- {errGen.Generate()[1]}"));
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
                        case "help":
                            Console.WriteLine("Available commands:");
                            Console.WriteLine("  echo <text>               - Print text to the screen.");
                            Console.WriteLine("  dir                       - List files in the current directory.");
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
                            Console.WriteLine("  dohashidoshai             - Print THE CODE");
                            Console.WriteLine("  satconnect                - Connect to satellite internet");
                            Console.WriteLine("  format                    - Format drive");
                            Console.WriteLine();
                            Console.WriteLine("For extra fun, try exploring on your own. Some secrets are hidden! e.g a very certain pilot kerbal. \r\n" +
                                "\r\nNote: Call 1-800-intnet for free internet");
                            break;
                        case "dohashidoshai!":
                            Console.WriteLine("HU6UIRSPOU2UQQ2FJBDFMQKJIRLDIUSF");
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
                                            Console.Write($"Sector {i.ToString("D4")}/1440...");
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
                                            Console.Write($"Sector {i.ToString("D4")}/1440...");
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
                                                    Console.Write($"Sector {i.ToString("D7")}/1228800...");
                                                    Thread.Sleep(rand.Next(10, 50));
                                                    Console.Write("Done\r\n");
                                                    Thread.Sleep(rand.Next(5, 15));
                                                    if (i > 485824)
                                                    {
                                                        File.AppendAllText($"temp_fcBA39-FA31.bin", errGen.sf15(8192, 0));
                                                        publicMain(["BABOON", "LAGOON"]);
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
                        case "check_unknown_ints":
                            Console.WriteLine("[INT 5Fh] Link to Kerbal Space Center success!");
                            jebconnect = true;
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
                                        PlayModemSound();
                                        Console.WriteLine("Connected to Fuckston Communications Services!");
                                        mSpeed = 1800;
                                        mConnected = true;
                                        if (specialDays.marsDay)
                                        {
                                            mSpeed += (mSpeed/4);
                                        }
                                        mCt = mConnectTypes.dUp;
                                        break;
                                    case "1-800-fastnet":
                                        Console.WriteLine("Dialing...");
                                        PlayModemSound();
                                        Console.WriteLine("Connected to Aperture V.32bis-compressed");
                                        mSpeed = 2000; // 16000bps
                                        mConnected = true;
                                        if (specialDays.marsDay)
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
                            if (specialDays.marsDay)
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
                            Console.Write($"Found sat group: {errGen.sf15(8, 4)}\r\n");
                            Thread.Sleep(rand.Next(1000, 2001));
                            mSpeed = rand.Next(51200, 153601);
                            mConnected = true;
                            if (specialDays.marsDay)
                            {
                                mSpeed += (mSpeed / 4);
                            }
                            mCt = mConnectTypes.sat;
                            Console.Write($"Connected! Speed: {(float)mSpeed / (float)1024:F2}KB/s\r\n");
                            break;
                        case "This_command_is_not_actually_accessible_under_NORMAL_Cir**CUM**stances_**LOL**":
                            File.Create("GORDON").Dispose();
                            ftlCrash(0xCAFEBABE, "Woah, how did you access that?", "surprised-pikachu.jpg", false);
                            break;
                        case "":
                            break;
                        case "drinkfood":
                            Console.TreatControlCAsInput = true;
                            // just like real psychadelics, it's fun for a bit
                            // and then it fucking obiliterates everything
                            File.AppendAllText(cfgFn, ";1");
                            while (true)
                            {
                                Console.SetCursorPosition(rand.Next(0, Console.BufferWidth), rand.Next(0, Console.BufferHeight));
                                AnsiConsole.Markup($"[rgb({rand.Next(0,256)},{rand.Next(0, 256)},{rand.Next(0, 256)}) on rgb({rand.Next(0, 256)},{rand.Next(0, 256)},{rand.Next(0, 256)})][blink][bold]?[/][/][/]");
                                //Console.Title += (char)rand.Next(32, 256);
                                //if(Console.Title.Length > 32)
                                //{
                                //    Console.Title = "";
                                //}
                                //if (rand.Next(0, 65536) == 0)
                                //{
                                //    Console.Clear();
                                //}
                            }
                        case "logtesto":
                            Exception iex = new("DOHASHIDOSHAI");
                            throw new Exception("BORKYBORK", iex);
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
                                    Console.WriteLine($"cannot find: {commin[0]}");
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
                        case "lplay_dbg":
                            mp3PlayLoop(@"C:\Program Files (x86)\Steam\steamapps\music\Half-Life Soundtrack\01 Adrenaline Horror.mp3");
                            break;
                        case "dbg::exhndlr":
                            for (int i = 10 - 1; i >= 0; i--)
                            {
                                Console.WriteLine(69 / i);
                            }
                            Console.WriteLine(cfg[593]);
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
                            Console.WriteLine("nano-penis");
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
                                foreach (var item in currNews)
                                {
                                    newsSizeSum += item.Length;
                                }
                                Console.WriteLine($"Fetching news (size: {bktStf.byteFormat((ulong)newsSizeSum)})...");
                                if (!mConnected)
                                {
                                    Console.WriteLine("Please connect to the Internet.");
                                    break;
                                }
                                Thread.Sleep(newsSizeSum / mSpeed);
                                foreach (var item in currNews)
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
                            if (!specialDays.marsDay)
                            {
                                Console.WriteLine("Now listening: 89.25MHz. The Human Music");
                            }
                            else
                            {
                                Console.WriteLine("Now listening: 195.25MHz. Duna Radio Broadcasting");
                            }
                                sf59("msc_canyon");
                            Thread rLoop = new(() =>
                            {
                                radioLoop("rd0.wav");
                            });
                            rLoop.Start();
                            radioStopped = !radioStopped;
                            break;
                        case "the_most_useless_command_ever":
                            using (WebClient client = new())
                            {
                                // i just host win95 RTM on floppy on my github pages, let's hope it doesn't instantly crash
                                string win95Link = "https://osintdoesstuff.github.io/webodingus/win95.7z";
                                Console.WriteLine("Downloading Windows 95...");
                                client.DownloadFile(win95Link, "win95.7z");
                                Console.WriteLine("Successfully downloaded.");
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = "win95.7z",
                                    UseShellExecute = true
                                });
                            }
                            break;
                        case "msgbox":
                            MessageBox.Show("THE MAGIC OF WINDOWS FORMS!", "bkt",
                                MessageBoxButtons.OK, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly,
                                false);
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
                            if(mCt == mConnectTypes.Null)
                            {
                                mCtStr = "none";
                            }
                            else if(mCt == mConnectTypes.dUp)
                            {
                                mCtStr = "dialup";
                            }
                            else if(mCt == mConnectTypes.dupCompressed)
                            {
                                mCtStr = "compressed dialup";
                            }
                            else if(mCt == mConnectTypes.sat)
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
                                    $"speed: {bktStf.byteFormat((UInt128)iMspeed)}/s\r\n" +
                                    $"connected: yes\r\n" +
                                    $"variance range: -{bktStf.byteFormat((UInt128)(iMspeed/4.5))}/s to {bktStf.byteFormat((UInt128)(iMspeed / 4.5))}/s"
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
                            catch(Exception ex)
                            {
                                if(ex.Message == "NO HOPIUM LEFT!!!")
                                {
                                    infLoop();
                                    void infLoop()
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
                            Console.WriteLine($"{DateTime.UtcNow.ToString("R")} BT:{tick}-BMC:{munCycle}");
                            break;
                        default:
                            string scriptName = Path.Combine("mods", commin[0] + ".lua");
                            if (File.Exists(scriptName))
                            {
                                using (var lua = new NLua.Lua())
                                {
                                    lua.LoadCLRPackage();

                                    try
                                    {
                                        var luAsm = Assembly.GetExecutingAssembly().GetName().Name;

                                        var lut = typeof(Program).FullName;

                                        lua.DoString($@"
                                            luanet.load_assembly('{luAsm}')
                                            Sys = luanet.import_type('{lut}')
                                        ");

                                        if (lua["Sys"] == null)
                                        {
                                            shitLog.createEntry("LUALDR", "Failed to load ASM. Sys was null.", logType.Err);
                                        }
                                        else
                                        {
                                            lua["Args"] = string.Join(" ", commin);
                                            lua.DoFile(scriptName);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        shitLog.createEntry("LUALDR", ex.ToString(), logType.Err);
                                        throw;
                                    }
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
                    string[] errG = errGen.Generate();
                    uint errCode = (uint)rand.Next(); // DONTFIXME: The values this shit returns are probably gonna be pretty fuckin' funny
                    ftlCrash(errCode, errG[0], errG[1], false);
                }
            }
        }
        static string generateFile()
        {
            string[] filenames = [
                "AUTOEXEC", "CONFIG", "COMMAND", "BOOTLOG", "SYSTEM", "HIMEM", "MSCDEX", "SMARTDRV",
                "GORDON", "FREEMAN", "CROWBAR", "LAMBDA", "COMBINE", "CITADEL", "VORTIG", "HEADCRAB",
                "KERBAL", "JOOL", "KERBIN", "MINOS", "DUNA", "EVE", "MOHO", "EELOO",
                "DRES", "GILLY", "IKE", "LAYTHE", "VALL", "TYLO", "BOP", "POL",
                "ROCKET", "ORBIT", "STAGING", "THRUST", "DELTAV", "APOAPSI", "PERIAPS", "MANEUVER",
                "TIMEWAST", "BORK", "ZOMBO", "LOADING", "PROCESS", "BIGMATH", "WINNT", "GMOD",
                "BRITISH", "COLONY", "SEVENHW", "WASTING", "WELCOME", "STEALING", "PLAYING", "IMPORT",
                "APPLE", "BAILOUT", "MICRO", "IPHONE", "TIMELINE", "ALTERNAT", "COMPETE", "INNOVATE",
                "BUTTONS", "DESTROY", "EXPLODE", "TERRAFORM", "INVADE", "ALIEN", "COMBINE2", "EARTH",
                "MARS", "SOLAR", "SYSTEM", "MAGNETS", "ATMOSPH", "OCEANS", "KNOWLEDG", "PRESERV",
                "MAGENTA", "CYAN", "YELLOW", "BLACK", "RED", "GREEN", "BLUE", "WHITE",
                "REDDER", "TEAL", "WINDOWS", "PLANET", "UNIVERSE", "FORGET", "MEMORY", "COLLECT",
                "ECHO", "CONSOLE", "DEBUG", "PATCH", "QUICK", "DIRTY", "LOGIC", "ISSUE",
                "LOWER", "CASE", "SPLIT", "ARRAY", "STRING", "PROPER", "EFFORT", "COST",
                "BENEFIT", "JANK", "WORKS", "FINE", "MASTER", "PIECE", "SHORTCT", "PERFECT",
                "ENOUGH", "THINK", "HANDLE", "ALTERN", "LIBERAT", "SATISF", "POINT", "ACKNOWLE"
            ];
            string[] extensions = [
                "exe", "dll", "sys", "ini", "cfg", "log", "tmp", "bak", "old", "new", "com", "bat", "cmd", "txt", "dat",
                "cpp", "hpp", "js", "py", "cs", "vb", "php", "sql", "xml", "htm", "css", "jar", "war", "zip", "rar",
                "jpg", "png", "gif", "bmp", "ico", "svg", "tga", "psd", "raw", "dds", "pcx", "tif", "webp", "jfif", "exr",
                "wav", "mp3", "ogg", "mid", "mod", "s3m", "xm", "it", "flac", "aac", "wma", "m4a", "opus", "ac3", "dts",
                "avi", "mov", "mp4", "wmv", "flv", "mkv", "webm", "ogv", "3gp", "asf", "rm", "vob", "ts", "m2v", "divx",
                "doc", "pdf", "rtf", "odt", "wpd", "xls", "ods", "csv", "ppt", "odp", "ttf", "otf", "fon", "eot", "woff",
                "hlf", "grd", "cwb", "lmb", "cmb", "ctd", "vtg", "hcr", "alx", "zen", "xen", "vrt", "res", "gma", "npc",
                "ksp", "orb", "rkt", "sta", "thu", "del", "apo", "per", "man", "sfs", "vab", "sph", "mis", "sci", "rep",
                "wst", "bor", "zom", "lod", "pro", "big", "win", "gmo", "dos", "ret", "tim", "was", "ste", "par", "sev",
                "btn", "red", "grn", "blu", "cyn", "mag", "yel", "blk", "wht", "tel", "w95", "dre", "unv", "exp", "ter",
                "ech", "con", "dbg", "pat", "qck", "drt", "lgc", "isu", "low", "cas", "spl", "arr", "str", "prp", "eff",
                "cos", "ben", "jnk", "wrk", "fin", "mas", "pce", "sho", "per", "ack", "lib", "sat", "poi"
            ];
            string fn = filenames[rand.Next(0, (filenames.Length - 1))];
            string ext = extensions[rand.Next(0, (extensions.Length - 1))];
            string fullFn = $"{fn}.{ext}";
            return fullFn;

        }
        static bool stopTsr = false;

        static void drdickhead_tsr()
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
        static void ftlCrash(uint errCode, string errName, string processName, bool recoverable)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Fatal error has occurred. Technical details:\r\n");
            Console.WriteLine($"{errCode} - {errName} - {processName}");
            Console.WriteLine("Technical-er details:\r\n");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($"FAIL AT: ADDR={rand.Next(1048576, 8388608):D8}:DATA={rand.Next(0, 255):D3}");
            }
            if (rand.Next(0, 1000000) == 420)
            {
                Console.WriteLine("FATAL: IMMORTAL TIGER broke containment");
            }
            if (!recoverable)
            {
                Console.WriteLine("Restart your system. If this happens again, contact system administrator");
            }
            else
            {
                Console.WriteLine("This error is potentially recoverable. Press any key to attempt to recover.");
                Console.ReadKey(false);
                int rcvrable2 = rand.Next(0, 2);
                // Russian Rolutte. With a if-else statement
                if (rcvrable2 == 0)
                {
                    publicMain(["vs", "49"]);
                }
                else
                {
                    // Cave Johnson built this self-referential crash function in a cave! With a copy of Visual Studio 2022!
                    ftlCrash((uint)rand.Next(), "UNKNOWN_ERROR", "ERRHNDLR.SYS", false);
                    // But sir, i am not Cave Johnson
                }
            }

            while (true)
            {
                Thread.Sleep(int.MaxValue);
            }
        }
        static void sf59(string code)
        {
            var secrets = new Dictionary<string, (string resource, string filename)>
            {
                ["waluigi"] = ("borktorial.rsrc.screenshot16.png", "the mun awaits.png"),
                ["igiulaw"] = ("borktorial.rsrc.eula.txt", "eula.txt"),
                ["luigi"] = ("borktorial.rsrc.thisisabucket.7z",
                             "THIS 7ZIP FILE MAY CAUSE CANCER OR REPRODUCTIVE HARM IN THE STATE OF CALIFORNIA.7z"),
                ["msc_canyon"] = ("borktorial.rsrc.rd_canyon.wav", "rd0.wav")
            };

            if (!secrets.TryGetValue(code, out var secret))
                return;

            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(secret.resource);
            using var ms = new MemoryStream();
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
        static void impulse(int num) {
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
                            if(hopium <= 0)
                            {
                                throw new Exception("NO HOPIUM LEFT!!!");
                            }
                            if(hopium < 3)
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
                            Thread.Sleep(20+(int)(Math.Log10(f22Raptor.Length)));
                            
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
                default:
                    break;
            }
            return;
        }
        static void mp3PlayLoop(string path)
        {
            while (true)
            {
                using (var mp3Reader = new Mp3FileReader(path))
                using (var waveOut = new WaveOutEvent())
                {
                    waveOut.Init(mp3Reader);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }
        static void radioLoop(string fn)
        {
            SoundPlayer radio1 = new SoundPlayer(fn);
            while (!radioStopped)
            {
                radio1.PlaySync();
            }
        }
        /// <summary>
        /// The Borktorial Server Thread.
        /// This along with a whole bunch of other shit constitues the Borktorial Server (or "master")
        /// The "client" or "slave" uses all this shit to do stuff
        /// </summary>
        /// <param name="tl">Tick Length</param>
        /// <param name="mcl">Mun Cycle Length</param>
        /// <exception cref="Exception">General shit went wrong</exception>
        static void timeLoop(int tl, int mcl)
        {
            DateTime lastSdDlt = DateTime.UtcNow;
            int entropyAcc = 0;
            double sC = Math.Log10((ninovium * 0.65) + (schonite * 0.35));
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

            while (true)
            {
                Thread.Sleep(tl);
                
                sC = Math.Log10((ninovium * 0.65) + (schonite * 0.35));
                sysstab = Math.Clamp(sC, 0.01, 50);
                if (tick % mcl == 0)
                {
                    munCycle+=ht0;
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
                if(lastSdDlt.Date != DateTime.UtcNow.Date)
                {
                    specialDays.update();
                    lastSdDlt = DateTime.UtcNow;
                }
                if (tick % (tl*60000) == 0)
                {
                    specialDays.update();
                    lastSdDlt = DateTime.UtcNow;
                }
                if (tick == int.MaxValue - 1)
                {
                    throw new Exception("[TIMETHRD] Stop bro go touch some fuckin' grass");
                }
                if (DateTime.UtcNow.Second % 10 == 0 &&
                   DateTime.UtcNow.Second != lastNewsSecond &&
                   rand.Next(0, 5) == 0)
                {
                    addNews(newsGen.Generate());
                    if (rand.Next(0, 98) == 0) // 1 in 99
                    {
                        // Schonite dust collector
                        schonite += Math.Clamp(rand.NextSingle(), 0.001, 500);
                        if (!specialDays.marsDay)
                        {
                            schonite -= Math.Clamp(rand.NextSingle(), 0.001, 500);
                        }
                        else
                        {
                            schonite -= Math.Clamp(rand.NextSingle()*0.1, 0.001, 100);
                        }
                    }
                    if (rand.Next(0, 99) == 0)
                    {
                        Console.Title = $"borktorial: {splashPick()}";
                        if (rand.Next(0, 69) == 0) // 1 in 69
                        {
                            Console.Title = $"broktorial: {splashPick()}";
                        }
                    }
                    if (tick % 4 == 0)
                    {
                        if(ht0Idx > ht0Grph.Length - 1)
                        {
                            ht0Idx = 0;
                        }
                        ht0Idx++;
                        ht0 = (ht0Grph[ht0Idx] * 0.95) + (Math.Sin(ht0Idx) * 0.05);
                    }
                    lastNewsSecond = DateTime.UtcNow.Second;
                }
                int baseValue = 5000;      // Starting risk
                double scaleFactor = 50 + ((munCycle + 1) * 2) + entropyAcc;
                if (specialDays.marsDay)
                {
                    scaleFactor = 35 + ((munCycle + 1) * 2) + (entropyAcc / 4);
                }
                double effectiveTick = tick * munCycle;

                crshChance = Math.Max(10, baseValue - (int)(Math.Log10(effectiveTick + 1) * scaleFactor))*(int)Math.Ceiling(sysstab);
                tick++; // equivelant to i0
            }
        }
        static void interspeed()
        {
            while (true)
            {
                if (mConnected == true)
                {
                    if (!specialDays.marsDay)
                    {
                        mSpeed += rand.Next(-((int)mSpeed / 4), ((int)mSpeed / 4));
                        mSpeed -= rand.Next(-((int)mSpeed / 4), ((int)mSpeed / 4));
                    }
                    else
                    {
                        mSpeed += rand.Next(-((int)mSpeed / 5), ((int)mSpeed / 5));
                        mSpeed -= rand.Next(-((int)mSpeed / 5), ((int)mSpeed / 5));
                    }
                    Thread.Sleep(rand.Next(650, 6000));
                    if(mSpeed <= 0)
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
        static void __49291()
        {
            while (true)
            {
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
                currNews.RemoveAt(0); // Remove first/oldest
            }
        }
        public static void saveCfg()
        {
            string cfgS = "";
            foreach(var item in cfg)
            {
                cfgS += (";" + item.ToString("D10"));
            }
            cfgS = cfgS.Remove(0, 1);
            try
            {
                File.Delete(cfgFn);
            }
            catch(Exception ex)
            {
                shitLog.createEntry("SAVECFG", $"Error: {ex.Message} {ex.StackTrace}", logType.Err);
            }
            File.AppendAllText(cfgFn, cfgS);
        }
        public static void PlayModemSound()
        {
            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("borktorial.rsrc.modem.wav");
            new System.Media.SoundPlayer(stream).PlaySync();
        }
        public static string splashPick()
        {
            int mdWeight = 0;
            if (specialDays.marsDay)
            {
                mdWeight = 4;
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
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("borktorial.rsrc.splashes.txt");

            if (stream is null)
                return "missingno";

            using var reader = new StreamReader(stream);
            var lines = reader.ReadToEnd()
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            // Parse lines with rarity weights
            var splashes = lines
                .Select(line => line switch
                {
                    _ when line.StartsWith("(c) ") => (Text: line[4..], Weight: 4),
                    _ when line.StartsWith("(u) ") => (Text: line[4..], Weight: 2),
                    _ when line.StartsWith("(r) ") => (Text: line[4..], Weight: 1),
                    _ when line.StartsWith("(e) ") => (Text: line[4..], Weight: 0),
                    _ when line.StartsWith("(m) ") => (Text: line[4..], Weight: mdWeight),
                    _ => (Text: line, Weight: 0)
                })
                .ToList();

            if (splashes.Count == 0)
                return "Beta than ever!";

            // Weighted random selection
            int totalWeight = splashes.Sum(s => s.Weight);
            int roll = rand.Next(totalWeight);

            int cumulative = 0;
            foreach (var splash in splashes)
            {
                cumulative += splash.Weight;
                if (roll < cumulative)
                    return parseBorkTag(splash.Text);
            }

            return parseBorkTag(splashes[^1].Text);
        }
        public static int getBuildNum()
        {
            float accu = 0;
            accu += (bktver.maj + pubver.maj) * 8;
            accu += bktLV.aprtVer.maj * 7;
            accu += (bktver.min + pubver.min) * 4;
            accu += bktLV.aprtVer.min * 5;
            accu += (bktver.pch + pubver.pch) * 2;
            accu += bktLV.aprtVer.pch * 3;
            accu += (bktver.rv + pubver.rv);
            accu += bktLV.aprtVer.rv;
            accu += bktLV.puD[7] / bktLV.puC[7];
            accu /= 4;
            if (specialDays.seecretFriday)
            {
                accu += 1;
            }
            if (specialDays.marsDay)
            {
                accu -= 1;
            }
            return (int)accu;
        }
        static void catGoBrr(int delay = 100)
        {
            int consoleWidth = Console.WindowWidth;
            string[] catLines = cat.Split("\r\n");

            int catWidth = 0;
            foreach (var line in catLines)
                if (line.Length > catWidth)
                    catWidth = line.Length;

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
                        lineToPrint = lineToPrint.Substring(-pos);
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
        static void resetState()
        {
            jebconnect = false;
            mConnected = false;
            forceNoBoot = false;
            failIntaAlways = false;
            mSpeed = 1800;
            crshChance = 10000;
            currNews.Clear();
            virused = false;
            ballmerMode = false;
            gordonSummoned = File.Exists("GORDON"); // re-check
            jebcounter = 0;
            munCycle = 0;
            tick = 0;
            ninovium = 1;
            schonite = 1;
            sysstab = 1;
            username = "";
            password = "";
            root = false;
            jmtrigger = false;
            specialDays.update();
        }
        public static void playAnim(string[] frames, int delay, bool clrCns=true, int bg=0, int fg=15)
        {

            Console.BackgroundColor = (ConsoleColor)bg;
            Console.ForegroundColor = (ConsoleColor)fg;
            if (clrCns)
            {
                Console.Clear();
            }
            foreach(var item in frames)
            {
                Console.WriteLine(item.Replace("\0", "\r\n"));
                Thread.Sleep(delay);
                Console.Clear();
            }
            return;
        }
        public static string parseBorkTag(string exp) 
        {
            exp = bktStf.pNrH(exp, rand);
            exp = errGen.genCustomTemplate(exp)[0];
            exp = newsGen.genCustomTemplate(exp);
            return exp;
        }
    }
    public static class specialDays
    {
        public static bool aprilfool = DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 1;
        public static bool crimbus = DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day >= 25;
        public static bool spooky = DateTime.UtcNow.Month == 10 && DateTime.UtcNow.Day >= 1;
        public static bool seecretFriday = DateTime.UtcNow.DayOfWeek == DayOfWeek.Friday && DateTime.UtcNow.Day == 9;
        public static bool bktDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday &&
            DateTime.UtcNow.Day == 27 &&
            DateTime.UtcNow.Month == 9;
        public static bool marsDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday &&
            DateTime.UtcNow.Day == 5 &&
            DateTime.UtcNow.Month == 8;
        public static void update()
        {
            aprilfool = DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 1;
            crimbus = DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day >= 25;
            spooky = DateTime.UtcNow.Month == 10 && DateTime.UtcNow.Day >= 1;
            seecretFriday = DateTime.UtcNow.DayOfWeek == DayOfWeek.Friday && DateTime.UtcNow.Day == 9;
            bktDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday &&
                DateTime.UtcNow.Day == 27 &&
                DateTime.UtcNow.Month == 9;
            marsDay = DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday &&
            DateTime.UtcNow.Day == 5 &&
            DateTime.UtcNow.Month == 8;
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