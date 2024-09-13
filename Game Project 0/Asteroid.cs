using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game_Project_0
{
    /// <summary>
    /// A class representing a ship
    /// </summary>
    public class Asteroid
    {
        private double directionTimer;

        private Texture2D texture;

        public Vector2 _position;

        private int screenWidth;

        private BoundingCircle bounds;
        public BoundingCircle Bounds => bounds;
        public Asteroid(Vector2 position)
        {
            _position = position;
            bounds = new BoundingCircle(_position, 15);
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Asteroid");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            _position.Y += 3;
            bounds.Center = _position;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(_position.Y >850)
            {
                return;
            }
            spriteBatch.Draw(texture, _position, null, Color.White, 0, new Vector2(5, 5), 3, SpriteEffects.None, 0);
        }
    }
}
