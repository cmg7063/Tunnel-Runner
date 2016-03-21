using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TunnelRunner
{
    enum GameState {CharacterSelection, Options, Playing, Pause, Menu, Exit,GameOver,Resume }//game over state added
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        // Game assets
        Character character;
        Texture2D background;   // Background is referring to the menu background. Game background will be animated
        Texture2D kate;
        Texture2D norman;
        Texture2D normanSprite;
        Texture2D kateSprite;
        Texture2D loadingScreen;
        Texture2D title;
        Texture2D healthBarThree;
        Texture2D healthBarTwo;
        Texture2D healthBarOne;
        Texture2D chair; 

        // Tunnel walls
        Scrolling tunnelWall1;
        Scrolling tunnelWall2;
        
        const int PLAYER_W = 108;
        const int PLAYER_H = 150;

        Vector2 backgroundPos;
        Vector2 normanPos;
        Vector2 katePos;
        Vector2 titlePos;

        // GameState button textures
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D charaSelButt;
        Texture2D optionButton;     // Start, Exit, and Option appear on the starting screen. Option brings you to external tool
        Texture2D menuButton;
        Texture2D normanButton;
        Texture2D kateButton;
        Texture2D ground;

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

        List<Obstacles> chairList;
        Obstacles chairOb;

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
            titlePos = new Vector2(150, 0);
            startButtPos = new Vector2(12,300);
            charaSelButtPos = new Vector2(187, 300);
            optionButtPos = new Vector2( 362, 300);
            exitButtPos = new Vector2( 537, 300);
            character = new Character();
            character.CharacterSprite = kateSprite;
            character.Position = new Rectangle(10, 250, PLAYER_W, PLAYER_H);
            gameState = GameState.Menu;
            msState = Mouse.GetState();
            previousMsState = msState;

            chairList = new List<Obstacles>();

            kbState = Keyboard.GetState();
            previousKbState = kbState;

            

            base.Initialize();
        }

        public void NextLevel()
        {
            chairList.Clear();
            character.Level++;
            Random rng = new Random();
            for(int i = 0; i < character.Level * 3; i++)
            {
                chairOb = new Obstacles(rng.Next(300, 1200), rng.Next(10, 200), 73, 100, true);//changed the start position for the chair so i can actually see if it is moving correctly
                chairOb.CollectibleImage = chair;
                chairList.Add(chairOb);
            }
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            /*

            loadingScreen = Content.Load<Texture2D>("loading");*/

            tunnelWall1 = new Scrolling(Content.Load<Texture2D>("Tunnel Walls/Wall1"), new Rectangle(0, 0, 700, 400), 5);
            tunnelWall2 = new Scrolling(Content.Load<Texture2D>("Tunnel Walls/Wall2"), new Rectangle(700, 0, 700, 400), 5);

            startButton = Content.Load<Texture2D>("Buttons/Start");
            optionButton = Content.Load<Texture2D>("Buttons/Options");
            exitButton = Content.Load<Texture2D>("Buttons/Exit");
            charaSelButt = Content.Load<Texture2D>("Buttons/Selection");
            menuButton = Content.Load<Texture2D>("Buttons/Menu");
            background = Content.Load<Texture2D>("background");
            kateSprite = Content.Load<Texture2D>("kateSprite");
            kate = Content.Load<Texture2D>("kate");
            norman = Content.Load<Texture2D>("norman");
            title = Content.Load<Texture2D>("title");
            ground = Content.Load<Texture2D>("ground");
            healthBarThree = Content.Load<Texture2D>("3Life");
            healthBarTwo = Content.Load<Texture2D>("2Life");
            healthBarOne = Content.Load<Texture2D>("1Life");
            chair = Content.Load<Texture2D>("Obstacles/chair");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            frameElapsed = (int)(gameTime.TotalGameTime.TotalMilliseconds / (0.7*timePerFrame)); //alter the # in front of "timePerFrame" to change the speed of the animation
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
                        NextLevel();
                    }
                    break;
                case GameState.CharacterSelection:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                        gameState = GameState.Playing;
                    }
                    previousMsState = msState;
                    break;
                case GameState.Playing:
                    if (character.Health > 0)//game is only running when player is alive
                    {
                        msState = Mouse.GetState();
                        if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                        {
                            MouseClick(msState.X, msState.Y);
                        }
                        previousMsState = msState;
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
                        KeepOnScreen(character);
                        //character.Level = 1;
                        //moveing all collctibles on the screen with the same speed as the background
                        for (int i = 0; i < chairList.Count; i++)
                        {
                            chairList[i].Speed = tunnelWall1.movingSpeed;
                            chairList[i].Moving();

                        }
                        // Check for collisions between character and chairList
                        foreach (Obstacles obstacle in chairList)
                        {
                            if (character.Health >= 1)//only check for collision if player still have any lives
                            {
                                if (obstacle.CheckCollision(character))
                                {
                                    character.Health--;
                                }
                            }
                        }
                        //click 'Enter' to pause the game
                        if(SingleKeyPress(Keys.Enter))
                        {
                            gameState = GameState.Pause;
                        }
                    }
                    else
                    {
                        gameState = GameState.GameOver;
                    }
                    break;
                case GameState.Options:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    break;
                case GameState.GameOver:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    ResetGame();
                    break;
                case GameState.Pause:
                    frame = 0;
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    previousKbState = kbState;
                    kbState = Keyboard.GetState();
                    if(SingleKeyPress(Keys.Space))  // Press 'Space' to resume
                    {
                        Pause(tunnelWall1);
                        Pause(tunnelWall2);
                        gameState = GameState.Resume;
                    }
                    break;

                case GameState.Resume:
                    timePerFrame = 100;
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    kbState = Keyboard.GetState();
                    if(SingleKeyPress(Keys.Space))
                    {
                        gameState = GameState.Playing;
                        Resume(tunnelWall1);
                        Resume(tunnelWall2);
                        msState = Mouse.GetState();                        
                    }
                    previousMsState = msState;
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

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
                    spriteBatch.Draw(title, titlePos, Color.White);
                    break;
                case GameState.CharacterSelection:
                    spriteBatch.Draw(kate, new Vector2(50, 100), Color.White);
                    spriteBatch.Draw(norman, new Vector2(484, 100), Color.White);
                    break;
                case GameState.Playing:

                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);
                    //need a switch statement later, so that the sprite sheet can change
                    spriteBatch.Draw(kateSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H),Color.White);
                    spriteBatch.DrawString(spriteFont, "Level: " + character.Level, new Vector2(250.0f, 0.0f), Color.White);
                    spriteBatch.Draw(ground, new Rectangle(0, 380, 700, 20), Color.White);
                    spriteBatch.Draw(menuButton, new Rectangle(630, 1, 60, 20), Color.White);
                    for (int i = 0; i < chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    switch(character.Health)
                    {
                        case 3:
                            spriteBatch.Draw(healthBarThree, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(healthBarTwo, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(healthBarOne, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                    }
                    break;
                case GameState.Options:
                    spriteBatch.Draw(menuButton, new Rectangle(630, 1, 60, 20), Color.White);
                    break;
                case GameState.GameOver:
                    spriteBatch.DrawString(spriteFont, "GAME OVER", new Vector2(300, 150), Color.Red);
                    spriteBatch.Draw(menuButton, new Rectangle(630, 1, 60, 20), Color.White);
                    break;
                case GameState.Pause:
                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);

                    spriteBatch.Draw(kateSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H), Color.White);
                    spriteBatch.DrawString(spriteFont, "Level: " + character.Level, new Vector2(250.0f, 0.0f), Color.White);
                    spriteBatch.Draw(ground, new Rectangle(0, 380, 700, 20), Color.White);
                    spriteBatch.Draw(menuButton, new Rectangle(630, 1, 60, 20), Color.White);
                    for (int i = 0; i < chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    switch (character.Health)
                    {
                        case 3:
                            spriteBatch.Draw(healthBarThree, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(healthBarTwo, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(healthBarOne, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                    }
                    for (int i = 0; i < chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    break;
                case GameState.Resume:
                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);
                    //need a switch statement later, so that the sprite sheet can change
                    spriteBatch.Draw(kateSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H), Color.White);
                    spriteBatch.DrawString(spriteFont, "Level: " + character.Level, new Vector2(250.0f, 0.0f), Color.White);
                    spriteBatch.Draw(ground, new Rectangle(0, 380, 700, 20), Color.White);
                    spriteBatch.Draw(menuButton, new Rectangle(630, 1, 60, 20), Color.White);
                    for (int i = 0; i < chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    switch (character.Health)
                    {
                        case 3:
                            spriteBatch.Draw(healthBarThree, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(healthBarTwo, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(healthBarOne, new Rectangle(5, 1, 100, 20), Color.White);
                            break;
                    }
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Handle click events
        public void MouseClick(int x, int y)
        {
            Rectangle mouseClick = new Rectangle(x, y, 10, 10);
            Rectangle menuButtRect = new Rectangle(630, 1, 60, 20);
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
                    character.CharacterSprite = normanSprite;
                }
                if (mouseClick.Intersects(kateRect))
                {
                    character.CharacterSprite = kateSprite;
                }
            }
            if(gameState==GameState.Playing)
            {
                if(mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                }
            }
            if (gameState == GameState.Options)
            {
                if (mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                }
            }
            if (gameState == GameState.GameOver)
            {
                if (mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                }
            }
        }

        private void KeepOnScreen(Character chara)
        {
            if(character.Position.Y <= 25)
            {
                character.Position = new Rectangle(character.Position.X, 25, PLAYER_W, PLAYER_H);
            }
            if(character.Position.Y >= 230)
            {
                character.Position = new Rectangle(character.Position.X, 230, PLAYER_W, PLAYER_H);
            }
        }
        private void ResetGame()
        {
            chairList.Clear();
            character.Health = 3;
            character.Level = 0;
        }
        
        //overloading Pause method
        private void Pause(Scrolling sub)
        {
            sub.movingSpeed = 0;
            sub.rectangle.X = sub.preXpos;
        }
        private void Pause(Collectibles coll)//including obstacles
        {
            coll.Speed = 0;
            coll.PreXpos = coll.Position.X;
        }

        private void Resume(Scrolling sub)
        {
            sub.movingSpeed = sub.preSpeed;
        }
        private void Resume(Collectibles coll)
        {
            coll.Speed = coll.PreSpeed;
        }

        public bool SingleKeyPress(Keys key)
        {
            if (kbState.IsKeyDown(key) && !previousKbState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

