using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace PacManProject
{
    public static class GameStats
    {
        public static bool didWin = false;
        public static int playerScore = 0;
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int currentLevel = 0;
        int maxNumLevels = 2;
        private Level level;
        private Player player;
        private Rectangle resetButton = new Rectangle(565, 565, 30, 30);
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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ResetGame();


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (level.isLevelCompleted)
            {
                if (maxNumLevels == currentLevel + 1)
                {
                    GameStats.didWin = true;
                    return;
                }
                currentLevel++;
                LoadNextLevel(currentLevel);
            }

            player.Update();
            level.UpdatePlayerLayerTile(player);

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();
            if (resetButton.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                ResetGame();
            }

            base.Update(gameTime);
        }

        private void ResetGame()
        {
            currentLevel = 0;
            GameStats.playerScore = 0;
            level = new Level(@"Content\Levels\0.txt", Services);
            Tile possiblePlayerTile = (Tile)level.playerTile;
            player = new Player(isPlayerAlive, possiblePlayerTile.Position, possiblePlayerTile.Rotation, level);


            Debug.WriteLine(player.position);
        }

        private void LoadNextLevel(int levelNumber)
        {
            level = new Level(@"Content\Levels\" + levelNumber.ToString() + ".txt", Services);
            Tile possiblePlayerTile = (Tile)level.playerTile;
            player = new Player(isPlayerAlive, possiblePlayerTile.Position, possiblePlayerTile.Rotation, level);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();

            level.Draw(gameTime, _spriteBatch, player);

            _spriteBatch.DrawString(Content.Load<SpriteFont>("default"), GameStats.playerScore.ToString(), new Vector2(12,5), Color.White);

            Rectangle heartRectangle1 = new Rectangle(565, 5, 30, 30);
            Rectangle heartRectangle2 = new Rectangle(525, 5, 30, 30);
            Rectangle heartRectangle3 = new Rectangle(485, 5, 30, 30);

            _spriteBatch.DrawString(Content.Load<SpriteFont>("default"), "LEVEL:" + (currentLevel + 1).ToString(), new Vector2(172, 5), Color.White);

            _spriteBatch.Draw(Content.Load<Texture2D>("pac_man_heart"), heartRectangle1, player.numberOfLives > 0 ? Color.Red : Color.Gray);
            _spriteBatch.Draw(Content.Load<Texture2D>("pac_man_heart"), heartRectangle2, player.numberOfLives > 1 ? Color.Red : Color.Gray);
            _spriteBatch.Draw(Content.Load<Texture2D>("pac_man_heart"), heartRectangle3, player.numberOfLives > 2 ? Color.Red : Color.Gray);

            if (!player.isAlive)
            {
                _spriteBatch.DrawString(Content.Load<SpriteFont>("default"), "YOU LOST!", new Vector2(125, 565), Color.White);
                _spriteBatch.Draw(Content.Load<Texture2D>("pac_man_reset_icon"), resetButton, Color.White);
            }

            if (GameStats.didWin)
            {
                _spriteBatch.DrawString(Content.Load<SpriteFont>("default"), "YOU WON!", new Vector2(167, 565), Color.White);
                _spriteBatch.Draw(Content.Load<Texture2D>("pac_man_reset_icon"), resetButton, Color.White);
            }
           
            // TODO: Add your drawing code here


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}