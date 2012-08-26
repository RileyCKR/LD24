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
        public static bool NeedsEvolve { get; private set; }

        public static bool FlagMoreChildren { get; private set; }
        public static bool FlagQuickness { get; private set; }
        public static bool FlagResilient { get; private set; }

        public static int ChildEnergyCost = 2;
        public static float VirusSpeed = 0.15F;
        public static int DamageCost = 2;

        public static List<Evolution> Evolutions = new List<Evolution>
        {
            new Evolution(1, "More Children"),
            new Evolution(2, "Quickness"),
            new Evolution(3, "Resilient"),
            //"Immune Compromiser",
            //"Antigen-Proof"
        };

        public static void IncrementDeadCellCount()
        {
            DeadCellCount++;
            ShowEvolutionMenu();
        }

        public static void IncrementDeadVirusCount()
        {
            DeadVirusCount++;
        }

        public static void ShowEvolutionMenu()
        {
            if (Evolutions.Count > 0)
            {
                GameStats.NeedsEvolve = true;
                EvolutionModal.SetMenuItems(Evolutions.ToArray());
            }
            else
            {
                //Fully evolved
            }
        }


        public static void SelectEvolution(int id)
        {
            NeedsEvolve = false;

            switch (id)
            {
                case 1:
                    FlagMoreChildren = true;
                    ChildEnergyCost = 1;
                    break;
                case 2:
                    FlagQuickness = true;
                    VirusSpeed = 0.25F;
                    break;
                case 3:
                    FlagResilient = true;
                    DamageCost = 1;
                    break;
                default:
                    throw new ApplicationException("Unknown Evolution ID: " + id.ToString());
            }

            //Remove item from the collection
            for (int x = 0; x < Evolutions.Count; x++)
            {
                Evolution item = Evolutions[x];
                if (item.ID == id)
                {
                    Evolutions.RemoveAt(x);
                    break;
                }
            }
        }
    }
}