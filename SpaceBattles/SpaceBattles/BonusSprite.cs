using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBattles
{
    class BonusSprite : Sprite
    {
        public byte type;
        public override Vector2 direction
        {
            get { return speed; }
        }

          public BonusSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame,byte type, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, rectangleList)
        {
            this.type = type;
        }

          public override void Update(GameTime gameTime, Rectangle clientBounds)
          {

              // Move sprite based on direction
              position += direction;
              if (position.X < 0 || position.X + frameSize.X > clientBounds.Width) speed.X = -speed.X;

              base.Update(gameTime, clientBounds);
          }
    }

    class Bonus
    {
        public Texture2D textureImage;
        public Point frameSize;
        public int collisionOffset;
        public Point currentFrame;
        public Point sheetSize;
        public Vector2 speed;
        public int millisecondsPerFrame;
        public List<Rectangle> rectangleList;
        public byte type;

        public Bonus(Texture2D textureImage, Point frameSize,
           int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
           int millisecondsPerFrame, byte type, List<Rectangle> rectangleList){
               this.textureImage = textureImage;
               this.frameSize = frameSize;
               this.millisecondsPerFrame = millisecondsPerFrame;
               this.collisionOffset = collisionOffset;
               this.currentFrame = currentFrame;
               this.sheetSize = sheetSize;
               this.speed = speed;
               this.rectangleList = rectangleList;
               this.type = type;
        }
         

    }
}
