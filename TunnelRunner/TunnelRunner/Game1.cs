using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

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
        Texture2D kate;
        Texture2D norman;
        Texture2D kateSprite;
        Texture2D normanSprite;
        Texture2D loadingScreen;

        Vector2 backgroundPos;
        Vector2 normanPos;
        Vector2 katePos;

        const int PLAYER_W = 124;
        const int PLAYER_H = 200;

        // GameState button textures
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D charaSelButt;
        Texture2D optionButton;     // Start, Exit, Character Selection, and Option appear on the starting screen. Option brings you to external tool
        Texture2D menuButton;
        Texture2D normanButton;
        Texture2D kateButton;
        const int BUTT_W = 150;
        const int BUTT_H = 75;

        // GameState button positions
        Vector2 startButtPos;
        Vector2 exitButtPos;
        Vector2 optionButtPos;
        Vector2 menuPos;
        Vector2 characterPos;
        Vector2 charaSelButtPos;

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
            normanPos = new Vector2(350, (GraphicsDevice.Viewport.Height / 2) - 50);
            katePos = new Vector2(400, (GraphicsDevice.Viewport.Height / 2) - 50);

            // There is 25px gap in between each button
            startButtPos = new Vector2(12,250);
            charaSelButtPos = new Vector2(187, 250);
            optionButtPos = new Vector2( 362, 250);
            exitButtPos = new Vector2( 537, 250);
            character = new Character();
            character.CharacterSprite = kateSprite;
            gameState = GameState.Menu;

            msState = Mouse.GetState();
            previousMsState = msState;
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

            /*normanSprite = Content.Load<Texture2D>("norman");
            kateSprite = Content.Load<Texture2D>("kate");

            loadingScreen = Content.Load<Texture2D>("loading");*/

            startButton = Content.Load<Texture2D>("Start");
            optionButton = Content.Load<Texture2D>("Options");
            exitButton = Content.Load<Texture2D>("Exit");
            charaSelButt = Content.Load<Texture2D>("Selection");
            background = Content.Load<Texture2D>("background");
            kateSprite = Content.Load<Texture2D>("kateSprite");
            kate = Content.Load<Texture2D>("kate");
            norman = Content.Load<Texture2D>("norman");

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
            switch (gameState)
            {
                case GameState.Menu:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;

                    if (gameState == GameState.Playing)
                    {
                        LoadGame();
                    }
                    break;
                case GameState.CharacterSelection:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    break;
                default:
                    break;
            }

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
            switch(gameState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(background, backgroundPos, Color.White);
                    spriteBatch.Draw(startButton, startButtPos, Color.White);
                    spriteBatch.Draw(optionButton, optionButtPos, Color.White);
                    spriteBatch.Draw(charaSelButt, charaSelButtPos, Color.White);
                    spriteBatch.Draw(exitButton, exitButtPos, Color.White);
                    break;
                case GameState.CharacterSelection:
                    spriteBatch.Draw(kate, new Vector2(50, 100), Color.White);
                    spriteBatch.Draw(norman, new Vector2(484, 100), Color.White);
                    break;
            }
            /*
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
                /*spriteBatch.Draw(background, backgroundPos, Color.White);
                spriteBatch.Draw(normanSprite, new Vector2(350, 200), Color.White);
                spriteBatch.Draw(normanSprite, new Vector2(400, 200), Color.White);
            }

            if (gameState == GameState.Options)
            {
                // Level editor
                // Music volume
                // Sfx volume
            }

            if (gameState == GameState.Exit)
            {
                gameState = GameState.CharacterSelection;
            }

            // Draw the game while playing
            if (gameState == GameState.Playing)
            {
                // Draw character, animated background, etc. in this statement
            }*/
        

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadGame()
        {
            // load the animated bg of the game
            // gameBackground = Content.Load<Texture2D>("tunnels");
            // Thread.Sleep(3000);
            // gameState = GameState.Playing;
        }

        // Handle click events
        public void MouseClick(int x, int y)
        {
            Rectangle mouseClick = new Rectangle(x, y, 10, 10);

            if (gameState == GameState.Menu)
            {
                Rectangle startButtonRect = new Rectangle((int)startButtPos.X, (int)startButtPos.Y, BUTT_W, BUTT_H);
                Rectangle exitButtonRect = new Rectangle((int)exitButtPos.X, (int)exitButtPos.Y, BUTT_W, BUTT_H);
                Rectangle optionButtonRect = new Rectangle((int)optionButtPos.X, (int)optionButtPos.Y, BUTT_W, BUTT_H);
                Rectangle charaSelButtRect = new Rectangle((int)charaSelButtPos.X, (int)charaSelButtPos.Y, BUTT_W, BUTT_H);
                if (mouseClick.Intersects(startButtonRect))
                {
                    gameState = GameState.Start;

                }
                if (mouseClick.Intersects(optionButtonRect))
                {
                    gameState = GameState.Options;
                }

                if (mouseClick.Intersects(exitButtonRect))
                {
                    Exit();
                }
                if (mouseClick.Intersects(charaSelButtRect))
                {
                    gameState = GameState.CharacterSelection;
                }
            }
            if (gameState == GameState.CharacterSelection)
            {
                    // Selecting which character to use
                    Rectangle normanRect = new Rectangle((int)normanPos.X, (int)normanPos.Y, PLAYER_W, PLAYER_H);
                    Rectangle kateRect = new Rectangle((int)katePos.X, (int)katePos.Y, PLAYER_W, PLAYER_H);
                    if (mouseClick.Intersects(normanRect))
                    {
                        gameState = GameState.Playing;
                        character.CharacterSprite = normanSprite;
                    }
                    if (mouseClick.Intersects(kateRect))
                    {
                        gameState = GameState.Playing;
                        character.CharacterSprite = kateSprite;
                    }
                
            }
        }
    }
}

