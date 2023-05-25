using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace PacManProject
{
    class Player
    {
/*        public Level Level
        {
            get { return level; }
        }
        Level level;*/

        bool isAlive;

        // Physics state

        public Vector2 position;

        public Player(bool isAlive, Vector2 position)
        {
            this.isAlive = isAlive;
            this.position = position;
        }


        public void Update()
        {
            
            if (isAlive)
            {
                
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    position.X += 1;
                }
            }
        }
    }
}
