using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace PacManProject
{

    enum LevelDesignElements
    {
        Wall,
        EmptySpace,
        Enemy,
        Player,
    }

    public class Level
    {
        public Tile[,] firstLayerTiles;
        public Tile[,] playerTile;
        private bool hasPlayerStart = false;


        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Level(string lvlPath, IServiceProvider serviceProvider, int levelIndex)
        {
            content = new ContentManager(serviceProvider, "Content");

            LoadLevel(lvlPath);
        }

        private void LoadLevel(string lvlPath)
        {
            List<string> lines = new List<string>();
            int width;
            

            using (Stream fileStream = TitleContainer.OpenStream(lvlPath))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = reader.ReadLine();
                    width = line.Length;

                    while (line != null)
                    {
                        

                        if (line.Length != width)
                            throw new Exception(String.Format("The length of line {0} is different from all preceding lines.", lines.Count + 1));

                        CheckForCharacters(line, lines.Count + 1);

                        lines.Add(line);
                        line = reader.ReadLine();

                    }

                    if (lines.Count == 0)
                        // Handle the case when the file is empty
                        throw new Exception("The file is empty.");
                    

                    firstLayerTiles = new Tile[width, lines.Count];
                    playerTile = new Tile[width, lines.Count];

                    if (!hasPlayerStart)
                        throw new System.Exception("ERROR: Each level must have a player start location");

                    for (int y = 0; y < Height; ++y)
                    {
                        for (int x = 0; x < Width; ++x)
                        {
                            // to load each tile.
                            char tileType = lines[y][x];

                            firstLayerTiles[x, y] = LoadTile(tileType, 0, x + 1, y + 1);

                            switch (tileType)
                            {
                                case 'P':
                                case 'p':
                                    playerTile[x, y] = LoadTile(tileType, 1, x + 1, y + 1);
                                    break;
                            }


                        }
                    }
                }
            }

            
        }


        private void CheckForCharacters(string line, int lineNumber)
        {
            foreach (char c in line)
            {
                if (c == '.') { }
                else if (c == '-') { }
                else if (c == 'e' || c == 'E') { }
                else if (c == 'p' || c == 'P') 
                {
                    if (hasPlayerStart)
                        throw new System.Exception("ERROR: Unable to have more than one player start location");

                    hasPlayerStart = true;
                }
                else
                {
                    throw new System.Exception(String.Format("ERROR: Value of {0} is not recognized on line(" + lineNumber + ") !", c == ' ' ? "(SPACE)" : c));
                }
            }
        }



        public int Width
        {
            get { return firstLayerTiles.GetLength(0); }
        }

        public int Height
        {
            get { return firstLayerTiles.GetLength(1); }
        }

        private Tile LoadTile(char type, int layer, int x, int y)
        {
            switch (layer)
            {
                case 0:
                    switch (type)
                    {
                        case '-':
                            return new Tile(Content.Load<Texture2D>("pac_man_wall"), TileCollision.Impassable, TileType.Wall, new Vector2(x, y));
                        case '.':
                            return new Tile(Content.Load<Texture2D>("pac_man_blank_space"), TileCollision.Passable, TileType.BlankSpace, new Vector2(x, y));
                        default:
                            return new Tile(Content.Load<Texture2D>("pac_man_blank_space"), TileCollision.Passable, TileType.BlankSpace, new Vector2(x, y));
                    }
                case 1:
                    switch (type)
                    {
                        case 'P':
                        case 'p':
                            return new Tile(Content.Load<Texture2D>("pac_man_character"), TileCollision.Impassable, TileType.Player, new Vector2(x, y));
                        default:
                            throw new System.Exception(String.Format("ERROR: Value of {0} is not recognized", type));
                    }
                default:
                    throw new System.Exception(String.Format("ERROR: Inavlid layer: ", layer));
            }
            

        }

        public void UpdatePlayerLayerTile(/*Vector2 newPosition*/)
        {
            if (getPlayerTile() != null)
            {
                
                Tile playerTileSpecific = (Tile)getPlayerTile();
                playerTile[(int)playerTileSpecific.Position.X + 1, (int)playerTileSpecific.Position.Y + 1] = playerTile[(int)playerTileSpecific.Position.X, (int)playerTileSpecific.Position.Y];
                //playerTile[(int)playerTileSpecific.Position.X, (int)playerTileSpecific.Position.Y];
            }
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    Texture2D textureFirstLayer = firstLayerTiles[x, y].Texture;
                    

                    Vector2 position = new Vector2(x, y) * Tile.Size;
                    Rectangle rect = new Rectangle((int)position.X, (int)position.Y, 39, 39);

                    if (textureFirstLayer != null)
                        spriteBatch.Draw(textureFirstLayer, rect, Color.White);


                    Texture2D playerLayerTile = playerTile[x, y].Texture;

                    if (playerLayerTile != null)
                        spriteBatch.Draw(playerLayerTile, rect, Color.White);

                }
            }

            
        }

        public Tile? getPlayerTile()
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {

                    Texture2D playerLayerTile = playerTile[x, y].Texture;

                    

                    if (playerLayerTile != null)
                    {
                        return playerTile[x, y];
                    }
                        
                }
            }

            return null;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
        }
    }
}
