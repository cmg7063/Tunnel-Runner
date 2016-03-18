using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TunnelRunner
{
    public class Collectibles
    {
        // Attributes
        Texture2D collectibleImage;
        Rectangle position;

        // Properties
        public Texture2D CollectibleImage
        {
            get { return collectibleImage; }
            set { collectibleImage = value; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        // Collectible parameterized constructor
        public Collectibles(int x, int y, int width, int height)
        {
            // Set up the object's rectangle attribute
            position = new Rectangle(x, y, width, height);
        }

        // A virtual method that Draws a spriteBatch object and can be overridden if need be
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(collectibleImage, position, Color.White);
        }
    }
}
