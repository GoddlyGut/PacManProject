using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacManProject
{
    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
    }

    public enum TileType
    {
        Wall,
        BlankSpace,
        Player,
        Enemy,
    }

    public struct Tile
    {
        public Texture2D Texture;
        public TileCollision Collision;
        public Vector2 Position;
        public float Rotation = 0.0f;

        public const float Width = 40.0f;
        public const float Height = 40.0f;

        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision, TileType type, Vector2 position, float rotation = 0.0f)
        {
            Texture = texture;
            Collision = collision;
            Position = position;
            Rotation = rotation;
        }
    }
}
