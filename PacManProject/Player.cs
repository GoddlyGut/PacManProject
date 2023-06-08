using System;
using System.Collections.Generic;
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

        

        public Level currentLevel;

        public enum Directions
        {
            Left, Right, Up, Down
        }

        readonly Dictionary<Directions, (Vector2 change, float rotation)> directionInfo = new Dictionary<Directions, (Vector2 change, float rotation)>
        {
            { Directions.Left,  (new Vector2(-1,  0), 180.0f) },
            { Directions.Right, (new Vector2(1,   0),   0.0f) },
            { Directions.Up,    (new Vector2(0,  -1), -90.0f) },
            { Directions.Down,  (new Vector2(0,   1),  90.0f) },
        };

        Dictionary<Keys, Directions> keyToDirection = new Dictionary<Keys, Directions>
        {
            { Keys.Right, Directions.Right },
            { Keys.Left,  Directions.Left  },
            { Keys.Up,    Directions.Up    },
            { Keys.Down,  Directions.Down  }
        };

        public Directions? currentDirection = null;

        public Vector2 position;
        public float rotation = 0.0f;

        public int numberOfLives = 3;
        public bool isAlive;


        public Player(bool isAlive, Vector2 position, float rotation, Level currentLevel)
        {
            this.isAlive = isAlive;
            this.position = position;
            this.rotation = rotation;
            this.currentLevel = currentLevel;
        }


        public void Update()
        {
            if (numberOfLives <= 0) 
            {
                isAlive = false;
            }

            if (isAlive && !currentLevel.isLevelCompleted && !GameStats.didWin)
            {
                if (currentDirection != null)
                {
                    if (directionInfo.TryGetValue(currentDirection ?? Directions.Left, out var info))
                    {
                        Vector2 proposedPosition = position + info.change * currentLevel.playerMoveSpeed;

                        if (!currentLevel.isColliding(proposedPosition, this))
                        {
                            position = proposedPosition;
                        }

                        rotation = info.rotation;
                    }
                }
                

                var keyboardState = Keyboard.GetState();

                foreach (var pair in keyToDirection)
                {
                    if (keyboardState.IsKeyDown(pair.Key))
                    {
                        currentDirection = pair.Value;
                        break;
                    }
                }

            }
        }
    }
}
