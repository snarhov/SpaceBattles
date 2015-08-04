using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceBattles
{
    class LevelManager
    {
        const int maxLevel = 100;
        public static int maxLvl {
            get { return maxLevel; }
        }
        static int level = 1;
        public static int Lvl {
            get { return level; }
        }
        static int expToNextLvl = 100000;
        static int currentExp=0;
        static GameTime prevGt; 
        static bool win = false;
        static double per=0;
        public static int expPer {
            get { return (int)(per * 100); }
        }
        public static bool isWin {
            get { return isWin; }
        }

        public static void Initializate(GameTime gt)
        {
            prevGt = gt;
        }

        public static void Update(GameTime gt) {

            currentExp += gt.ElapsedGameTime.Milliseconds;
            
            if (currentExp > expToNextLvl) {

                currentExp = currentExp - expToNextLvl;
                level++;
                expToNextLvl = (int)(expToNextLvl * 1.05);

                if (level == maxLevel)
                {
                    win = true;
                }

            }

            per = (double) currentExp /  (double) expToNextLvl;

        }

        public static void addExp(int Exp) {
            Exp = Exp * 100;
            currentExp += Exp;
        }


    }
}
