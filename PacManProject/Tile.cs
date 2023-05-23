using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacManProject
{
    enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
    }

    enum TileType
    {
        Wall,
        BlankSpace,
        Player,
        Enemy,
    }

    struct Tile
    {
        public Texture2D Texture;
        public TileCollision Collision;

        public const float Width = 40.0f;
        public const float Height = 40.0f;

        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision, TileType type)
        {
            Texture = texture;
            Collision = collision;
        }
    }
}
