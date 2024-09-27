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
using SharpDX.MediaFoundation;

namespace Game_Project_0
{
    /// <summary>
    /// A class representing a ship
    /// </summary>
    public class Asteroid
    {
        Rectangle[] frames = new Rectangle[4];

        private Texture2D texture;

        float timer;
        public Vector2 _position;


        int currentframe;

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

            texture = content.Load<Texture2D>("firerock");
            #region chatgpt provided to remove color
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);  // Get pixel data
            Color green = new Color(0, 255, 0, 255);
            // Loop through the pixel data and replace the green pixels with transparency
            for (int i = 0; i < textureData.Length; i++)
            {
                if (textureData[i] == green)  // Check if pixel is green
                {
                    textureData[i] = Color.Transparent;  // Replace with transparent
                }
            }
            #endregion

            texture.SetData(textureData);
            for (int i = 0; i < 4; i++)
            {
                int row = i / 2;
                int column = i % 2;
                frames[i] = new Rectangle(column * 20, row * 20, 20, 20);
            }
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer > 0.25)
            {
                currentframe++;
                if(currentframe >= 4)
                {
                    currentframe = 0;
                }
                timer = 0f;
            }
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
            spriteBatch.Draw(texture, _position, frames[currentframe], Color.White, 0, new Vector2(10, 10), 3, SpriteEffects.None, 0);
        }
    }
}
