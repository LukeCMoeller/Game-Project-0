using System;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;



namespace Game_Project_0
{
    public class Game1 : Game
    {
        private float difficulty = 3;
        private float astroidtimer;
        private bool gamemove = false;
        private Random random = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Ship ship;
        private List<Asteroid> _asteroid = new List<Asteroid>();
        private Star[] stars;
        private double Score;
        private bool gameStart = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            ship = new Ship(GraphicsDevice);
            _asteroid.Add(new Asteroid(new Vector2(GraphicsDevice.Viewport.Width / 2, -50)));
            stars = new Star[10];
            for (int i = 0; i < 10; i++)
            {
                stars[i] = new Star(new Vector2(random.Next(30, 470), random.Next(30, 770)));
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Phy");
            ship.LoadContent(Content);
            foreach (var ash in _asteroid)
            {
                ash.LoadContent(Content);
            }
            foreach (var s in stars)
            {
                s.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (gameStart == true)
            {
                astroidtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (astroidtimer > difficulty)
                {
                    _asteroid.Add(new Asteroid(new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), -50)));
                    _asteroid[_asteroid.Count - 1].LoadContent(Content);
                    astroidtimer = 0;
                    if(difficulty - 0.2f >= 0)
                    {
                        difficulty -= 0.2f;
                    }
                }


                ship.Update(gameTime);
                ship.color = Color.White;
                foreach (var ash in _asteroid)
                {
                    if (ash.Bounds.CollidesWith(ship.Bounds))
                    {
                        ship.color = Color.Red;
                        gamemove = true;
                    }
                    ash.Update(gameTime);
                }
                if (!gamemove)
                {
                    Score += gameTime.ElapsedGameTime.TotalSeconds;
                }
                // TODO: Add your update logic here

                base.Update(gameTime);
            }
            else
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right)){
                    gameStart = true;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);
            if (gamemove == true)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin();

                string lose = "YOU LOSE";
                string score = "Survived " + Score.ToString();
                string escape = "Press escape to quit";

                _spriteBatch.DrawString(_font, lose, printMiddle(lose, 100, 1), Color.Red);
                _spriteBatch.DrawString(_font, score, printMiddle(score, 130, 1), Color.Red);
                _spriteBatch.DrawString(_font, escape, printMiddle(escape, 160, 1), Color.Red);
                _spriteBatch.End();


            }
            else if(gameStart != true)
            {
                string title = "Ship Quest";
                string tostart = "Press Left or right to start";
                string esxape = "Press escape to quit";
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_font, title, printMiddle(title, 300, 3f), Color.Red, 0f, Vector2.Zero, 3, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, tostart, printMiddle(tostart, 250, 1f), Color.Red);
                _spriteBatch.DrawString(_font, esxape, printMiddle(esxape, 225, 1f), Color.Red);
                _spriteBatch.End();
            }
            else
            {

                _spriteBatch.Begin();
            ship.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(_font, $"{gameTime.TotalGameTime:c}", new Vector2(2, 2), Color.Red);
                foreach (var ash in _asteroid)
                {
                    ash.Draw(gameTime, _spriteBatch);
                }
            foreach (var s in stars)
            {
                s.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
                base.Draw(gameTime);
            }



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">the string</param>
        /// <param name="i">how high on the y acces to print</param>
        /// <returns></returns>
        public Vector2 printMiddle(string s, int i, float scale)
        {
            Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            Vector2 length = _font.MeasureString(s) * scale;
            Vector2 toprint = new Vector2(screenCenter.X - length.X / 2, screenCenter.Y - length.Y / 2 - i);
            return toprint;
        }

    }
}
/* how to make cool wavy asteroid
 * float waveFrequency = 5f; // Adjust this value to control the frequency of the wave
                float waveAmplitude = 100f; // Adjust this value to control the amplitude of the wave
                float time = (float)gameTime.TotalGameTime.TotalSeconds;

                ash._position.X += 5;
                ash._position.Y = (waveAmplitude * (float)System.Math.Cos(waveFrequency * time)) + _graphics.GraphicsDevice.Viewport.Height / 2;

                if (ash._position.X < 0 || ash._position.X > _graphics.GraphicsDevice.Viewport.Width)
                {
                    ash._position.X += ash._position.X * -1;
                }
                ash.Update(gameTime);
 */
