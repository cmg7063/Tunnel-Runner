using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TunnelRunner
{
    class Character
    {
        // Attributes
        int health;
        string name;
        int initialSpeed;
        List<int> powerUp;
        Texture2D characterSprite;
        Rectangle position;
        
        // Parameterized Character() constructor
        public Character(int x, int y, int width, int height, string name, int initialSpeed)
        {
            position = new Rectangle(x, y, width, height);
            this.name = name;
            this.initialSpeed = initialSpeed;
        }

        // Properties
        public List<int> PowerUp
        {
            get { return powerUp; }
            set { powerUp = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterSprite, position, Color.White);
        }
    }
}
