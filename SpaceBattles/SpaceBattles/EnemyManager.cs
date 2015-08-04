using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceBattles
{
    class EnemyManager
    {
        static public int enemyType; // Тип следующего сгенерированого врага.
        static public Vector2 speed; // Позиция и скорость следующего сгенирированного врага
        static int MilSecToNextEnemy;
        static int MilSecFromLastEnemy=0;
        const int maxMilSecToNextEnemy = 2000;
        
          

       
        static public bool isNext(GameTime gt) {

            

            MilSecFromLastEnemy += gt.ElapsedGameTime.Milliseconds;

            if (MilSecToNextEnemy<MilSecFromLastEnemy) return true;

            return false;
        }

        static public void newEnemy(Random rnd, int LoadedEnemyCount,int level) {

            MilSecFromLastEnemy = 0;
            MilSecToNextEnemy = rnd.Next(maxMilSecToNextEnemy - (2*maxMilSecToNextEnemy / (3*LevelManager.maxLvl) * (int)Math.Sqrt(level)));

            
            enemyType = rnd.Next((rnd.Next(0,level)*LoadedEnemyCount/level),LoadedEnemyCount);

            speed = new Vector2(rnd.Next(3) - 1, 1);
           

        }

        
    }
}
