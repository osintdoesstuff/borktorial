using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine;
namespace borktorial.adventures
{
    public class debug_adventure : IAdventure
    {
        public string Title => "The quick brown fox falls asleep and then jumps over the lazy dog";
        public void Start(GameState state)
        {
            state.Reset();
            state.Inventory.Add(new InventoryItem(ItemType.Can, "A can of Dr. Breen's Private Reserve", "Tasty!"));
            state.Inventory.Add(new InventoryItem(ItemType.OrbOfConfusion, "Microwave Popcorn", "Yum!"));
            state.Page = 0;
        }
        public void Render(GameState state)
        {
            Console.Clear();
            if(state.Page == 0)
            {
                Console.WriteLine("You are in a empty white room with 1 door and a turned-on incinerator");
                Console.WriteLine("A) Leave the room");
                Console.WriteLine("B) Jump in the incinerator");
                state.Inventory.Print();
            }
            if(state.Page == 1)
            {
                Console.WriteLine("You're in the outside world. There's grass, sheep, and a Vortigaunt. It's peaceful");
                state.AdventureOver = true;
            }
            if (state.Page == 2)
            {
                Console.WriteLine("You died.");
                state.AdventureOver = true;
            }
        }
        public void HandleInput(GameState state, string input)
        {
            if(state.Page == 0)
            {
                switch (input)
                {
                    case "a":
                        state.Page = 1;
                        break;
                    case "b":
                        state.Page = 2;
                        break;
                    default:
                        Console.WriteLine("WRONG!!!");
                        break;
                }
            }
        }
    }
}
