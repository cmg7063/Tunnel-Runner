using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel_Game
{
    class Character
    {
        // Attributes
        private int characterHealth;
        private int characterSpeed;
        private string characterName;

        // Constructor
        public Character()
        {

        }

        // Properties
        public string CharacterName
        {
            get { return characterName; }
        }
        public int CharacterSpeed
        {
            get { return characterSpeed; }
        }
        public int CharacterHealth
        {
            get { return characterHealth; }
        }

        // Abstract methods
        abstract public int IncreaseSpeed();

        // Methods
        public void TakeDamage()
        {

        }
    }
}
