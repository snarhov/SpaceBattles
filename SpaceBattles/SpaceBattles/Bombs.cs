using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBattles
{
    class Bombs
    {
        
        
          public Texture2D textureImage; // Отображаемый на экране спрайт со спрайт-листа
          public Point frameSize; //Размер отдельного фрейма в спрайт-листе
          public  Point currentFrame; //индекс текущего фрейма в спрайт-листе
          public  Point sheetSize; // Число столбцов/строк в спрайт-листе
          public  int collisionOffset; // Смещение относительно размеров фрейма для прямоугольника, используемого для определения столкновений
          public Vector2 speed;
          public int damage;
          public List<Rectangle> rectangleList;
          
          public Bombs(Texture2D textureImage, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int damage, List<Rectangle> rectangleList)
        {
            this.textureImage = textureImage;   
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.rectangleList = rectangleList;
            this.damage = damage;
            
        }
        
    }
}
