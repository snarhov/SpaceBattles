using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceBattles
{
    class BonusManager
    {

        static public int bonusType; // Тип следующего сгенерированого врага.
        static public Vector2 speed; // Позиция и скорость следующего сгенирированного врага
        static int MilSecToNextBonus=30000  ;
        static int MilSecFromLastBonus = 0;
        const int maxMilSecToNextBonus = 30000;




        static public bool isNext(GameTime gt)
        {



            MilSecFromLastBonus += gt.ElapsedGameTime.Milliseconds;

            if (MilSecToNextBonus < MilSecFromLastBonus) return true;

            return false;
        }

        static public void newBonus(Random rnd, int LoadedBonusCount, int level)
        {

            MilSecFromLastBonus = 0;
            MilSecToNextBonus = rnd.Next(maxMilSecToNextBonus - (2 * maxMilSecToNextBonus / (3 * LevelManager.maxLvl) * (int)Math.Sqrt(level)));


            bonusType = rnd.Next((rnd.Next(0, level) * LoadedBonusCount / level), LoadedBonusCount);

            speed = new Vector2(rnd.Next(3) - 1, 1);


        }


    }
}
