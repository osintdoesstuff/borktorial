using AdventureEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borktorial.adventures
{
    public class wdsim : IAdventure
    {
        public string Title => "Water drinking simulato";
        public void Start(GameState state)
        {
            state.Reset();
            state.Inventory.Add(new InventoryItem(ItemType.Can, "A can of Dr. Breen's Private Reserve", "Tasty!"));
        }
        public void Render(GameState state)
        {
            Console.Clear();
            if (state.Page == 0)
            {
                Console.WriteLine("You are in a water drinking center");
                Console.WriteLine("Commands available: drink");
                state.Inventory.Print();
                Console.WriteLine($"Loyalty Points: {state.LoyaltyPoints}");
            }
        }
        public void HandleInput(GameState state, string input)
        {
            if (state.Page == 0)
            {
                string[] inputSplit = input.Split(' ');
                switch (inputSplit[0])
                {
                    case "drink":
                        if(inputSplit.Length >= 2)
                        {
                            switch (inputSplit[1])
                            {
                                case "can":
                                    state.Inventory.Remove(ItemType.Can);
                                    state.Inventory.Add(new InventoryItem(ItemType.Can, "A can of Dr. Breen's Private Reserve", "Tasty!"));
                                    state.LoyaltyPoints++;
                                    break;
                                default:
                                    Console.WriteLine("Item unavailable");
                                    break;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Command unavailable");
                        break;
                }
            }
        }
    }
}
