using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD24
{
    static class RNG
    {
        private static Random random;

        public static void Seed()
        {
            random = new Random();
        }

        public static void Seed(int seed)
        {
            random = new Random(seed);
        }

        public static int NexVal(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
