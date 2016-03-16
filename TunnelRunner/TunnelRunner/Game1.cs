﻿using Microsoft.Xna.Framework;
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
        Texture2D normanSprite;
        Texture2D kateSprite;
        Texture2D loadingScreen;

        // Tunnel walls
        Scrolling tunnelWall1;
        Scrolling tunnelWall2;
        
        const int PLAYER_W = 108;
        const int PLAYER_H = 150;

        Vector2 backgroundPos;
        Vector2 normanPos;
        Vector2 katePos;

        // GameState button textures
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D charaSelButt;
        Texture2D optionButton;     // Start, Exit, and Option appear on the starting screen. Option brings you to external tool
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

        KeyboardState kbState;
        KeyboardState previousKbState;

        //animation stuff
        int frame;
        int numFrames = 4;
        int frameElapsed;
        double timePerFrame = 100;

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
            //there is 25 pix gap in between each button
            startButtPos = new Vector2(12,250);
            charaSelButtPos = new Vector2(187, 250);
            optionButtPos = new Vector2( 362, 250);
            exitButtPos = new Vector2( 537, 250);
            character = new Character();
            character.CharacterSprite = kateSprite;
            character.Position = new Rectangle(10, 250, PLAYER_W, PLAYER_H);
            gameState = GameState.Menu;

            msState = Mouse.GetState();
            previousMsState = msState;

            kbState = Keyboard.GetState();
            previousKbState = kbState;
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

            /*

            loadingScreen = Content.Load<Texture2D>("loading");*/

            tunnelWall1 = new Scrolling(Content.Load<Texture2D>("Tunnel Walls/Wall1"), new Rectangle(0, 0, 700, 400));
            tunnelWall2 = new Scrolling(Content.Load<Texture2D>("Tunnel Walls/Wall2"), new Rectangle(700, 0, 700, 400));

            startButton = Content.Load<Texture2D>("Buttons/Start");
            optionButton = Content.Load<Texture2D>("Buttons/Options");
            exitButton = Content.Load<Texture2D>("Buttons/Exit");
            charaSelButt = Content.Load<Texture2D>("Buttons/Selection");
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
            frameElapsed = (int)(gameTime.TotalGameTime.TotalMilliseconds / timePerFrame);
            frame = frameElapsed % numFrames + 1;

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
                case GameState.Playing:

                    // Scrolling backgrounds
                    if (tunnelWall1.rectangle.X + tunnelWall1.tunnel.Width <= 0)
                    {
                        tunnelWall1.rectangle.X = tunnelWall2.rectangle.X + tunnelWall2.tunnel.Width;
                    }
                    if (tunnelWall2.rectangle.X + tunnelWall2.tunnel.Width <= 0)
                    {
                        tunnelWall2.rectangle.X = tunnelWall1.rectangle.X + tunnelWall1.tunnel.Width;
                    }
                    tunnelWall1.Update();
                    tunnelWall2.Update();

                    kbState = Keyboard.GetState();
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        character.Position = new Rectangle(character.Position.X, character.Position.Y - 3, PLAYER_W, PLAYER_H);
                    }
                    if (kbState.IsKeyDown(Keys.Down))
                    {
                        character.Position = new Rectangle(character.Position.X, character.Position.Y + 3, PLAYER_W, PLAYER_H);
                    }
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
                case GameState.Playing:

                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);
                    
                    spriteBatch.Draw(kateSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H),Color.White);
                    
                    break;
            }
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
                    gameState = GameState.Playing;

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
                    //selecting which character to use
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

