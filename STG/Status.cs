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

        private static int power;

        public static int Power
        {
            get
            {
                return power;
            }

            private set
            {
                if (value < 0)
                    power = 0;
                else if (value > 100)
                    power = 100;
                else
                    power = value;
            }
        }

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
            Power = 0;
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

        public static void AddPower()
        {
            Power++;
        }

        public static void AddMultiplier()
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

        public static void RemovePower()
        {
            Power -= 20;
        }

        public static void ResetBomb()
        {
            if (Bombs < 3)
                Bombs = 3;
        }
    }
}
