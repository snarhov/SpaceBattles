using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBattles
{
    class BombSprite : Sprite
    {
        // Sprite is automated. Direction is same as speed

        public int damage;

        public override Vector2 direction
        {
            get { return speed; }
        }

        public BombSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int damage, List<Rectangle> rectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, rectangleList)
        {
            this.damage = damage;
        }

        public BombSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, List<Rectangle> rectangleList, int damage, List<Rectangle> collisionRectangleList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame,rectangleList)
        {
            this.damage = damage;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move sprite based on direction
            position += direction;

            

            base.Update(gameTime, clientBounds);
        }
    }
}
