using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;

namespace PacManProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Level level;
        private Player player;
        private bool isPlayerAlive = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            isPlayerAlive = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            level = new Level(@"Content\Levels\0.txt", Services, 0);
            if (level.getPlayerTile() != null)
            {
                Tile possiblePlayerTile = (Tile)level.getPlayerTile();
                player = new Player(isPlayerAlive, possiblePlayerTile.Position);
            }

            Debug.WriteLine(player.position);
            
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            level.UpdatePlayerLayerTile();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();

            level.Draw(gameTime, _spriteBatch);

            // TODO: Add your drawing code here


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}