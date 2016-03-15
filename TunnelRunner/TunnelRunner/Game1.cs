using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TunnelRunner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    enum GameState { Start, CharacterSelection, Options, Menu, Exit }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game assets
        Character character;
        Texture2D background;

        //GameState button textures
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D optionButton;     //Start, Exit, and Option appear on the starting screen. Option brings you to external tool
        Texture2D menuButton;
        Texture2D norman;
        Texture2D kate;

        //GameState button positions
        Vector2 startPosition;
        Vector2 exitPosition;
        Vector2 optionPosition;
        Vector2 menuPosition;
        Vector2 characterPosition;

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

            startPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            optionPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);
            exitPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 300);

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
            // TODO: Add your drawing code here

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
