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
        public Texture2D tunnel;
        public Rectangle rectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tunnel, rectangle, Color.White);
        }
    }

    class Scrolling : TunnelBackground
    {
        public Scrolling(Texture2D newTunnel, Rectangle newRectangle)
        {
            tunnel = newTunnel;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 3;
        }
    }
}
