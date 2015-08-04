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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        Color backGround;
        enum GameState { Start, InGame, GameOver };
        GameState currentGameState = GameState.Start;
        SpriteFont menuFont;
        public ExplosionSmokeParticleSystem smoke;
        public ExplosionParticleSystem explosion;
        public SmokePlumeParticleSystem smokePlume;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            backGround = new Color(5, 5, 5, 255);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            
            
         }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            spriteManager.Enabled = false;
            spriteManager.Visible = false;

            // create the particle systems and add them to the components list.
            // we should never see more than one explosion at once
            explosion = new ExplosionParticleSystem(this, 5);
            Components.Add(explosion);

            // but the smoke from the explosion lingers a while.
            smoke = new ExplosionSmokeParticleSystem(this, 5);
            Components.Add(smoke);

            // we'll see lots of these effects at once; this is ok
            // because they have a fairly small number of particles per effect.
            smokePlume = new SmokePlumeParticleSystem(this, 9);
            Components.Add(smokePlume);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuFont = Content.Load<SpriteFont>(@"menuFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            switch (currentGameState)
            {
                case GameState.Start:
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        currentGameState = GameState.InGame;
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                    }
                    break;
                case GameState.InGame:
                    if (spriteManager.gameOver) {
                        currentGameState = GameState.GameOver;
                        spriteManager.Enabled = false;
                        spriteManager.Visible = false;
                    }
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) this.Exit();
                    break;
            }


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backGround);


            switch (currentGameState)
            {
                case GameState.Start:
                    GraphicsDevice.Clear(Color.AliceBlue);

                    // Вывод в заставке текста
                    spriteBatch.Begin();
                    string text = "Space";
                    spriteBatch.DrawString(menuFont, text,
                        new Vector2((Window.ClientBounds.Width / 2)
                        - (menuFont.MeasureString(text).X / 2),
                        (Window.ClientBounds.Height / 2)
                        - (menuFont.MeasureString(text).Y / 2)),
                        Color.SaddleBrown);

                    text = "(Please, press any key!)";
                    spriteBatch.DrawString(menuFont, text,
                        new Vector2((Window.ClientBounds.Width / 2)
                        - (menuFont.MeasureString(text).X / 2),
                        (Window.ClientBounds.Height / 2)
                        - (menuFont.MeasureString(text).Y / 2) + 30),
                        Color.SaddleBrown);

                    spriteBatch.End(); 
                        

                    break;

                case GameState.InGame:
                    break;


                case GameState.GameOver:

                    GraphicsDevice.Clear(Color.AliceBlue);

                     spriteBatch.Begin();
                     string gameover = "Game Over!!!";
                     spriteBatch.DrawString(menuFont, gameover,
                     new Vector2((Window.ClientBounds.Width / 2) - (menuFont.MeasureString(gameover).X / 2),
                            (Window.ClientBounds.Height / 2) - (menuFont.MeasureString(gameover).Y / 2)),
                              Color.SaddleBrown);    	
   
                      spriteBatch.End();

                    break;
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
