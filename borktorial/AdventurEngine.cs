using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureEngine
{
    // ================== INVENTORY SETUP ==================

    public enum ItemType
    {
        None,
        Can,
        Crowbar,
        CheeseWheel,
        WaterFlask,
        OrbOfConfusion,
        Wood,
        Rock,
        VapePen,
        CombineKeycard,
        Sand,
        CombineRation,
        PortablePizza,
        RocketEngine,
        HandGrenade,
        Generic00,
        Generic01,
        Generic02,
        Generic03,
        Generic04,
        Generic05, 
        Generic06,
        Generic07,
        Generic08,
        Generic09,
        Generic10,
        Generic11,
        Generic12,
        Generic13,
        Generic14,
        Generic15,
        Generic16,
        Generic17,
        Generic18,
        Generic19,
        Generic20,
        Generic21,
        Generic22,
        Generic23,
        Generic24
    }

    public class InventoryItem
    {
        public ItemType Type { get; }
        public string Name { get; }
        public string Description { get; }

        public InventoryItem(ItemType type, string name, string description)
        {
            Type = type;
            Name = name;
            Description = description;
        }

        public override string ToString() => $"{Name}: {Description}";
    }

    public class InventoryManager
    {
        private readonly List<InventoryItem> _items = new();

        public void Add(InventoryItem item)
        {
            _items.Add(item);
        }

        public void Remove(ItemType type)
        {
            _items.RemoveAll(i => i.Type == type);
        }

        public void Clear() => _items.Clear();

        public bool Has(ItemType type) => _items.Any(i => i.Type == type);

        public void Print()
        {
            Console.WriteLine("Inventory:");
            if (_items.Count == 0)
            {
                Console.WriteLine("  (empty)");
                return;
            }
            foreach (var item in _items)
            {
                Console.WriteLine($"  • {item}");
            }
        }
    }

    // ================== GAME STATE ==================

    public class GameState
    {
        public int Page { get; set; }
        public bool AdventureOver { get; set; }
        public int LoyaltyPoints { get; set; }
        public Dictionary<string, bool> Flags { get; set; } = new();
        public InventoryManager Inventory { get; } = new();

        public void Reset()
        {
            Page = 0;
            LoyaltyPoints = 0;
            Flags.Clear();
            Inventory.Clear();
            AdventureOver = false;
        }
    }

    // ================== INTERFACE ==================

    public interface IAdventure
    {
        string Title { get; }

        void Start(GameState state);              // Called once at the start (initialize states)
        void Render(GameState state);             // Gets called on each "page draw"
        void HandleInput(GameState state, string input);  // Called after input
    }

    // ================== ADVENTURE MANAGER ==================

    public static class AdventureManager
    {
        public static void Run(IAdventure adventure)
        {
            var gameState = new GameState();
            Console.Clear();
            Console.WriteLine($"Starting Adventure: {adventure.Title}\n");

            adventure.Start(gameState);

            while (!gameState.AdventureOver)
            {
                adventure.Render(gameState);

                Console.Write("\n> ");
                string? input = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(input)) continue;

                adventure.HandleInput(gameState, input);
            }

            Console.WriteLine("\nAdventure Completed.");
            Console.WriteLine($"Loyalty Points: {gameState.LoyaltyPoints}");

            string log = $"[{DateTime.Now}] {adventure.Title} ended with {gameState.LoyaltyPoints} loyalty points.\n";
            File.AppendAllText("ADVENTURE.LOG", log);
        }
    }
}