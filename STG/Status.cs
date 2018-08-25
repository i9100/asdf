using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    static class Status
    {
        public static int Lives { get; private set; }
        public static int Bombs { get; private set; }
        public static int Score { get; private set; }
        public static int Multiplier { get; private set; }

        public static bool IsGameOver { get { return Lives < 0; } }

        static Status()
        {
            Reset();
        }

        public static void Reset()
        {
            Score = 0;
            Lives = 0;
            Bombs = 3;
            Multiplier = 1;
        }

        public static void AddScore(int point)
        {
            Score += point * Multiplier;
        }

        public static void AddLife()
        {
            Lives++;
        }


        public static void AddBomb()
        {
            Bombs++;
        }

        public static void RaiseMultiplier()
        {
            Multiplier++;
        }

        public static void RemoveLife()
        {
            Lives--;
        }

        public static void RemoveBomb()
        {
            Bombs--;
        }
    }
}
