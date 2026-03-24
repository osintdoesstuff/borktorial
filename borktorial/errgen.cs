using System.Text;
using aperture;

namespace borktorial
{
    internal class errGen
    {
        public static readonly Random rand = new();
        public static readonly string[] templates = [
            "<animal>_BROKE_CONTAINMENT<notem>",
            "CRITICAL_PROCESS_FAILED<notem>",
            "SCP-<scpnum>_BROKE_CONTAINMENT<notem>",
            "INVALID_OPCODE<notem>",
            "SEGMENTATION_FAULT<notem>",
            "<kerbal>_KERMAN_DEAD<notem>",
            "NETWORK_ERROR_<num><notem>",
            "STACK_OVERFLOW<notem>",
            "STACK_UNDERFLOW<notem>",
            "GLADOS_ERROR_<num><notem>",
            "THREAD_ERROR_<num><notem>",
            "GENERAL_PROTECTION_FAULT<notem>",
            "<subject>_ERROR_<num><notem>",
            "<object>_ERROR_<num><notem>",
            "KERNEL_ERROR_<lowerltr><num16><notem>",
            "SYS_UNKNOWN_ERR_<lowerltr><num16><hletter><nibble><notem>",
            "INVALID_VALUE_IN_<register><notem>",
            "MEMORY_ERROR_<addr386><notem>",
            "<animal>_<verb>_<object><notem>",
            "<subject>_<object>_ERROR<notem>",
            "<subject>_<verb>_<object><notem>",
            "<subject>_<verb>_<animal><notem>",
            "<kerbal>_KERMAN_<verb>_<object><notem>",
            "<kerbal>_KERMAN_<verb>_<subject><notem>",
            "<object>_<verb>_<subject><notem>",
            ];
        public static readonly string[] subjects =
        [
            "GLaDOS",
            "JEBEDIAH_KERMAN",
            "VAL_KERMAN",
            "DRDICKHD",
            "THE_GOAT",
            "THE_USER",
            "MISSINGNO",
            "AI_OVERLORD_V4",
            "COMMAND_MODULE",
            "CAPYBARA_CLUSTER",
            "BOOPABLE_UNIT",
            "INPUT_HANDLER",
            "DREAMSTACK_ENTITY",
            "OS_KERNEL",
            "BUTTON_PRESSER",
            "GRAVITY_SIMULATOR",
            "MOUSE_DRIVER",
            "BADGER_AI",
            "JAVA_RUNTIME",
            "MEME_ENGINE",
            "QUANTUM_PROCESSOR",
            "TOASTER_AI",
            "PORTAL_GUN",
            "ERROR_HANDLER",
            "GLITCH_ENTITY",
            "CAPYBARA_OVERLORD",
            "INFINITE_LOOP",
            "NULL_POINTER",
            "RANDOMIZER_CORE",
            "SHRIMP_OVERSEER",
            "VIRTUAL_TIGER",
            "BUTTON_MASHER",
            "COFFEE_MAKER",
            "INTERNET_ARCHIVIST"
        ];
        public static readonly string[] objects =
        [
            "MEMORY_BANK",
            "GRAVITY_CORE",
            "NAVIGATION_STACK",
            "EXPERIMENTAL_DRIVE",
            "NULL_POINTER",
            "BOOTLOADER",
            "ETHERNET_PORT",
            "BREADBOARD",
            "CRITICAL_SUBSYSTEM",
            "FLUX_CAPACITOR",
            "ELEVATOR_SHAFT",
            "SPAGHETTI_HEAP",
            "CONFIG_FILE",
            "BAUD_RATE_LIMITER",
            "VIRTUAL_FILESYSTEM",
            "SHRIMP_BUFFERS",
            "REALITY_INTERFACE",
            "LIFE_SUPPORT_SYSTEM",
            "KERBAL_NETWORK_LINK",
            "FAX_MACHINE_CONTROLLER",
            "MEME_CACHE",
            "TIGER_CAGE",
            "PORTAL_FRAME",
            "QUANTUM_FLUX",
            "INFINITE_STACK",
            "GLITCH_MATRIX",
            "ERROR_LOG",
            "SOFTLOCK_ZONE",
            "COFFEE_RESERVOIR",
            "INTERNET_PIPE",
            "BUTTON_ARRAY",
            "RANDOM_SEED",
            "NULL_ZONE",
            "SHRIMP_POOL",
            "CAPYBARA_DEN"
        ];
        public static readonly string[] kerbals = [
            "JEBEDIAH",
            "BILL",
            "BOB",
            "VALENTINA",
            "WERNHER_VON",
            "GENE",
            "MORTIMER",
            "LINUS",
            "WALT",
            "GUS",
            "ALAN",
            "BOBAK",
            "DAWTON",
            "DINKELSTEIN",
            "EUMON",
            "FELIPE",
            "JULES",
            "KIRRIM",
            "KURT"
            ];
        public static readonly string[] verbs = [
            "ATE",
            "ENDED",
            "CREATED",
            "DRANK",
            "RAMMED",
            "NEUROTOXINED",
            "CORRUPTED",
            "INVERTED",
            "QUANTIZED",
            "DELETED",
            "REBOOTED",
            "EXPLODED",
            "GLITCHED",
            "SOFTLOCKED",
            "OVERCLOCKED",
            "FRIED",
            "REDACTED",
            "MEMEIFIED",
            "RECURSIFIED",
            "DEFRAGMENTED",
            "REANIMATED",
            "UNINSTALLED"
            ];
        public static readonly string[] animals = [
            "TIGER",
            "DOLPHIN",
            "CAPYBARA",
            "LLAMA",
            "FERRET",
            "SNAKE",
            "BADGER",
            "SHRIMP",
            "GOOSE",
            "CATGIRL",
            "SQUIRREL",
            "KOALA",
            "AXOLOTL",
            "RACCOON",
            "BASILISK",
            "PLATYPUS",
            "WOMBAT",
            "PENGUIN",
            "MOOSE",
            "YAK",
            "TARDIGRADE",
            "OPOSSUM",
            "CHUPACABRA",
            "QUOKKA",
            "HYENA",
            "PUFFERFISH",
            "ALPACA",
            "BEAVER",
            "OTTER",
            "MEGALODON"
            ];
        public static readonly string[] processNames = [
            "NTOSKRNL",
            "SVCHOST",
            "CMDSHELL",
            "P32KRNL",
            "DOS32_INIT",
            "WINLOGON",
            "LOGIN32",
            "PKGMNGR",
            "MEMORY_MANAGER",
            "KERNEL_HANDLER",
            "GRAPHICS_POLLER",
            "INPUT_DAEMON",
            "MOUSE_HANDLER",
            "KEYBOARD_HANDLER",
            "TIGER_CORE",
            "WATCHDOG_SUBSYS",
            "DRDICKHD",
            "CRYPTERFACE",
            "TLS_ENGINE",
            "NETWORK",
            "NETPROTSTACK",
            "DNS_FAILED",
            "SOCKET_HANDLER",
            "GPU_RENDER3D",
            "VIRTUAL_13H",
            "PROCESS_SCHEDULER",
            "HYPERVISOR",
            "VORTEX_DRIVER",
            "BLACKBOX_THRASHER",
            "NULLREF_COLLECTOR",
            "GC_SWEEPER",
            "API_WRANGLER",
            "BOOPABLE_UNIT",
            "MIRROR_CACHE",
            "DOLPHIN_VIRUS",
            "FERAL_THREAD",
            "TOASTER_STACK",
            "TIMEKEEPER",
            "REALTIME_CLOCK_MGR",
            "MEMLEAK_REPORTER",
            "SPAGHETTI_HEAP",
            "GRAVITY_WIDGET",
            "KSP_ENGINE",
            "HL3_BOOTER",
            "BORK_CORE",
            "SHITFUCK_ENGINE",
            "WASTEOFTIME8000_SERVICE",
            "ZOMBO_API",
            "POOHBEAR_SOFTLOCK",
            "TERMINAL_BUFFER",
            "JANK_RENDERER",
            "BUTTON_PROVIDER",
            "PHYSX_SIM_ENGINE",
            "COFFEE_SUBSYSTEM",
            "PANIC_CRASHDUMP",
            "FLUX_CAPACITOR",
            "DELIRIUM_DAEMON",
            "TOAST_MONITOR",
            "EXCEPTION_HANDLER",
            "BLUE_SCREEN_WRITER",
            "CAPYBARA_DEDUPLICATOR",
            "KERNEL32_EXPLODER",
            "SLOW_FATAL_LOOP",
            "DREAMSTACK",
            "NULL_POINTER_HANDLER",
            "SYNTAX_PARSER",
            "STACK_OVERFLOW_HANDLER",
            "PIZZA_PROCESSOR",
            "BOBBYTABLE_SANITIZER",
            "JAVA_RUNTIME_EXCEPTION_THROWER",
            "MEME_HANDLER",
            "QUANTUM_ENTANGLER",
            "BORK_BOT",
            "TIGER_OVERLORD",
            "PORTAL_GUN_DRIVER",
            "INFINITE_LOOP_DETECTOR",
            "GLITCH_MATRIX_ENGINE",
            "SOFTLOCK_MONITOR",
            "COFFEE_MAKER_DAEMON",
            "INTERNET_PIPELINE",
            "BUTTON_MASHER_SERVICE",
            "STANLEY",
            "RANDOMIZER_CORE",
            "SHRIMP_OVERSEER",
            "CAPYBARA_DEN_MANAGER",
            "NULL_ZONE_HANDLER",
            "INVERSE_SQUARE_ROOT",
            "QUAKE",
            "BREEN",
            "WOLF3D",
            "FASTFAT",
            "SLCS",
            "SMSS",
            "CSRSS",
            "MSBOB",
            "NTVDM",
            "HL3"
            ];
        public static readonly string[] notem = [
            // filler to make it more unlikely
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "_NTISBTR",
            "_FATAL",
            "_WARN",
            "_UNK",
            "_KRNL", // Kernel-level error
            "_IRQT", // IRQ triggered error
            "_XSW" // External software caused error
            ];
        public static readonly char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();
        public static readonly char[] lowletters = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
        public static readonly string[] registers = ["EAX", "EBX", "ECX", "EDX", "ESP", "SS", "ES", "DS", "CS"];
        public static readonly char[] nibbles = "1234567890".ToCharArray();
        public static readonly char[] hexLetters = "ABCDEF".ToCharArray();
        public static readonly char[] hexDigits = "1234567890ABCDEF".ToCharArray();
        // we codenamed this one the Aperture Science Templated System Potential Failure Detection and Generation System(TM)
        public static string[] generateErr()
        {
            // Choose random template
            string gErrI = templates[rand.Next(templates.Length)];
            return genCustomTemplate(gErrI);
        }
        public static string[] genCustomTemplate(string template)
        {
            string gErrI = template;
            string processName = processNames[rand.Next(processNames.Length)];
            gErrI = gErrI.Replace("<animal>", animals[rand.Next(animals.Length)]);
            gErrI = gErrI.Replace("<kerbal>", kerbals[rand.Next(kerbals.Length)]);
            gErrI = gErrI.Replace("<num>", rand.Next(0, 256).ToString());
            gErrI = gErrI.Replace("<scpnum>", rand.Next(2, 7500).ToString());
            gErrI = gErrI.Replace("<notem>", notem[rand.Next(notem.Length)]); // Marker to designate "NOt TEMplate". At times.
            gErrI = gErrI.Replace("<subject>", subjects[rand.Next(subjects.Length)]);
            gErrI = gErrI.Replace("<object>", objects[rand.Next(objects.Length)]);
            gErrI = gErrI.Replace("<num16>", rand.Next(65536).ToString());
            gErrI = gErrI.Replace("<letter>", letters[rand.Next(letters.Length)].ToString());
            gErrI = gErrI.Replace("<nibble>", nibbles[rand.Next(nibbles.Length)].ToString());
            gErrI = gErrI.Replace("<hletter>", hexLetters[rand.Next(hexLetters.Length)].ToString());
            gErrI = gErrI.Replace("<lowerltr>", lowletters[rand.Next(lowletters.Length)].ToString());
            gErrI = gErrI.Replace("<addr8086>", rand.Next(0, 1048576).ToString());
            gErrI = gErrI.Replace("<reg8086>", rand.Next(0, 65536).ToString());
            gErrI = gErrI.Replace("<addr386>", rand.Next(0, int.MaxValue).ToString());
            gErrI = gErrI.Replace("<reg386>", rand.Next(0, int.MaxValue).ToString());
            gErrI = gErrI.Replace("<register>", registers[rand.Next(registers.Length)]);
            gErrI = gErrI.Replace("<verb>", verbs[rand.Next(verbs.Length)]);
            gErrI = gErrI.Replace("<pname>", processNames[rand.Next(processNames.Length)]);
            gErrI = aprtMain.pNrH(gErrI, rand);
            return [gErrI, processName];
        }
    }
}