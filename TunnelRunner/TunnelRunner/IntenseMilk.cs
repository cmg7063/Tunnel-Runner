using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TunnelRunner
{
    class IntenseMilk : Collectibles
    {
        // Attributes
        bool active = false;

        // Properties
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        // Paramterized constructor
        public IntenseMilk(int x, int y, int width, int height, bool active) : base(x, y, width, height)
        {
            this.active = active;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(CollectibleImage, Position, Color.White);
            }
        }
    }
}