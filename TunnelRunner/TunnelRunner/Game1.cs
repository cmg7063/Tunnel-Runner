using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TunnelRunner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    enum GameState { Start, CharacterSelection, Options, Playing, Pause, Menu, Exit }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game assets
        Character character;
        Texture2D background;   // Background is referring to the menu background. Game background will be animated

        Vector2 backgroundPos;

        // GameState button textures
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D optionButton;     // Start, Exit, and Option appear on the starting screen. Option brings you to external tool
        Texture2D menuButton;
        Texture2D norman;
        Texture2D kate;

        // GameState button positions
        Vector2 startPos;
        Vector2 exitPos;
        Vector2 optionPos;
        Vector2 menuPos;
        Vector2 characterPos;

        GameState gameState;

        MouseState msState;
        MouseState previousMsState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 400;

            backgroundPos = new Vector2(0, 0);
            startPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            optionPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);
            exitPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 300);

            gameState = GameState.Menu;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startButton = Content.Load<Texture2D>("start");
            optionButton = Content.Load<Texture2D>("option");
            exitButton = Content.Load<Texture2D>("exit");
            background = Content.Load<Texture2D>("background");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameState == GameState.Menu)
            {
                spriteBatch.Draw(background, backgroundPos, Color.White);
                spriteBatch.Draw(startButton, startPos, Color.White);
                spriteBatch.Draw(optionButton, optionPos, Color.White);
                spriteBatch.Draw(exitButton, exitPos, Color.White);
            }
            if (gameState == GameState.Start)
            {
                gameState = GameState.CharacterSelection;
            }
            if (gameState == GameState.CharacterSelection)
            {
                // if choose norman, character.Draw(lkjadflk)
                // if choose kate, character.Draw(lkjadflk)
            }
            if (gameState == GameState.Exit)
            {
                gameState = GameState.CharacterSelection;
            }
            // Draw the game while playing
            if (gameState == GameState.Playing)
            {
                // Draw character, background, etc. in this statement
                spriteBatch.Draw(background, )
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Handle click events
        public void MouseClick(int x, int y)
        {

        }
    }
}
