using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
    class Randomizer
    {
        private static Random rand = new Random();

        public static bool FlipCoin()
        {
            if (rand.Next(0, 2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int RollZeroBasedD3()
        {
            return rand.Next(0, 3);
        }

        public static int RollDX(int X)
        {
            return rand.Next(1, X + 1);
        }
    }
}
