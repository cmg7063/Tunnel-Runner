using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TunnelRunner
{
    public class Character
    {
        // Attributes
        int health;
        List<Collectibles> collected;
        Texture2D characterSprite;
        Rectangle position;
        int level;
        
        // Properties
        public List<Collectibles> Collected
        {
            get { return collected; }
            set { collected = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public Texture2D CharacterSprite
        {
            get { return characterSprite; }
            set { characterSprite = value; }
        }
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        // Default constructor
        public Character()
        {
            health = 3;
        }

        // Parameterized Character() constructor
        public Character(int x, int y, int width, int height)
        {
            position = new Rectangle(x, y, width, height);
            health = 3;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterSprite, position, Color.White);
        }
    }
}
