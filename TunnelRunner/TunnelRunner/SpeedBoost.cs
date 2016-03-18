using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TunnelRunner
{
    class SpeedBoost : Collectibles
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
        public SpeedBoost(int x, int y, int width, int height, bool active) : base (x, y, width, height)
        {
            this.active = active;
        }
    }
}
