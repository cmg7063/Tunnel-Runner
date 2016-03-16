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
        int initialSpeed;
        List<int> powerUp;
        Texture2D characterSprite;
        Rectangle position;
        
        // Default constructor
        public Character()
        {

        }
        // Parameterized Character() constructor
        public Character(int x, int y, int width, int height, int initialSpeed)
        {
            position = new Rectangle(x, y, width, height);
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
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterSprite, position, Color.White);
        }
    }
}
