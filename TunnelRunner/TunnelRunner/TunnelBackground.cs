using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TunnelRunner
{
    class TunnelBackground
    {
        // Attributes
        public Texture2D tunnel;
        public Rectangle rectangle;
        public int movingSpeed;

        // Properties
        public int MovingSpeed
        {
            get { return movingSpeed; }
            set { movingSpeed = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tunnel, rectangle, Color.White);
        }
    }

    // Sub-class
    class Scrolling : TunnelBackground
    {
        public Scrolling(Texture2D newTunnel, Rectangle newRectangle, int newSpeed)
        {
            tunnel = newTunnel;
            rectangle = newRectangle;
            movingSpeed = newSpeed;
        }

        public void Update()
        {
            rectangle.X -= movingSpeed;
        }
    }
}
