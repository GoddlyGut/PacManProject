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

        bool isAlive;

        public Level currentLevel;

        public enum Directions
        {
            Left, Right, Up, Down
        }

        public Directions currentDirection;

        public Vector2 position;
        public float rotation = 0.0f;


        public Player(bool isAlive, Vector2 position, float rotation, Level currentLevel)
        {
            this.isAlive = isAlive;
            this.position = position;
            this.rotation = rotation;
            this.currentLevel = currentLevel;
        }


        public void Update()
        {
            
            if (isAlive)
            {


                switch (currentDirection)
                {
                    case Directions.Left:
                        if (!currentLevel.isColliding(this))
                        {
                            position.X -= currentLevel.playerMoveSpeed;
                        }

                        rotation = 180.0f;
                        break;
                    case Directions.Right:
                        if (!currentLevel.isColliding(this))
                        {
                            position.X += currentLevel.playerMoveSpeed;
                        }

                        rotation = 0.0f;
                        break;
                    case Directions.Up:
                        if (!currentLevel.isColliding(this))
                        {
                            position.Y -= currentLevel.playerMoveSpeed;
                        }

                        rotation = -90.0f;
                        break;
                    case Directions.Down:
                        if (!currentLevel.isColliding(this))
                        {
                            position.Y += currentLevel.playerMoveSpeed;
                        }

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
