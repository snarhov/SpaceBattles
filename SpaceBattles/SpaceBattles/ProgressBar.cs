using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBattles
{
    public class ProgressBar
    {
        // SpriteBatch for drawing 2D image
        SpriteBatch spriteBatch;

        // ProgressBar forefront and background images
        Texture2D texForefront;
        Texture2D texBackground;

        // The background and forefront positon 
        Vector2 backgroundPosition;
        Vector2 forefrontPosition;

        // The offset of forefront image from the background.
        float forefrontStartOffSetX;
        float forefrontStartOffSetY;

        // Current value of progressbar
        public int Value;

        // The Min and Max values of progressbar
        public int Min;
        public int Max;

        // Percent of current value around 100
        float percent;

        // the actual rendering width of forefront image
        float actualWidth;

       
        public ProgressBar(Vector2 position, Texture2D forefront, Texture2D background, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            texForefront = forefront;
            texBackground = background;
            backgroundPosition = position;

            // Calulate the offset for forefront image 
            forefrontStartOffSetY = (texBackground.Width - texForefront.Width) / 2;
            forefrontStartOffSetX = (texBackground.Height - texForefront.Height) / 2;

            // Create the forefront image position
            forefrontPosition = new Vector2(backgroundPosition.X + forefrontStartOffSetX+1,
                backgroundPosition.Y - forefrontStartOffSetY);

            // Intitialize the Min and Max
            Min = 0;
            Max = 100;

        }

        public int GetTextureWidth() {

            return texBackground.Height;
        }

        public void Update(GameTime gameTime)
        {

            if (Value > Max) Value = Max;
            // Compute the actual forefront image for drawing
            percent = (float)Value / 100;
            actualWidth = percent * texForefront.Width;

        }

        public void Draw()
        {
            spriteBatch.Draw(texBackground, backgroundPosition, null, Color.White*.5f, -(float)(Math.PI)/2, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(texForefront, forefrontPosition, new Rectangle(0, 0, (int)actualWidth, texForefront.Height), Color.White, -(float)(Math.PI) / 2, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }
}
