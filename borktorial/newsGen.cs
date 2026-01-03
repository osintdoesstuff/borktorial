namespace borktorial
{
    internal class newsGen
    {
        static Random rand = new Random();
        static string[] templates = [
            "<company> announced <product> today, expected to ship Q<nr1-4> <nr1995-1999>.",
            "<company> stock fell <nr5-40>% following <scandal>.",
            "<product> version <nr1-9>.<nr0-99> released with <feature> support.",
            "<company> acquires <competitor> for $<nr50-999> million.",
            "<person> departs <company>, cites 'creative differences'.",
            "Local BBS '<bbsname>' reaches <nr1000-50000> registered users.",
            "<product> recalled after reports of <problem>.",
            "<company> denies <product> delay rumors, promises Q<nr1-4> launch.",
            "Benchmark tests show <product> outperforms <competitor_product> by <nr10-45>%.",
            "<person> keynote at <event> reveals <product> details.",
            "<company> faces lawsuit over <scandal>.",
            "Review: <product> delivers <nr5-10>% performance boost over previous generation.",
            "<product> requires <nr8-128>MB RAM, <nr100-500>MB disk space.",
            "<company> and <competitor> announce partnership on <technology> standard.",
            "Early adopters report <problem> with <product>.",
            "Industry analysts predict <technology> will replace <old_tech> by <nr1999-2005>.",
            "<product> hits <nr100000-2000000> units sold in first quarter.",
            "<company> stock surges <nr10-35>% on strong <product> sales.",
            "<person> promises '<product> will revolutionize <industry>'.",
            "Beta testers praise <product>, launch set for <month> <nr1998-1999>.",
            "<product> supports up to <nr2-16> processors in SMP configuration.",
            "OEM deals bring <product> to <competitor> systems.",
            "<company> cuts <nr500-5000> jobs amid restructuring.",
            "Service pack <nr1-4> for <product> fixes <nr20-150> known issues.",
            "<event> attendance hits record <nr5000-50000>, <product> demos dominate floor."
                ];

        static string[] companies = [
            "3DFX",
            "Creative Labs",
            "Microsoft",
            "IBM",
            "Compaq",
            "Dell",
            "NVIDIA",
            "ATI",
            "Intel",
            "AMD",
            "Novell",
            "Apple",
            "Silicon Graphics",
            "Sun Microsystems",
            "Borland",
            "Lotus",
            "Netscape"
                ];

        static string[] products = [
            "Voodoo 3",
            "SoundBlaster AWE64",
            "Windows 98",
            "OS/2 Warp",
            "Pentium III",
            "Athlon",
            "Quake III Arena",
            "Unreal Tournament",
            "NetWare 5",
            "Matrox G400",
            "RIVA TNT2",
            "Half-Life",
            "StarCraft",
            "RedHat Linux 6.0"
                ];

        static string[] competitors = [
            "S3",
            "Cyrix",
            "VIA",
            "Trident",
            "Rendition",
            "Number Nine"
                ];

        static string[] competitor_products = [
            "Savage 4",
            "Rage 128",
            "TNT",
            "Voodoo 2",
            "Pentium II",
            "K6-2"
                ];

        static string[] persons = [
            "John Carmack",
            "Bill Gates",
            "Steve Jobs",
            "Linus Torvalds",
            "Michael Dell",
            "Tim Sweeney",
            "Gabe Newell",
            "Brian Hook"
                ];

        static string[] scandals = [
            "driver stability issues",
            "Y2K compliance failures",
            "monopoly allegations",
            "missing DirectX features",
            "thermal design flaws",
            "patent infringement claims"
                ];

        static string[] problems = [
            "IRQ conflicts",
            "driver crashes",
            "overheating under load",
            "incompatibility with AGP 4X",
            "memory leaks",
            "blue screens on NT 4.0"
                ];

        static string[] features = [
            "hardware T&L",
            "32-bit color",
            "AGP 4X",
            "MMX",
            "3DNow!",
            "OpenGL 1.2",
            "Direct3D 6",
            "EAX audio",
            "USB"
                ];

        static string[] technologies = [
            "AGP",
            "USB",
            "FireWire",
            "DDR RAM",
            "DVD-ROM",
            "UDMA/66"
                ];

        static string[] tech = [
            "PCI",
            "ISA",
            "EDO RAM",
            "CD-ROM",
            "IDE"
                ];

        static string[] events = [
            "COMDEX",
            "E3",
            "CES",
            "WinHEC",
            "LinuxWorld"
                ];

        static string[] industries = [
            "gaming",
            "3D graphics",
            "professional workstations",
            "home computing",
            "multimedia"
                ];

        static string[] months = [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
                ];

        static string[] bbsnames = [
            "Silicon Dreams",
            "Digital Dungeon",
            "The Neon Nights",
            "CyberZone",
            "Hack Shack",
            "The Matrix",
            "Electric Cafe",
            "Data Haven"
                ];

        public static string Generate()
        {
            string gNe = templates[rand.Next(templates.Length)];

            // Replace all tags
            gNe = gNe.Replace("<company>", companies[rand.Next(companies.Length)]);
            gNe = gNe.Replace("<competitor>", competitors[rand.Next(competitors.Length)]);
            gNe = gNe.Replace("<product>", products[rand.Next(products.Length)]);
            gNe = gNe.Replace("<competitor_product>", competitor_products[rand.Next(competitor_products.Length)]);
            gNe = gNe.Replace("<person>", persons[rand.Next(persons.Length)]);
            gNe = gNe.Replace("<scandal>", scandals[rand.Next(scandals.Length)]);
            gNe = gNe.Replace("<problem>", problems[rand.Next(problems.Length)]);
            gNe = gNe.Replace("<feature>", features[rand.Next(features.Length)]);
            gNe = gNe.Replace("<technology>", technologies[rand.Next(technologies.Length)]);
            gNe = gNe.Replace("<old_tech>", tech[rand.Next(tech.Length)]);
            gNe = gNe.Replace("<event>", events[rand.Next(events.Length)]);
            gNe = gNe.Replace("<industry>", industries[rand.Next(industries.Length)]);
            gNe = gNe.Replace("<month>", months[rand.Next(months.Length)]);
            int rc1 = rand.Next(0, 2);
            if(rc1 == 0)
            {
                gNe = gNe.Replace("<bbsname>", bbsnames[rand.Next(bbsnames.Length)]);
            }
            else
            {
                gNe = gNe.Replace("<bbsname>", bbsNameGen());
            }

            // Process number ranges
            gNe = aperture.bktStf.pNrH(gNe, rand);

            return gNe;
        }
        public static string bbsNameGen()
        {
            string[] bbs_adjectives = ["Silicon", "Digital", "Cyber", "Neon", "Electric", "Data"];
            string[] bbs_nouns = ["Dreams", "Dungeon", "Zone", "Haven", "Cafe", "Nexus"];
            string[] bbs_prefixes = ["The ", ""];

            // Generate:
            string bbsName = bbs_prefixes[rand.Next(bbs_prefixes.Length)] +
                             bbs_adjectives[rand.Next(bbs_adjectives.Length)] + " " +
                             bbs_nouns[rand.Next(bbs_nouns.Length)];
            return bbsName;
        }
    }
}
