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
        public int preSpeed;
        public int preXpos;

        // Properties
        public int MovingSpeed
        {
            get { return movingSpeed; }
            set { movingSpeed = value; }
        }
        public int PreSpeed
        {
            get { return preSpeed; }
            set { preSpeed = value; }
        }
        public int PreXpos
        {
            get { return preXpos;}
            set { preXpos = value; }
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
            preSpeed = newSpeed;
        }

        public void Update()
        {
            rectangle.X -= movingSpeed;
            preXpos = rectangle.X;
            preSpeed = movingSpeed;
        }
    }
}
