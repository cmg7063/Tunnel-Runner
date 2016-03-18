using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TunnelRunner
{
    class Obstacles : Collectibles
    {
        // Constructor
        public Obstacles(int x,int y, int width, int height, bool active):base(x , y, width, height, active)
        {

        }
    }
}
