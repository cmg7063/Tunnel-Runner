using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TunnelRunner
{
    class Norman : Character
    {
        // Attribute
        int doubleSpeed;    // Some kind of special attribute unique to Norman
        public Norman(int x, int y, int width, int height, string name, int initialSpeed, int doubleSpeed) : base (x, y, width, height, name, initialSpeed)
        {

        }
    }
}
