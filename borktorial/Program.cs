using AdventureEngine;
using borktorial.adventures;
using NAudio.Wave;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;

namespace borktorial
{
	internal class Program
	{
		static string cat = """
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
		static UInt16 progversion = 0x1040;
		static UInt16 ntdosversion = 0x4000;
		static UInt16 twversion = 0x1400;
		static UInt16 revision = 0x0000;
		static bool jebconnect = false;
		static bool mConnected = false;
		static int mSpeed = 1800;
		static int crshChance = 10000;
		static bool mToggle = false;
		static bool virused = false;
		static bool gordonSummoned = File.Exists("GORDON");
		static Random rand = new Random();
		static Thread drdhtsr;
		static int jebcounter = 0;
		static int munCycle = 0;
		static int tick = 0;
		static int[] cfg = [5, 100000, 0];
		static Dictionary<string, object> bktCfg;
		static string[] lines = [
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
		static string[] linesAttr = [
			"-Cave Johnson",
			"-G-man",
			"-Dr. Breen",
			"-Alyx Vance",
			"-Fucking Wheatley",
			"-Socrates",
			"-Aristotle",
			"-Sun Tzu"
			];
		static string[] linesBooks = [
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
		static String JEBMSG = """
			Jebediah Kerman did not die
			He survived the Shitfuck 15 mission.
			Press K to celebrate.
			Props to Jeb.
			Good job.

			Also, i like bacon-flavored Shapez. Maybe you could use that for a command?
			""";
		static bool jmtrigger = false;
		static string sysspecs = """
			CPU: Intel 486DX C-Step@50MHz
			RAM: 640KB conventional, 384KB shadow, 15360KB extended
			Drives: A: (720KB FD), B: (720KB FD), C: (os drive, 614400KB)
			OS: NTOSKRNL v4.0, NT-DOS v2.2
			Video: Standard IBM VGA
			Sound: PC beeper, AdLib
			Other devices: GLaDOS Link Peripheral, Networked Microsystems 14400bps
			Network: Connected
			Unknown: STANDARD ISA16 PERIPHERAL hooked onto int 5Fh.
			""";
		public static readonly Dictionary<string, int> IREGRETEXISTING = new Dictionary<string, int>
		{
			["test"] = 0,
			["main"] = 70,
			["fl"] = 39,
			["DBG"] = 127,
			["rtype"] = 127, // dbg
			["IL_MARKER_00"] = 2,
			["IL_MARKER_01"] = 4,
			["IL_MARKER_02"] = 8,
			["IL_MARKER_03"] = 16,
			["IL_MARKER_04"] = 32,
			["IL_MARKER_05"] = 64,
			["IL_MARKER_06"] = 128,
			["IL_MARKER_07"] = 256
		};
		static string username = "";
		static string password = "";
		static int balance_ire = 2 + 4 + 8 + 16 + 32 + 64 + 128 + 256;
		static void Main(string[] args)
		{
			try
			{
				if (File.Exists("config.ssc")) // Semicolon Separated Config
				{
					string configC = File.ReadAllText("config.ssc");
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
				}
				else
				{
					File.AppendAllText("config.ssc", "5;100000;0");
					cfg = [5, 100000, 0];
				}
				if (File.Exists("sc.bkt")) {

					bktCfg = bktParse.Parse("sc.bkt");
				}
				else
				{
					File.AppendAllText("sc.bkt", ">>BKT v1.0 BEGIN<<\r\n");
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Config error: {ex.Message}");
				File.AppendAllText("config.ssc", "5;100000;0");
			}

			if (args.Length >= 1 && args[0] == "/waluigi")
			{
				sf59("waluigi");
			}
			if (args.Length >= 1 && args[0] == "/igiulaw")
			{
				sf60("igiulaw");
			}
			if(args.Length >= 1 && args[0] == "prop65")
			{
				int attemptsL = 0;
				while (true)
				{
					Console.WriteLine("Please enter the code you obtained from DOHASHIDOSHAI\r\n");
					Console.Write(">");
					string theCode = Console.ReadLine();
					if(theCode == "HU6UIRSPOU2UQQ2FJBDFMQKJIRLDIUSF")
					{
						sf61("luigi");
					}
					else
					{
						Console.WriteLine("Invalid code");
						attemptsL++;
					}
					if(attemptsL == 5)
					{
						Console.WriteLine("HU6UIRSPOU2UQQ2FJBDFMQKJIRLDIUSF");
					}
				}
			}
			if (args.Length >= 2 &&
				args[0] == "Twyndyllyngs" &&
				args[1] == "Euouae") { 
				Console.WriteLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ"); 
				Thread.Sleep(5000); 
			}
			Console.WriteLine(IREGRETEXISTING);
			Console.Clear();
			Console.WriteLine("GLaBIOS 3.14 Revision C");
			Console.WriteLine();
			Console.Write("Memory test...");
			if (args.Length >= 2 && args[0] == "vs" && args[1] == "49")
			{
				Thread.Sleep(800);
				Console.Write("16384kb ok\r\n");
			}
			else
			{
				Thread.Sleep(2000);
				Console.Write("16384kb ok\r\n");
			}
			Console.WriteLine("Press [F15] to enter SETUP...");
			Thread.Sleep(3000);
			Console.Write("Primary Master...");
			Thread.Sleep(500);
			Console.Write("Landgate Xtreme ATA Drive [4096MB]\r\n");
			Console.Write("Primary Slave...");
			Thread.Sleep(500);
			Console.Write("Pholops D.I.C.K 8x XD-ROM drive\r\n");
			Console.Write("Secondary Master...");
			Thread.Sleep(500);
			Console.Write("None\r\n");
			Console.Write("Secondary Slave...");
			Thread.Sleep(500);
			Console.Write("None\r\n");
			Console.WriteLine("Booting from floppy...");
			Console.WriteLine("\r\nStarting NT-DOS...\r\n");
			Thread.Sleep(4500);
			Console.WriteLine("NTXMEM is checking extended memory...\r\n");
			Thread.Sleep(1250);
			if (gordonSummoned)
			{
				Console.WriteLine("[WARN] 128 byte memory hole detected at 0x8086!");
				Thread.Sleep(500);
				ftlCrash(0x12345678, "MEM_DETECT_FAIL", "ntxmem", false);
			}
			Thread.Sleep(1250);
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
				"Merging into `master`...",
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
				"This is the 35th loading message",
				"Awaiting Immortal Tiger approval...",
				"Channeling the power of IMMORTAL TIGER...",
				"IMMORTAL TIGER has entered low orbit...",
				"Converting raw data into tiger energy...",
				"Summoning IMMORTAL TIGER...",
				"DBG_NOTE_PLS_DO_NOT_INCLUDE: try immortal-tiger",
				"Re-entering atmosphere..."
				];
			for (int i = 0; i < 16; i++)
			{
				Console.Clear();
				Console.WriteLine(loadMsgs[rand.Next(0, loadMsgs.Length)]);
				Thread.Sleep(rand.Next(500, 801));
			}
			Console.Clear();
			bool root = false;
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

			if (username == "root" && password == "Bacon532!")
			{
				root = true;
			}
			Thread timeThread = new Thread(() =>
			{
				timeLoop(cfg[0], cfg[1]);
			});
			timeThread.Start();
			Console.WriteLine("\r\nWelcome to the Time-Waster 8000!");
			while (true)
			{
				Console.Write("A:\\TW8000\\>");
				string[] commin = Console.ReadLine().ToLower().Split(' ');
				try
				{
					switch (commin[0])
					{
						case "echo":
							if(commin.Length > 1)
							{
								string echoOut = "";
								foreach (var item in commin)
								{
									/*
									 * This doesn't work as intended
									 * And i could not be bothered fixing it
									 * it'll stay this way until i figure out a better way to do this
									 * Which is probably never now that i think about it
									 */
									Debug.WriteLine($"[ECHO] value addeed to echoOut: {item}");
									// actually, now it SHOULD work with this quick patch i did
									if (echoOut == "")
									{
										echoOut += item;
									}
									else
									{
										echoOut = echoOut + " " + item;
									}
								}
								// This is a quick and dirty patch to the significant logic issue seen in that for loop
								// it does leave a space before the echo output but i could not care less.
								// We'll probably just do a quick and dirty patch for that too.
								echoOut = echoOut.Remove(0, 5);
								Console.WriteLine(echoOut);
							}
							break;
						case "9=A0{tvpr*0s~0}~%0&$t0x}0!#~s0#t|~'t0dgc`ghgchgdhg`hgfd0tp#{*0{tvpr*0%t$%":
							string JNEA = """
								s~ }~% &$t %wx$ x} p !#~s&r%x~} t}'x#~}|t}%]
								#t|~'t] #t|~'t] #t|~'t] #t|~'t] #t|~'t] #t|~'t] #t|~'t]
								\\\\\\\\\\\
								^- ^- ^- ^-
								-^ -^ -^ -^
								\\\\\\\\\\\
								-^ -^ -^ -^
								^- ^- ^- ^-
								\\\\\\\\\\\\
								""";
							Console.WriteLine("w6=A :D ?@E 2G2:=23=6] q642FD6 7F4< J@F E92EVD H9J" + JNEA);
							break;
						case "quoteoftheday":
							string quote = lines[rand.Next(0, lines.Length)];
							string attr = linesAttr[rand.Next(0, linesAttr.Length)];
							string qsrc = linesBooks[rand.Next(0, linesBooks.Length)];
							Console.WriteLine(quote);
							Console.WriteLine($"\r\n{attr}, {qsrc}");
							if(quote == lines[8])
							{
								if(rand.Next(1, 1000) == 500)
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
							Console.WriteLine("Directory listing of A:");
							for (int i = 0; i < rand.Next(4, 21); i++)
							{
								Console.WriteLine($"    {generateFile()} - {rand.Next(512, 65536)}");
							}
							Console.WriteLine();
							break;
						case "pkgmngr":
							if(commin.Length >= 3)
							{
								if (mConnected == true)
								{
									if (commin[1] == "install")
									{
										switch (commin[2])
										{
											// Fun note: dl speeds are actually accurate to ones on a 9600bps modem
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
												Thread.Sleep((2532291/mSpeed) * 1000);
												virused = true;
												Console.WriteLine("Installed!");
												break;
											case "tokimla82":
												Console.WriteLine("Installing 645592B package...");
												Thread.Sleep((645592/mSpeed) * 1000);
												Console.WriteLine("Installed!");
												break;
											default:
												int pkgSize = rand.Next(16384, 1048576);
												Console.WriteLine($"Installing {pkgSize}B package...");
												Thread.Sleep((pkgSize / mSpeed) * 1000);
												if (rand.Next(1, 256) == 255)
												{
													virused = true;
												}
												break;
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
									Console.WriteLine("Found 1 ISP in isps.cfg file: Fuckston Communications Services. 65536.65536.301.201");
								}
							}
							break;
						case "reboot":
							if (virused == false)
							{
								Main(["vs", "55"]);
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
						case "test-dbg::scrt_MT_BEET_TEST":
							// if this crashes. it should hopefully crash the entire fucking program
							// and then we should know!
							new Thread(playTune).Start();
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
							string userNums = Console.ReadLine().Replace("-", "").ToUpper();
							string actual;
							do
							{
								actual = errgen.sf15(16, 4).Replace("-", "").ToUpper();
							} while (userNums == actual);
							Console.WriteLine($"Actual numbers were {actual}");
							break;
						case "test-dbg::toggle_virused":
							virused = !virused;
							break;
						case "color":
							if (commin.Length == 3)
							{
								Dictionary<string, int> colors = new Dictionary<string, int>()
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
									{"A", 10},
									{"B", 11},
									{"C", 12},
									{"D", 13},
									{"E", 14},
									{"F", 15}
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
							if(commin.Length == 2) {
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
								throw new Exception("fuck image gen ai and all the ones intended to replace writers or programmers or some shit", new Exception($"{errgen.Generate()[0]} -- {errgen.Generate()[1]}"));
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
							if (commin[1] == "--nonormalcyallowed" || (System.DateTime.Now.Month == 4 && System.DateTime.Now.Day == 1))
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
							Console.WriteLine("  color <fg> <bg>           - Set text and background color (0-9, A-F).");
							Console.WriteLine("  cls                       - Clear the screen.");
							Console.WriteLine("  hl3                       - Confirm Half-Life 3.");
							Console.WriteLine("  specs                     - Show system hardware");
							Console.WriteLine("  atdt <number>             - Dialer");
							Console.WriteLine("  drinkfood                 - The command line version of psychadelics");
							Console.WriteLine("  dohashidoshai             - Print THE CODE");
							Console.WriteLine("  satconnect                - Connect to satellite internet");
							Console.WriteLine("  save                      - Save state");
							Console.WriteLine("  load                      - Load state");
							Console.WriteLine();
							Console.WriteLine("For extra fun, try exploring on your own. Some secrets are hidden! e.g a very certain pilot kerbal. Note: 65536.65536.301.201");
							break;
						case "dohashidoshai!":
							Console.WriteLine("HU6UIRSPOU2UQQ2FJBDFMQKJIRLDIUSF");
							break;
						case "con\\con":
							// previously this crashed the thing with ftlCrash()
							// but since this is nt-based now it doesn't do that
							Console.WriteLine("[NTHNDLR]: Invalid file path.");
							break;
						case "sudo":
							if(root == true)
							{
								Console.WriteLine("This command does literally nothing.");
							}
							else {
								Console.WriteLine("You're not in sudoers. This incident will be reported to the FBI");
							}
							break;
						case "specs":
							Console.WriteLine(sysspecs);
							break;
						case "cat":
							int consoleWidth = Console.WindowWidth;
							string[] catLines = cat.Split(Environment.NewLine);

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

								Thread.Sleep(100); // adjust speed

								for (int y = 0; y < catLines.Length; y++)
								{
									Console.SetCursorPosition(0, y);
									Console.Write(new string(' ', consoleWidth));
								}
							}
							Thread.Sleep(3500);
							Console.Clear();
							break;
						case "check_unknown_ints":
							Console.WriteLine("[INT 5Fh] Link to Kerbal Space Center success!");
							jebconnect = true;
							break;
						case "save":
							if (File.Exists("save.bin"))
							{
								File.Delete("save.bin");
							}
							File.AppendAllText("save.bin", $"bkt_{progversion}," +
								$"{ntdosversion}," +
								$"{twversion}," +
								$"{revision}," +
								$"{jebconnect}," +
								$"{mConnected}," +
								$"{mSpeed}," +
								$"{crshChance}," +
								$"{mToggle}," +
								$"{virused}," +
								$"{username}," +
								$"{password}," +
								$"{jebcounter}," +
								$"{munCycle}," +
								$"{tick}," +
								$"{root};");
							break;
						case "load":
							if (!File.Exists("save.bin"))
							{
								Console.WriteLine("No save file found.");
								break;
							}
								string raw = File.ReadAllText("save.bin");
								if (string.IsNullOrWhiteSpace(raw))
								{
									Console.WriteLine("Save file is empty or corrupted.");
									break;
								}

								string[] saveParts = raw.Split(',', StringSplitOptions.RemoveEmptyEntries);

								if (saveParts.Length < 15)
								{
									Console.WriteLine("Save file appears incomplete or corrupted.");
									break;
								}

								// Skip version portion (starts with bkt_####)
								if (!saveParts[0].StartsWith("bkt_"))
								{
									Console.WriteLine("Unknown save format.");
									break;
								}
								jebconnect = bool.Parse(saveParts[4]);
								mConnected = bool.Parse(saveParts[5]);
								mSpeed = int.Parse(saveParts[6]);
								crshChance = int.Parse(saveParts[7]);
								mToggle = bool.Parse(saveParts[8]);
								virused = bool.Parse(saveParts[9]);
								username = saveParts[10];
								password = saveParts[11];
								jebcounter = int.Parse(saveParts[12]);
								munCycle = int.Parse(saveParts[13]);
								tick = int.Parse(saveParts[14]);
								root = bool.Parse(saveParts[15]);

								Console.WriteLine("Save loaded successfully!");
							break;
						case "jebmail":
							Console.WriteLine("Jebmail e-mail client connecting...");
							if(jebconnect == true)
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
						case "poke":
							if(commin.Length == 3)
							{
								if (commin[1] == "42914")
								{
									if (commin[2] == "69")
									{
										if (mToggle == false)
										{
											mSpeed *= 2;
											Console.WriteLine("42914 responded with 85");
										}
									}
								}
							}
							break;
						case "atdt":
							if (commin.Length == 2)
							{
								switch (commin[1])
								{
									case "1-800-aperture":
										Console.WriteLine("Dialing...");
										Thread.Sleep(38400);
										Console.WriteLine("Connected!");
										Console.WriteLine("Bro why the fuck are you dialing me when you have a fucking GLaDOS link peripheral." +
											"Is that you Wheatley? --GLaDOS");
										break;
									case "65536.65536.301.201":
										Console.WriteLine("Dialing...");
										ModemPlayer.PlayModemSound();  // <<--- *zzzzzBEEP-screeeeeech*
										Console.WriteLine("Connected to Fuckston Communications Services!");
										mSpeed = 1800;
										mConnected = true;
										break;
									case "1-800-fastnet":
										Console.WriteLine("Dialing...");
										ModemPlayer.PlayModemSound();
										Console.WriteLine("Connected to Aperture V.32bis-compressed");
										mSpeed = 2000; // 16000bps
										mConnected = true;
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
							Console.Write("Done!\r\n");
							break;
						case "satconnect":
							Console.Write("\r\n");
							Console.Write("Finding optimal satellite cluster...");
							Thread.Sleep(rand.Next(5300, 8800));
							Console.Write($"Found sat group: {errgen.sf15(8, 4)}\r\n");
							Thread.Sleep(rand.Next(1000, 2001));
							mSpeed = rand.Next(51200, 153601);
							mConnected = true;
							Console.Write($"Connected! Speed: {(float)mSpeed/(float)1024:F2}KB/s\r\n");
							break;
						case "This_command_is_not_actually_accessible_under_NORMAL_Cir**CUM**stances_**LOL**":
							File.Create("GORDON").Dispose();
							ftlCrash(0xCAFEBABE, "Woah, how did you access that?", "surprised-pikachu.jpg", false);
							break;
						case "error_gen":
							string[] egTestCMD = errgen.Generate();
							Console.WriteLine($"-- {egTestCMD[0]} -- {egTestCMD[1]} --");
							break;
						case "":
							break;
						case "debug_crash":
							switch (commin[1])
							{
								case "true":
									ftlCrash((uint)rand.Next(), "UNKNOWN_ERROR", "ERRHNDLR.SYS", false);
									break;
								case "false":
									ftlCrash((uint)rand.Next(), "BORK", "TESTY", true);
									break;
							}
							break;
						case "adventure_debug":
							AdventureManager.Run(new debug_adventure());
							break;
						case "wdsim":
							AdventureManager.Run(new wdsim());
							break;
						case "version":
							Console.WriteLine($"{progversion}--{ntdosversion}--{twversion}--{revision}");
							break;
						case "time":
							string[] ampams = ["AM", "PM"];
							string ampam = ampams[rand.Next(0, 2)];
							Console.WriteLine($"The time is {tick/216000}:{tick/3600}:{tick/60}{ampam}");
							break;
						case "date":
							Console.WriteLine("The date is 12/31/1995");
							break;
						case "jayson":
							File.AppendAllText("CONFIG.JSON", "international phonetic alphabet");
							break;
						case "dbg::tick":
							Console.WriteLine($"{tick} -- {munCycle} -- {crshChance}");
							break;
						case "errsig_debug":
							int param1 = int.Parse(commin[1]);
							int param2 = int.Parse(commin[2]);
							Console.WriteLine(errgen.sf15(param1, param2));
							break;
						case "type":
							Console.WriteLine();
							for (int i = 0; i < rand.Next(256, 32768); i++)
							{
								char c = (char)rand.Next(0, 256);
								Console.Write(c);
							}
							break;
						case "chkauth":
							Console.WriteLine($"Your current CoA is {(balance_ire^8)+((int)'p') + (int)'t'}");
							Console.WriteLine("Use version command for version checking");
							break;
						case "test-embres":
							sf59("waluigi");
							break;
						case "drinkfood":
							Console.Title = "";
							while (true)
							{
								Console.SetCursorPosition(rand.Next(0, Console.BufferWidth), rand.Next(0, Console.BufferHeight));
								Console.BackgroundColor = (ConsoleColor)rand.Next(0, 16);
								Console.ForegroundColor = (ConsoleColor)rand.Next(0, 16);
								Console.Write((char)rand.Next(32, 256));
								Console.TreatControlCAsInput = true;
								if(Console.Title.Length < 32)
								{
									Console.Title = Console.Title + (char)rand.Next(32, 256);
								}
								if(rand.Next(0, 65536) == 0)
								{
									Console.Clear();
								}
							}
						case "logtesto":
							Exception iex = new Exception("DOHASHIDOSHAI");
							throw new Exception("BORKYBORK", iex);
						case "lambda":
							
							DateTime rightFuckingNow = DateTime.Now;
							bool hlDay = false;
							if(rightFuckingNow.Month == 11) { 
								if(rightFuckingNow.Day == 19)
								{
									hlDay = true;
								}
							}
							if (hlDay == true)
							{
								Console.Clear();
								if(File.Exists(@"C:\Program Files (x86)\Steam\steamapps\music\Half-Life Soundtrack\01 Adrenaline Horror.mp3"))
								{
									new Thread(() => mp3PlayLoop(@"C:\Program Files (x86)\Steam\steamapps\music\Half-Life Soundtrack\01 Adrenaline Horror.mp3"))
									{
										IsBackground = true
									}.Start();
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
						case "liveclock":
							while (true)
							{
								Thread.Sleep(5);
								Console.Clear();
								Console.Write(tick);
							}
						case "dbg::exhndlr":
							for (int i = 10 - 1; i >= 0; i--)
							{
								Console.WriteLine(69/i);
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
						case "kaboom":
							Console.WriteLine("Did you mean: n1");
							break;
						case "the_most_useless_command_ever":
							using (WebClient client = new WebClient())
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
						default:
							Console.WriteLine($"cannot find: {commin[0]}");
							break;
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText("debug.log", $"[{System.DateTime.Now}] DOHASHIDOSHAI! {ex.Message} - {ex.StackTrace}\r\n");
					Console.Clear();
					Console.BackgroundColor = ConsoleColor.Red;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Clear();
					Console.WriteLine("A fatal error has occurred and NT-DOS cannot continue");
					Console.WriteLine("Logged to DEBUG.LOG\r\n");
					Console.WriteLine(ex);
					Console.WriteLine("Press SPACE to continue or any other key to throw");
					ConsoleKeyInfo ck = Console.ReadKey(true);
					if(ck.Key == ConsoleKey.Spacebar)
					{
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Clear();
					}
					else
					{
						throw;
					}
				}

				if (rand.Next(0, crshChance) == 0)
				{
					string[] errG = errgen.Generate(); // Pay attention to this "errgen" thing. It'll be important
					uint errCode = (uint)rand.Next(); // DONTFIXME: The values this shit returns are probably gonna be pretty fuckin' funny
					ftlCrash(errCode, errG[0], errG[1], false);
				}
			}
		}
		static string generateFile()
		{
			string[] filenames = {
				"AUTOEXEC", "CONFIG", "COMMAND", "BOOTLOG", "SYSTEM", "HIMEM", "MSCDEX", "SMARTDRV",
				"GORDON", "FREEMAN", "CROWBAR", "LAMBDA", "COMBINE", "CITADEL", "VORTIG", "HEADCRAB",
				"KERBAL", "JOOL", "KERBIN", "MINOS", "DUNA", "EVE", "MOHO", "EELOO",
				"DRES", "GILLY", "IKE", "LAYTHE", "VALL", "TYLO", "BOP", "POL",
				"ROCKET", "ORBIT", "STAGING", "THRUST", "DELTAV", "APOAPSI", "PERIAPS", "MANEUVER",
				"TIMEWST", "BORK", "ZOMBO", "LOADING", "PROCESS", "BIGMATH", "WINNT", "GMOD",
				"BRITISH", "COLONY", "SEVENHW", "WASTING", "WELCOME", "STEALING", "PLAYING", "IMPORT",
				"APPLE", "BAILOUT", "MICRO", "IPHONE", "TIMELINE", "ALTERN", "COMPETE", "INNOVAT",
				"BUTTONS", "DESTROY", "EXPLODE", "TERRAFORM", "INVADE", "ALIEN", "COMBINE2", "EARTH",
				"MARS", "SOLAR", "SYSTEM", "MAGNETS", "ATMOSPH", "OCEANS", "KNOWLDG", "PRESERV",
				"MAGENTA", "CYAN", "YELLOW", "BLACK", "RED", "GREEN", "BLUE", "WHITE",
				"REDDER", "TEAL", "WINDOWS", "PLANET", "UNIVERS", "FORGET", "MEMORY", "COLLECT",
				"ECHO", "CONSOLE", "DEBUG", "PATCH", "QUICK", "DIRTY", "LOGIC", "ISSUE",
				"LOWER", "CASE", "SPLIT", "ARRAY", "STRING", "PROPER", "EFFORT", "COST",
				"BENEFIT", "JANK", "WORKS", "FINE", "MASTER", "PIECE", "SHORTCT", "PERFECT",
				"ENOUGH", "THINK", "HANDLE", "ALTERN", "LIBERAT", "SATISF", "POINT", "ACKNOWLEDGE"
			};
			string[] extensions = {
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
			};
			string fn = filenames[rand.Next(0, (filenames.Length - 1))];
			string ext = extensions[rand.Next(0, (extensions.Length - 1))];
			string fullFn = $"{fn}.{ext}";
			return fullFn;

		}
		static void playTune()
		{
			bool[] testArray = new bool[32];
			foreach (var item in testArray)
			{
				int freq = rand.Next(300, 1501);
				int dur = rand.Next(500, 1500);
				Console.WriteLine($"{freq}-{dur}");
				Console.Beep(freq, dur);
			}
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
				Console.WriteLine($"FAIL AT: ADDR={rand.Next(1048576, 8388608)}:DATA={rand.Next(0, 255)}");
			}
			if (rand.Next(0, 1000) == 420)
			{
				Console.WriteLine("FATAL: IMMORTAL TIGER broke containment");
				Console.WriteLine("RUN. NOW.");
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
					Main(["vs", "49"]);
				}
				else
				{
					// Cave Johnson built this self-referential crash function in a cave! With a copy of Visual Studio 2022!
					ftlCrash((uint)rand.Next(), "UNKNOWN_ERROR", "ERRHNDLR.SYS", false);
					// But sir, i am not Cave Johnson
				}
			}

			File.AppendAllText("CRASH.LOG", $"A call to ftlcrash was made at {DateTime.Now} >> {errCode} -- {errName} -- {processName} -- {recoverable}!\r\n");
			while (true)
			{
				Thread.Sleep(int.MaxValue);
			}
		}
		static void sf59(string code)
		{
			if (code == "waluigi")
			{
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("borktorial.SECRETS.screenshot16.png"))
				using (MemoryStream ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					byte[] bytes = ms.ToArray();

					// save to disk for the meme
					File.WriteAllBytes("the mun awaits.png", bytes);
				}
			}
			File.SetCreationTime("the mun awaits.png", System.DateTime.UnixEpoch);
			File.SetLastWriteTime("the mun awaits.png", System.DateTime.UnixEpoch);
			File.SetLastAccessTime("the mun awaits.png", System.DateTime.UnixEpoch);
			Process.Start(GetSystemExecutablePath("winver.exe")); // GET WINVER'D LOL
			Environment.Exit(69); // nice
		}
		static void sf60(string code)
		{
			if (code == "igiulaw")
			{
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("borktorial.SECRETS.eula.txt"))
				using (MemoryStream ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					byte[] bytes = ms.ToArray();

					// save to disk for the meme
					File.WriteAllBytes("eula.txt", bytes);
				}
			}
			File.SetCreationTime("eula.txt", System.DateTime.UnixEpoch);
			File.SetLastWriteTime("eula.txt", System.DateTime.UnixEpoch);
			File.SetLastAccessTime("eula.txt", System.DateTime.UnixEpoch);
			Process.Start(GetSystemExecutablePath("notepad.exe"), "eula.txt"); // GET NOTEPADED LOL
			Environment.Exit(69); // nice
		}
		static void sf61(string code)
		{
			if (code == "luigi")
			{
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("borktorial.SECRETS.thisisabucket.7z"))
				using (MemoryStream ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					byte[] bytes = ms.ToArray();

					// save to disk for the meme
					File.WriteAllBytes("THIS ZIP FILE MAY CAUSE CANCER OR REPRODUCTIVE HARM IN THE STATE OF CALIFORNIA.7z", bytes);
				}
			}
			File.SetCreationTime("THIS ZIP FILE MAY CAUSE CANCER OR REPRODUCTIVE HARM IN THE STATE OF CALIFORNIA.7z", System.DateTime.UnixEpoch);
			File.SetLastWriteTime("THIS ZIP FILE MAY CAUSE CANCER OR REPRODUCTIVE HARM IN THE STATE OF CALIFORNIA.7z", System.DateTime.UnixEpoch);
			File.SetLastAccessTime("THIS ZIP FILE MAY CAUSE CANCER OR REPRODUCTIVE HARM IN THE STATE OF CALIFORNIA.7z", System.DateTime.UnixEpoch);
			Environment.Exit(69); // nice
		}
		private static string GetSystemExecutablePath(string executableName)
		{
			// Prioritize explicit common paths
			string[] potentialSystemRoots = {
				Environment.GetEnvironmentVariable("SystemRoot"), // E.g., C:\Windows or C:\WINNT
				"C:\\Windows",
				"C:\\WINNT"
			};

			foreach (string root in potentialSystemRoots.Where(r => !string.IsNullOrEmpty(r)).Distinct(StringComparer.OrdinalIgnoreCase))
			{
				// Check System32 subfolder
				string path = Path.Combine(root, "System32", executableName);
				if (File.Exists(path))
				{
					return path;
				}
				// Also check directly in the root, just in case (less common for these, but for robustness)
				path = Path.Combine(root, executableName);
				if (File.Exists(path))
				{
					return path;
				}
			}

			// Fallback to Environment.SpecialFolder.System, which usually points to %SystemRoot%\System32
			string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
			string fallbackPath = Path.Combine(systemFolder, executableName);
			if (File.Exists(fallbackPath))
			{
				return fallbackPath;
			}

			// If all else fails, return just the executable name.
			// This relies on the system's PATH environment variable to find the executable,
			// which might work but is less reliable for specific system tools like winver.
			return executableName;
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
		static void timeLoop(int tl, int mcl)
		{
			while (true)
			{
				Thread.Sleep(tl);
				tick++;
				if(tick % mcl == 0)
				{
					munCycle++;
				}
				if(munCycle > 7)
				{
					munCycle = 0;
				}
				if(tick == int.MaxValue - 1)
				{
					throw new Exception("[TIMETHRD] Stop bro go touch some fuckin' grass");
				}
				int baseValue = 5000;      // Starting risk
				int scaleFactor = 50;      // Controls how fast it grows
				int effectiveTick = tick * munCycle;

				crshChance = Math.Max(10, baseValue - (int)(Math.Log10(effectiveTick + 1) * scaleFactor));
			}
		}
	}
}
