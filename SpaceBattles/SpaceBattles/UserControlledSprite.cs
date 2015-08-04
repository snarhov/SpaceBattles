using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceBattles
{
    class UserControlledSprite : Sprite
    {
        int hp;
        const int maxHp = 1000;
        public int MaxHp {
            get { return maxHp; }
        }
        public bool isFire;
        const int defaultMilSecForShoot = 300;
        int MilSecForShoot, MilSecSinceLastShoot;
        public byte BombType;
        public byte GunsCount;
        public float OverHeating = 1.0f;
        const float heatingSpeed = .1f;
        const float coolingSpeed = 0.03f;
        float dh, maxDh;


        public Vector2 GunPosMid() {
             return position + new Vector2(57,70);
        }
        public Vector2 GunPosLeftOne()
        {
            return position + new Vector2(13, 27);
        }
        public Vector2 GunPosLeftTwo()
        {
            return position + new Vector2(3, 40);
        }
        public Vector2 GunPosRightOne()
        {
            return position + new Vector2(106, 27);
        }
        public Vector2 GunPosRightTwo()
        {
            return position + new Vector2(116, 40);
        }
        public int GetHp() {
            return hp;
        }

        // Get direction of sprite based on player input and speed
        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;
                isFire = false;
                // If player pressed arrow keys, move the sprite
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    inputDirection.Y += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    if (MilSecSinceLastShoot < 0 && OverHeating<100) 
                    {
                        isFire = true;
                        MilSecSinceLastShoot = MilSecForShoot;
                        dh = (heatingSpeed * OverHeating) * (float)(99+GunsCount)/100;
                        maxDh = (float)(3 + GunsCount) / 2;
                        if (dh > maxDh) { OverHeating += maxDh; } else { OverHeating += dh;}
                    }
                
                // If player pressed the gamepad thumbstick, move the sprite
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                return inputDirection * speed;
            }
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, rectangleList)
        {
            isFire = false;
            hp = maxHp;
            MilSecForShoot = defaultMilSecForShoot;
            BombType = 7;
            GunsCount = 5;

        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, bool isfire, int Hp, int milSecforShoot, byte bombType, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, rectangleList)
        {
            isFire = isfire;
            hp = Hp;
            MilSecForShoot = milSecforShoot;
            BombType = bombType;
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, rectangleList)
        {
        }

        

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
         

                
            MilSecSinceLastShoot -= gameTime.ElapsedGameTime.Milliseconds;

            if (OverHeating < 1.0f) { OverHeating = 1.0f; } 

            if (MilSecSinceLastShoot < 0) {

                OverHeating += (MilSecSinceLastShoot / MilSecForShoot) * coolingSpeed;
            
            }
            // Move the sprite based on direction
            position += direction;

            

            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y != clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            

            base.Update(gameTime, clientBounds);
        }

        public bool isDead(int damage)
        {

            hp -= damage;
            if (hp <= 0)
            {
                return true;
            }
            return false;
        } // end isDead()

        public void UseBonus(byte bonusType) { 
            /// <bonus type>
            ///  0 - health
            ///  1 - guns++
            ///  10 - 19 bomb type  
            /// </bonus type>

            switch (bonusType)
            {
                case 0:
                    {
                        hp += (int)(maxHp * 0.2);
                        if (hp > maxHp) hp = maxHp;
                    }
                    break;

                case 1:
                    {

                    }
                    break;

                case 2:
                    {

                    }
                    break;

            } // End switch

        } // End UseBonus()
    }

}
