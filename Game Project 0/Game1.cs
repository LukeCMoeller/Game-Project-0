using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;



namespace Game_Project_0
{
    public class Game1 : Game
    {
        //private GameTime astroidtimer;
        private bool gamemove = false;
        private Random random = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Ship ship;
        private Asteroid[] _asteroid;
        private Star[] stars;
        //private double Score;
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
            _asteroid = new Asteroid[]{
                new Asteroid(new Vector2(100, 50))
            };
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
            ship.Update(gameTime);
            ship.color = Color.White;
            foreach (var ash in _asteroid)
            {
                /*if (ash.Bounds.CollidesWith(ship.Bounds))
                {
                    ship.color = Color.Red;
                    gamemove = true;


                }*/
                float waveFrequency = 5f; // Adjust this value to control the frequency of the wave
                float waveAmplitude = 100f; // Adjust this value to control the amplitude of the wave
                float time = (float)gameTime.TotalGameTime.TotalSeconds;

                ash._position.X += 5;
                ash._position.Y = (waveAmplitude * (float)System.Math.Cos(waveFrequency * time)) + _graphics.GraphicsDevice.Viewport.Height / 2;

                if (ash._position.X < 0 || ash._position.X > _graphics.GraphicsDevice.Viewport.Width)
                {
                    ash._position.X += ash._position.X * -1;
                }
                ash.Update(gameTime);
            }
            /*if (!gamemove)
            {
                Score += gameTime.ElapsedGameTime.TotalSeconds;
            }*/
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            /*
            if(gamemove == true)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin();
                ///code below from chat gpt to help center text
                Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

                string lose = "YOU LOSE";
                string score = "Survived " + Score.ToString();
                string escape = "Press escape to quit";

                Vector2 loseSize = _font.MeasureString(lose);
                Vector2 survivedSize = _font.MeasureString(score);
                Vector2 escapeSize = _font.MeasureString(escape);

                Vector2 loseVector = new Vector2(screenCenter.X - loseSize.X / 2, screenCenter.Y - loseSize.Y / 2 - 160);
                Vector2 survivedVector = new Vector2(screenCenter.X - survivedSize.X / 2, screenCenter.Y - survivedSize.Y / 2 - 130);
                Vector2 escapeVector = new Vector2(screenCenter.X - escapeSize.X / 2, screenCenter.Y - escapeSize.Y / 2 - 100);

                _spriteBatch.DrawString(_font, lose, loseVector, Color.Red);
                _spriteBatch.DrawString(_font, score, survivedVector, Color.Red);
                _spriteBatch.DrawString(_font, escape, escapeVector, Color.Red);
                _spriteBatch.End();
            }
            else
            {
            */
            GraphicsDevice.Clear(Color.MediumPurple);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Ship Quest", new Vector2(130, 100), Color.Red, 0f, Vector2.Zero, 3, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_font, "Press escape to quit", new Vector2(170,200), Color.Red);

            ship.Draw(gameTime, _spriteBatch);
            //_spriteBatch.DrawString(_font, $"{gameTime.TotalGameTime:c}", new Vector2(2, 2), Color.Red);
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
            //}

        }
    }
}
