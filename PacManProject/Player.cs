using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace PacManProject
{
    public class Player
    {
/*        public Level Level
        {
            get { return level; }
        }
        Level level;*/

        bool isAlive;

        // Physics state

        public enum Directions
        {
            Left, Right, Up, Down
        }

        public Directions currentDirection;

        public Vector2 position;
        public float rotation = 0.0f;


        public Player(bool isAlive, Vector2 position, float rotation)
        {
            this.isAlive = isAlive;
            this.position = position;
            this.rotation = rotation;
        }


        public void Update()
        {
            
            if (isAlive)
            {

                switch (currentDirection)
                {
                    case Directions.Left:
                        position.X -= 0.1f;
                        rotation = 180.0f;
                        break;
                    case Directions.Right:
                        position.X += 0.1f;
                        rotation = 0.0f;
                        break;
                    case Directions.Up:
                        position.Y -= 0.1f;
                        rotation = -90.0f;
                        break;
                    case Directions.Down:
                        position.Y += 0.1f;
                        rotation = 90.0f;
                        break;
                }
                
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    currentDirection= Directions.Right;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    currentDirection = Directions.Left;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    currentDirection = Directions.Up;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    currentDirection = Directions.Down;
                }

            }
        }
    }
}
