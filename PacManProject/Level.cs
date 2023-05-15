using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        private Tile[,] tiles;


        public Level(string lvlPath)
        {
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

                        CheckForForbiddenCharacters(line, lines.Count + 1);

                        lines.Add(line);
                        line = reader.ReadLine();

                    }

                    if (lines.Count == 0)
                    {
                        // Handle the case when the file is empty
                        throw new Exception("The file is empty.");
                    }

                    tiles = new Tile[width, lines.Count];

                    for (int y = 0; y < Height; ++y)
                    {
                        for (int x = 0; x < Width; ++x)
                        {
                            // to load each tile.
                            char tileType = lines[y][x];
                            tiles[x, y] = LoadTile(tileType, x, y);
                        }
                    }
                }
            }
        }

        private void CheckForForbiddenCharacters(string line, int lineNumber)
        {
            foreach (char c in line)
            {
                if (c == '.') { }
                else if (c == '-') { }
                else if (c == 'e' || c == 'E') { }
                else
                {
                    throw new System.Exception(String.Format("ERROR: Value of {0} is not recognized on line(" + lineNumber + ") !", c == ' ' ? "(SPACE)" : c));
                }
            }
        }

        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        private Tile LoadTile(char type, int x, int y)
        {
            switch (type)
            {
                case '-':
                    return new Tile(null, TileCollision.Impassable);
                default:
                    return new Tile(null, TileCollision.Passable);
            }
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // If there is a visible tile in that position
                    Texture2D texture = tiles[x, y].Texture;
                    if (texture != null)
                    {
                        // Draw it in screen space.
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
        }
    }
}
