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
        // Paramterized constructor
        public IntenseMilk(int x, int y, int width, int height, bool active) : base(x, y, width, height)
        {
        }
    }
}