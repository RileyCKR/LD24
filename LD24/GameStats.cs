using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD24
{
    static class GameStats
    {
        public static Virus PlayerVirus { get; set; }
        public static int DeadCellCount { get; private set; }
        public static int DeadVirusCount { get; private set; }

        public static void IncrementDeadCellCount()
        {
            DeadCellCount++;
        }

        public static void IncrementDeadVirusCount()
        {
            DeadVirusCount++;
        }
    }
}