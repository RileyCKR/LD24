using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public static Vector2 RandomUnitVector()
        {
            Vector2 vect = new Vector2();
            int rand = NexVal(0, 7);
            switch (rand)
            {
                case 0:
                    vect = new Vector2(0, 1);
                    break;
                case 1:
                    vect = new Vector2(1, 1);
                    break;
                case 2:
                    vect = new Vector2(1, 0);
                    break;
                case 3:
                    vect = new Vector2(1, -1);
                    break;
                case 4:
                    vect = new Vector2(0, -1);
                    break;
                case 5:
                    vect = new Vector2(-1, -1);
                    break;
                case 6:
                    vect = new Vector2(-1, 0);
                    break;
                case 7:
                    vect = new Vector2(-1, 1);
                    break;
            }

            vect.Normalize();
            return vect;
        }
    }
}
