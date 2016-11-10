using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_menu
{
    class GameObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public int Width;
        public int Height;
        public Boolean Disabled = false;


        /// <summary>
        /// Rectangle for GameObjects with an imported texture
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                if (!Disabled)
                {
                    return new Rectangle
                        (
                            (int)Position.X,
                            (int)Position.Y,
                            Texture.Width,
                            Texture.Height
                        );
                }
                return new Rectangle();
            }
        }


        /// <summary>
        /// Create a new GameObject
        /// </summary>
        /// <param name="texture">Texture to use for the object. A Texture2D Object</param>
        /// <param name="position">Initial position. A Vector2 Object</param>
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }


        /// <summary>
        /// Create a new GameObject
        /// </summary>
        /// <param name="texture">Texture to use for the object. A Texture2D Object</param>
        /// <param name="position">Initial position. A Vector2 Object</param>
        /// <param name="velocity">Initial velocity. A Vector2 Object</param>
        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }

        
        /// <summary>
        /// Faster way of drawing GameObjects
        /// </summary>
        /// <param name="spriteBatch">A SpriteBatch instance</param>
        /// <param name="color">Color for the GameObject to be rendered. Default is white.</param>
        public void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            Color c = color ?? Color.White;
            spriteBatch.Draw(Texture, BoundingBox, c);
        }


        /// <summary>
        /// Returns the GameObject to the interactive world
        /// </summary>
        /// <param name="spriteBatch">A SpriteBatch instance</param>
        public void Enable(SpriteBatch spriteBatch)
        {
            Disabled = false;
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
            spriteBatch.End();
        }


        /// <summary>
        /// Hides the GameObject from any interactions
        /// </summary>
        /// <param name="spriteBatch">A SpriteBatch instance</param>
        public void Disable(SpriteBatch spriteBatch)
        {
            Disabled = true;
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, BoundingBox, new Color(0, 0, 0, 0));
            spriteBatch.End();
        }
    }
}
