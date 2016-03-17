using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TunnelRunner
{
    class Obstacles:Collectible
    {
        bool touched;
        public bool Touched
        {
            get { return touched; }
            set { touched = value; }
        }
        //constructor
        public Obstacles(int x,int y, int width, int height):base(x,y,width,height)
        {
            touched = false;
        }
        public bool CheckCollision(Character chara)
        {
            if(touched==false)
            {
                if(Position.Intersects(chara.Position))
                {
                    touched = true;
                    return true;
                }
                else
                { return false; }
            }
            else { return false; }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (touched == false)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
