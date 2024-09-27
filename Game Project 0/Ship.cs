using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CollisionExample.Collisions;

namespace Game_Project_0
{
    /// <summary>
    /// A class representing a ship
    /// </summary>
    public class Ship
    {
        private short facing = 0;
        private KeyboardState keyboardState;

        public Color color = Color.White;
        private Texture2D texture;

        private Vector2 position = new Vector2(250, 750);
        private int screenWidth;

        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        public Ship(GraphicsDevice graphicsDevice)
        {
            screenWidth = graphicsDevice.Viewport.Width;
            bounds = new BoundingRectangle(position, 50, 50);
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ShipSprite");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            facing = 0;
            // Apply keyboard movement
            if ((keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) && position.X > 22 && !keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.D)) //numbers weird becuase ShipSprite not made evenly
            {
                 facing = 2;
                 position += new Vector2(-4, 0);
            }
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) && position.X < screenWidth - 26 && !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.A)) //numbers weird becuase ShipSprite not made evenly
            {
                facing = 1;
                position += new Vector2(4, 0);
            }
            // update the bounds
            bounds.X = position.X - 22;
            bounds.Y = position.Y - 26;
        }
        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(facing * 20, 0, 20, 20);
            spriteBatch.Draw(texture, position, source, color, 0, new Vector2(10, 10), 3, SpriteEffects.None, 0);
            
        }
    }
}
