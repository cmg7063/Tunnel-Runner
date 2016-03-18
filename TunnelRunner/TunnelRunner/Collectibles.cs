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
        bool active = true;
        int speed;

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

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public int Speed
        {
            get { return speed; }
            set{ speed = value; }
        }

        // Collectible parameterized constructor
        public Collectibles(int x, int y, int width, int height, bool active)
        {
            // Set up the object's rectangle attribute
            position = new Rectangle(x, y, width, height);
            this.active = active;
        }

        // A virtual method that Draws a spriteBatch object and can be overridden if need be
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                spriteBatch.Draw(collectibleImage, position, Color.White);
            }
        }

        public bool CheckCollision(Character character)
        {
            if (active && Position.Intersects(character.Position))
            {
                active = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Moving( )
        {
            position.X -= speed;
        }
    }
}
