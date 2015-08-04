using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBattles
{
    class AutomatedSprite : Sprite
    {

        int hp;
        int maxHp;
        public bool isFire;
        bool FireAval = true;
        const int defaultMilSecForShoot = 1000;
        int MilSecForShoot, MilSecSinceLastShoot;
        public byte BombType;
        Random rnd;
        Vector2 GunPos;
        public int Score;

        // Sprite is automated. Direction is same as speed
        public override Vector2 direction
        {
            get { return speed; }
        }

        public Vector2 GunsPos() {
            return position + GunPos;
        }
        public int GetHp() { return hp; }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int MaxHP, byte bombType, int milSecForShoot, Vector2 gunPos, int score, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, rectangleList)
        {
            isFire = false;
            maxHp = MaxHP;
            hp = maxHp;
            MilSecForShoot = milSecForShoot;
            MilSecSinceLastShoot = 0;
            BombType = bombType;
            rnd = new Random();
            GunPos = gunPos;
            Score = score;
            if (milSecForShoot < 0) FireAval = false;
            
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, Vector2 gunPos, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, rectangleList)
        {
            rnd = new Random();
            GunPos = gunPos;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {

            if (FireAval)
            {
                isFire = false;
                // Обновление счетчика выстрела
                MilSecSinceLastShoot -= gameTime.ElapsedGameTime.Milliseconds;
                if (MilSecSinceLastShoot < 0)
                {
                    isFire = true;
                    MilSecSinceLastShoot = rnd.Next(MilSecForShoot / 2, MilSecForShoot * 2);
                }
            }

            // Move sprite based on direction
            position += direction;
            if (position.X < 0 || position.X + frameSize.X > clientBounds.Width ) speed.X = -speed.X;

            base.Update(gameTime, clientBounds);
        }

        public bool isDead(int damage) {

            hp -= damage;
            if (hp <= 0)
            {
                return true;
            }
            return false;
        } // end isDead()


    } // end class AutomatedSprite
} // end namespace

