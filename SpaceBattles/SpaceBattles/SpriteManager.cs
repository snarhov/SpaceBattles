using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SpaceBattles
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        

        SpriteBatch spriteBatch;
        UserControlledSprite playerShip;
        List<Sprite> spriteList = new List<Sprite>();
        List<AutomatedSprite> EnemySpriteList = new List<AutomatedSprite>();
        List<BombSprite> BombSpriteList = new List<BombSprite>();
        List<BombSprite> EnemyBombSpriteList = new List<BombSprite>();
        List<Enemy> enemyList = new List<Enemy>(); 
        List<Bombs> bombsList = new List<Bombs>();
        List<BonusSprite> bonusSpriteList = new List<BonusSprite>();
        List<Bonus> bonusList = new List<Bonus>();
        List<Rectangle> rectangleList;
        SpriteFont gameOverFont, inGameFont;
        public bool gameOver;
        public static Random rnd;
        int Score;
        Rectangle gameField;
        const int barFieldWidth = 150;

        Game1 game;
        ProgressBar hpBar,expBar, heatingBar;
        

        public SpriteManager(Game1 game)
            : base(game)
        {
            this.game = game;
            rnd = new Random();
            gameField = Game.Window.ClientBounds;
            gameField.Width -= barFieldWidth;
            // TODO: Construct any child components here

            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            gameOverFont = Game.Content.Load<SpriteFont>(@"GameOver");
            inGameFont = Game.Content.Load<SpriteFont>(@"inGameFont");

            hpBar = new ProgressBar(new Vector2(game.Window.ClientBounds.Width - barFieldWidth + 10, game.Window.ClientBounds.Height - 40), Game.Content.Load<Texture2D>(@"Images/hpBar2"), Game.Content.Load<Texture2D>(@"Images/ProgressBarBackground2"), spriteBatch);
            expBar = new ProgressBar(new Vector2(game.Window.ClientBounds.Width - barFieldWidth +10 + (int)(hpBar.GetTextureWidth()*1.2), game.Window.ClientBounds.Height - 40), Game.Content.Load<Texture2D>(@"Images/expBar"), Game.Content.Load<Texture2D>(@"Images/ProgressBarBackground2"), spriteBatch);
            heatingBar = new ProgressBar(new Vector2(game.Window.ClientBounds.Width - barFieldWidth + 10 + (int)(hpBar.GetTextureWidth() * 1.2) + (int)(expBar.GetTextureWidth() * 1.2), game.Window.ClientBounds.Height - 40), Game.Content.Load<Texture2D>(@"Images/heatingBar"), Game.Content.Load<Texture2D>(@"Images/ProgressBarBackground2"), spriteBatch); 
           
            
            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 5, 7));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/Bomb0"),
                   new Point(5, 7), 10, new Point(0, 0),
                   new Point(1, 1), new Vector2(0, -2), 20, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 5, 9));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/Bomb1"),
                    new Point(5, 9), 10, new Point(0, 0),
                    new Point(1, 1), new Vector2(0, -2), 34, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 5, 12));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/Bomb2"),
                   new Point(5, 12), 10, new Point(0, 0),
                   new Point(1, 1), new Vector2(0, -2), 50, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 10, 14));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/Bomb3"),
                   new Point(10, 14), 10, new Point(0, 0),
                   new Point(1, 1), new Vector2(0, -2), 100, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 4, 11));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/freeze0"),
                   new Point(4, 11), 10, new Point(0, 0),
                   new Point(1, 1), new Vector2(0, -2), 25, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 10, 14));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/freeze1"),
                   new Point(10, 14), 10, new Point(0, 0),
                   new Point(1, 1), new Vector2(0, -2), 125, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 11, 42));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/freeze2"),
                   new Point(11, 42), 10, new Point(0, 0),
                   new Point(1, 1), new Vector2(0, -2), 500, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 11,11));
            bombsList.Add(new Bombs(Game.Content.Load<Texture2D>(@"Images/bombs/greenCircle"),
                   new Point(11, 11), 10, new Point(0, 0),
                   new Point(4, 1), new Vector2(0, -2), 100, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0,0,19,27));
            enemyList.Add(new Enemy(Game.Content.Load<Texture2D>(@"Images/enemys/enemy1"),
                new Point(19, 27), 10, new Point(0, 0),
                new Point(4, 1), new Vector2(1.5f,1.5f), 100, 0, 3500, new Vector2(9,27), 30, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 37, 28));
            enemyList.Add(new Enemy(Game.Content.Load<Texture2D>(@"Images/enemys/enemy2"),
                new Point(37, 28), 10, new Point(0, 0),
                new Point(1, 1), new Vector2(1, 1), 200, 4, 4500, new Vector2(19, 27), 60, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 48, 49));
            enemyList.Add(new Enemy(Game.Content.Load<Texture2D>(@"Images/enemys/bumerang0"),
                new Point(48, 49), 10, new Point(0, 0),
                new Point(4, 1), new Vector2(3, 3), 400, 6, -1, new Vector2(24, 27), 90, rectangleList));

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 118, 138));
            playerShip = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/PlayerShipAnim"),
                Vector2.Zero, new Point(118, 138), 10, new Point(0, 0),
                new Point(4, 1), new Vector2(4, 4), rectangleList);

            rectangleList = new List<Rectangle>();
            rectangleList.Clear();
            rectangleList.Add(new Rectangle(0, 0, 58, 58));
            bonusList.Add(new Bonus(
                Game.Content.Load<Texture2D>(@"Images/bonuses/bonusLifeCh"),
                new Point(58, 58), 10, new Point(0, 0),
                new Point(4, 1), new Vector2(1, 1), 100, 0, rectangleList));

            
            base.LoadContent();
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            gameOver = false;
            Score = 0;
            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            playerShip.Update(gameTime, gameField);

            hpBar.Value = (int)(100 * (float)playerShip.GetHp()/playerShip.MaxHp);
            hpBar.Update(gameTime);
            expBar.Value = LevelManager.expPer;
            expBar.Update(gameTime);
            heatingBar.Value = (int)playerShip.OverHeating;
            heatingBar.Update(gameTime);

            // Добавление врагов
            if (EnemyManager.isNext(gameTime)) 
            {

                EnemyManager.newEnemy(rnd,enemyList.Count, LevelManager.Lvl);
                
                EnemySpriteList.Add(new AutomatedSprite(enemyList[EnemyManager.enemyType].textureImage,
                    new Vector2(rnd.Next(gameField.Width - enemyList[EnemyManager.enemyType].frameSize.X), -enemyList[EnemyManager.enemyType].frameSize.Y),
                    enemyList[EnemyManager.enemyType].frameSize,
                    enemyList[EnemyManager.enemyType].collisionOffset,
                    enemyList[EnemyManager.enemyType].currentFrame,
                    enemyList[EnemyManager.enemyType].sheetSize,
                    Vector2.Multiply(enemyList[EnemyManager.enemyType].speed, EnemyManager.speed),
                    enemyList[EnemyManager.enemyType].MaxHp,
                    enemyList[EnemyManager.enemyType].BombType,
                    enemyList[EnemyManager.enemyType].MilSecForShoot,
                    enemyList[EnemyManager.enemyType].GunPos,
                    enemyList[EnemyManager.enemyType].Score,
                    enemyList[EnemyManager.enemyType].rectangleList
                    ));
            }

            // Добавление бонусов
            if (BonusManager.isNext(gameTime))
            {

                BonusManager.newBonus(rnd, bonusList.Count, LevelManager.Lvl);

                bonusSpriteList.Add(new BonusSprite(bonusList[BonusManager.bonusType].textureImage,
                    new Vector2(rnd.Next(gameField.Width - bonusList[BonusManager.bonusType].frameSize.X), -bonusList[BonusManager.bonusType].frameSize.Y),
                    bonusList[BonusManager.bonusType].frameSize,
                    bonusList[BonusManager.bonusType].collisionOffset,
                    bonusList[BonusManager.bonusType].currentFrame,
                    bonusList[BonusManager.bonusType].sheetSize,
                    Vector2.Multiply(bonusList[BonusManager.bonusType].speed, BonusManager.speed),
                    bonusList[BonusManager.bonusType].millisecondsPerFrame,
                    bonusList[BonusManager.bonusType].type,
                    bonusList[BonusManager.bonusType].rectangleList
                    ));
            }

            // Добавление спрайтов огня игрока
            #region AddFireSprite
            if (playerShip.isFire == true)
            {
               

                if (playerShip.GunsCount == 1)
                {
                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosMid(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                } else if (playerShip.GunsCount == 2)
                
                {
                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosLeftOne(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosRightOne(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                } else if  (playerShip.GunsCount == 3)
                {
                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosMid(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosLeftTwo(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        new Vector2(-GetNewSpeed(bombsList[playerShip.BombType].speed), -GetNewSpeed(bombsList[playerShip.BombType].speed)),
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosRightTwo(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        new Vector2(GetNewSpeed(bombsList[playerShip.BombType].speed), -GetNewSpeed(bombsList[playerShip.BombType].speed)),
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                } else if (playerShip.GunsCount == 4)
                
                {
                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosLeftOne(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosRightOne(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));
                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosLeftTwo(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        new Vector2(-GetNewSpeed(bombsList[playerShip.BombType].speed), -GetNewSpeed(bombsList[playerShip.BombType].speed)),  
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosRightTwo(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        new Vector2(GetNewSpeed(bombsList[playerShip.BombType].speed), -GetNewSpeed(bombsList[playerShip.BombType].speed)),
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                }
                else if (playerShip.GunsCount == 5) { 
                   BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosLeftOne(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        new Vector2(-GetNewSpeed(bombsList[playerShip.BombType].speed), -GetNewSpeed(bombsList[playerShip.BombType].speed)),             
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosRightOne(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        new Vector2(GetNewSpeed(bombsList[playerShip.BombType].speed), -GetNewSpeed(bombsList[playerShip.BombType].speed)),     
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosLeftTwo(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosRightTwo(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                    BombSpriteList.Add(new BombSprite(
                        bombsList[playerShip.BombType].textureImage,
                        playerShip.GunPosMid(),
                        bombsList[playerShip.BombType].frameSize,
                        bombsList[playerShip.BombType].collisionOffset,
                        bombsList[playerShip.BombType].currentFrame,
                        bombsList[playerShip.BombType].sheetSize,
                        bombsList[playerShip.BombType].speed,
                        bombsList[playerShip.BombType].damage,
                        bombsList[playerShip.BombType].rectangleList));

                }

            } // Конец добавления спрайтов огня игрока
            #endregion

            // Добавление спрайтов огня врага

            for (int i = 0; i < EnemySpriteList.Count; i++)
            {
                if (EnemySpriteList[i].isFire == true) {
                    EnemyBombSpriteList.Add(new BombSprite(
                       bombsList[EnemySpriteList[i].BombType].textureImage,
                       EnemySpriteList[i].GunsPos(),
                       bombsList[EnemySpriteList[i].BombType].frameSize,
                       bombsList[EnemySpriteList[i].BombType].collisionOffset,
                       bombsList[EnemySpriteList[i].BombType].currentFrame,
                       bombsList[EnemySpriteList[i].BombType].sheetSize,
                       new Vector2(bombsList[EnemySpriteList[i].BombType].speed.X, -bombsList[EnemySpriteList[i].BombType].speed.Y),
                       bombsList[EnemySpriteList[i].BombType].damage,
                       bombsList[EnemySpriteList[i].BombType].rectangleList));
                }

            } // Конец добавленея вражеского огня



            // Обновление спрайтов врагов
                for (int i = 0; i < EnemySpriteList.Count; ++i)
                {
                    Sprite s = EnemySpriteList[i];
                    s.Update(gameTime, gameField);

                    // Удаляем объект, если он вне поля
                    if (s.IsOutOfBounds(gameField))
                    {

                        EnemySpriteList.RemoveAt(i);
                        --i;
                    }

                }

            // Обновление спрайтов огня
            for (int i = 0; i < BombSpriteList.Count; ++i)
            {
                Sprite s = BombSpriteList[i];
                s.Update(gameTime, gameField);

                // Удаляем объект, если он вне поля
                if (s.IsOutOfBounds(gameField))
                {

                    BombSpriteList.RemoveAt(i);
                    --i;
                }    

            }


            // Обновление спрайтов вражеского огня
            for (int i = 0; i < EnemyBombSpriteList.Count; ++i)
            {
                Sprite s = EnemyBombSpriteList[i];
                s.Update(gameTime, gameField);

                // Удаляем объект, если он вне поля
                if (s.IsOutOfBounds(gameField))
                {

                    EnemyBombSpriteList.RemoveAt(i);
                    --i;
                }

            }

            // Обновление спрайтов бонусов
            for (int i = 0; i < bonusSpriteList.Count; ++i)
            {
                Sprite s = bonusSpriteList[i];
                s.Update(gameTime, gameField);

                // Удаляем объект, если он вне поля
                if (s.IsOutOfBounds(gameField))
                {

                    bonusSpriteList.RemoveAt(i);
                    --i;
                }

            }
            
            //---------------------------------------------------------------------
            // Проверка столконовений (!оптимизировать!)
            //---------------------------------------------------------------------
            
            // Столкновение выстрелов игрока с врагами
            for (int i = 0; i < EnemySpriteList.Count; i++) {

                for (int j = 0; j < BombSpriteList.Count; j++) {
                       
                    if (EnemySpriteList[i].IsCollision(BombSpriteList[j])) {
                       
                        
                        if (EnemySpriteList[i].isDead(BombSpriteList[j].damage)) {
                            Score += EnemySpriteList[i].Score;
                            LevelManager.addExp(EnemySpriteList[i].Score);
                            game.explosion.AddParticles(EnemySpriteList[i].Position);
                            game.smoke.AddParticles(EnemySpriteList[i].Position);
                            EnemySpriteList.RemoveAt(i); 
                            }

                        BombSpriteList.RemoveAt(j);
                        break;
                        
                    }

                }
 
            }

            // Столкновение выстрелов врагов с игроком
            for (int j = 0; j < EnemyBombSpriteList.Count; j++)
            {           
                if (playerShip.IsCollision(EnemyBombSpriteList[j]))
                {
                    if (playerShip.isDead(EnemyBombSpriteList[j].damage)) {
                        gameOver = true;
                    }
                        EnemyBombSpriteList.RemoveAt(j);
                        break;
                 }

            }

            // Столкновение игрока с врагами

            for (int j = 0; j < EnemySpriteList.Count; j++)
            {
                if (playerShip.IsCollision(EnemySpriteList[j]))
                {
                    if (playerShip.isDead(EnemySpriteList[j].GetHp()))
                    {
                        gameOver = true;
                    }
                    Score += EnemySpriteList[j].Score;
                    LevelManager.addExp(EnemySpriteList[j].Score);
                    EnemySpriteList.RemoveAt(j);
                    
                    break;
                }

            }

            // Столкновение снарядов

            for (int i = 0; i < EnemyBombSpriteList.Count; i++)
            {

                for (int j = 0; j < BombSpriteList.Count; j++)
                {

                    if (EnemyBombSpriteList[i].IsCollision(BombSpriteList[j]))
                    {
                        EnemyBombSpriteList.RemoveAt(i);
                        BombSpriteList.RemoveAt(j);
                        Score++;
                        break;

                    }

                }

            }

            // Столкновение игрока с бонусами

            for (int j = 0; j < bonusSpriteList.Count; j++)
            {
                if (playerShip.IsCollision(bonusSpriteList[j]))
                {

                    playerShip.UseBonus(bonusSpriteList[j].type);
                    bonusSpriteList.RemoveAt(j);
                    break;
                }

            }
            
            //--------------------------------------------------------------------
            // Конец описывания логики столкновений
            //--------------------------------------------------------------------

                LevelManager.Update(gameTime);

                

                base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);    // Draw the player
            playerShip.Draw(gameTime, spriteBatch);

            // Отрисовываем все спрайты
               foreach (Sprite s in EnemySpriteList)
                   s.Draw(gameTime, spriteBatch);
               foreach (Sprite s in BombSpriteList)
                   s.Draw(gameTime, spriteBatch);
               foreach (Sprite s in EnemyBombSpriteList)
                   s.Draw(gameTime, spriteBatch);
               foreach (Sprite s in bonusSpriteList)
                   s.Draw(gameTime, spriteBatch);
                /*
                   if (s.collisionRect.Intersects(player.collisionRect))
                   Game.Exit();
               */

               string score = "Очки: " + Score;
               string healt = "Здоровье: " + playerShip.GetHp();
               string level = "Уровень: " + LevelManager.Lvl;
               string percent = "Опыт: " + LevelManager.expPer;
               float maxMes;
               if (inGameFont.MeasureString(score).X >= inGameFont.MeasureString(healt).X)
               {
                   maxMes = inGameFont.MeasureString(score).X+5;
               }
               else { maxMes = inGameFont.MeasureString(healt).X+5; }

               Vector2 textCoordinate = new Vector2(Game.Window.ClientBounds.Width - maxMes, 0);

               spriteBatch.DrawString(inGameFont, score, textCoordinate, Color.Gold);
               textCoordinate.Y += inGameFont.MeasureString(score).Y;   

               spriteBatch.DrawString(inGameFont, healt, textCoordinate, Color.Gold);
               textCoordinate.Y += inGameFont.MeasureString(healt).Y;

               spriteBatch.DrawString(inGameFont, level, textCoordinate, Color.Gold);
               textCoordinate.Y += inGameFont.MeasureString(level).Y;

               spriteBatch.DrawString(inGameFont, percent, textCoordinate, Color.Gold);
               textCoordinate.Y += inGameFont.MeasureString(percent).Y;

               hpBar.Draw();
               expBar.Draw();
               heatingBar.Draw();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        float GetNewSpeed(Vector2 speed) {

            double r =  - (double) speed.Y;
            r =  Math.Sqrt(r);
            return (float)r;
            
        }

        public static float RandomBetween(float min, float max)
        {
            return min + (float)rnd.NextDouble() * (max - min);
        }

    }

}
