using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceBattles
{
    abstract class Sprite
    {
        protected Texture2D textureImage; // Отображаемый на экране спрайт со спрайт-листа
        protected Point frameSize; //Размер отдельного фрейма в спрайт-листе
        protected Point currentFrame; //индекс текущего фрейма в спрайт-листе
        protected Point sheetSize; // Число столбцов/строк в спрайт-листе
        protected int collisionOffset; // Смещение относительно размеров фрейма для прямоугольника, используемого для определения столкновений
        protected int timeSinceLastFrame = 0; // Число миллисекунд после последнего обновления фрейма
        protected int millisecondsPerFrame; // Число миллисекунд, которое должно пройти между отображением двух соседних фреймов
        protected const int defaultMillisecondsPerFrame = 50; 
        protected Vector2 speed; // Скорость, с которой спрайт перемещается по горизонтали и вертикали
        protected Vector2 position;
        public Vector2 Position {
            get { return position; }
        }
        // Позиция для отрисовки спрайта
        protected List<Rectangle> rectangleList;
        protected List<Rectangle> collisionRectangleList;
         


        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
             int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, List<Rectangle> rectangleList)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame, rectangleList)
        {
            
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, List<Rectangle> rectangleList)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.rectangleList = new List<Rectangle>(rectangleList);
            this.collisionRectangleList = new List<Rectangle>(rectangleList);
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }

            } // end if
            
            // обновление прямоугольников используемых при столкновениях
            for (int i=0; i<rectangleList.Count; ++i){
                collisionRectangleList[i] = new Rectangle((int)position.X + rectangleList[i].X,
                                                         (int)position.Y + rectangleList[i].Y,
                                                         rectangleList[i].Width,
                                                         rectangleList[i].Height);
              

            }    

        } // end Update();

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White, 0, Vector2.Zero,
                    1f, SpriteEffects.None, 0);
        } // end Draw();

        public abstract Vector2 direction
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                    return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        } // end collisionRect

        public bool IsOutOfBounds(Rectangle clientRect) {

            if (position.X < -frameSize.X ||
                position.X > clientRect.Width ||
                position.Y < -frameSize.Y ||
                position.Y > clientRect.Height){
                    return true;
            }

            return false;
        }

        public bool IsCollision(Sprite sprite) {

            // Проверка, если количество прямоугольников = 1 
            if (this.rectangleList.Count == 1)
            {

                if (sprite.rectangleList.Count == 1)
                {
                    
                    return this.collisionRectangleList[0].Intersects(sprite.collisionRectangleList[0]);
                }
                else

                    for (int i = 0; i < sprite.rectangleList.Count; i++)
                    {
                        if (this.collisionRectangleList[0].Intersects(sprite.collisionRectangleList[i]))
                        {
                            return true;
                        }
                    }

            }   // Конец случая для одиночного прямоугольника 
            else 
            {

                if (sprite.rectangleList.Count == 1)
                {
                    if (this.collisionRectangleList[0].Intersects(sprite.collisionRectangleList[0]))
                    {
                        for (int i = 1; i < this.rectangleList.Count; i++)
                        {
                            if (this.collisionRectangleList[i].Intersects(sprite.collisionRectangleList[0]))
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (this.collisionRectangleList[0].Intersects(sprite.collisionRectangleList[0]))
                    {
                        for (int i = 1; i < sprite.rectangleList.Count; i++)
                        {
                            for (int j = 1; j < this.rectangleList.Count; j++)
                            {

                                if (this.collisionRectangleList[j].Intersects(sprite.collisionRectangleList[i]))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                } 
            
            }


            return false;
        } // end IsCollision 



    } // end class Sprite

    class iRectangle {

            public Rectangle rectangle;
            public Rectangle collisionRectangle; // прямоугольник для столкновений
            
            public iRectangle(Rectangle rect){
            this.rectangle=rect;
            this.collisionRectangle = rect;
            }
            

     } // end class iRectangle



} // end namespace
