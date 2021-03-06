﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
namespace TunnelRunner
{
    enum GameState {CharacterSelection, Options, Credit, Playing, Pause, Menu, Prologue, Exit,GameOver,Resume }//game over state added
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        //Song song;

        // Game assets
        // Texture2D
        Character character;
        //Texture2D background;   // Background is referring to the menu background. Game background will be animated
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
        Texture2D milk;
        Texture2D id;
        Texture2D snow;

        // Screens
        Texture2D gameOver;
        Texture2D prologue;
        Texture2D creditScreen;
        Texture2D optionScreen;

        // Tunnel walls
        Scrolling tunnelWall1;
        Scrolling tunnelWall2;
        
        // Const ints
        const int PLAYER_W = 108;
        const int PLAYER_H = 150;
        const int BUTT_W = 106;
        const int BUTT_H = 33;

        // Vector2
        Vector2 backgroundPos;
        Vector2 normanPos;
        Vector2 katePos;
        Vector2 titlePos;
        Vector2 snowPos;

        // Texture2D GameState button textures
        Texture2D background;
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D charaSelButt;
        Texture2D optionButton;     // Start, Exit, and Option appear on the starting screen. Option brings you to external tool
        Texture2D menuButton;
        Texture2D normanButton;
        Texture2D kateButton;
        Texture2D ground;
        Texture2D menu;
        Texture2D selScreen;
        // Vector2 GameState button positions
        Vector2 startButtPos;
        Vector2 exitButtPos;
        Vector2 optionButtPos;
        Vector2 menuPos;
        Vector2 characterPos;
        Vector2 charaSelButtPos;

        GameState gameState;

        // Mouse state
        MouseState msState;
        MouseState previousMsState;

        // Keyboard state
        KeyboardState kbState;
        KeyboardState previousKbState;

        List<Collectibles> collectibleList;
        List<Collectibles> idList;
        List<Obstacles> chairList;

        Obstacles chairOb;
        Collectibles idOb;
        Collectibles collectOb;

        //score
        int score;
        double timer;
        int frequency = 700;

        // Animation stuff
        int frame;
        int numFrames = 5;
        int frameElapsed;
        double timePerFrame = 100;

        //level class
        Levels currLvl = new Levels();
        Random rgn = new Random();

        int testing = 1;
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
            normanPos = new Vector2(400, 120);
            katePos = new Vector2(220, 120);

            // There is 25px gap in between each button
            titlePos = new Vector2(400, 220);
            startButtPos = new Vector2(50, 200);
            charaSelButtPos = new Vector2(50, 250);
            optionButtPos = new Vector2( 50, 300);
            exitButtPos = new Vector2(50, 350);
            character = new Character();

            character.Position = new Rectangle(10, 250, PLAYER_W, PLAYER_H);
            gameState = GameState.Prologue;
            msState = Mouse.GetState();
            previousMsState = msState;

            chairList = new List<Obstacles>();
            idList = new List<Collectibles>();
            collectibleList = new List<Collectibles>();


            kbState = Keyboard.GetState();
            previousKbState = kbState;

            character.Level = 0;
            score = 0;

            currLvl.Populate();
            currLvl.LoadLevels();
            currLvl.IdImg = id;
            currLvl.ObsImg = chair;
            currLvl.MilkImg = milk;

            snowPos = new Vector2(-900, 0);

            base.Initialize();
        }

        public void NextLevel()
        {
            chairList.Clear();
            idList.Clear();
            collectibleList.Clear();

            character.Level++;

            currLvl.GetLvl(character.Level);

            if (character.Level > 0)
            
            for (int l = 1; l <= currLvl.LvlList[character.Level-1].Obst; l++)
            {
                chairOb = new Obstacles(rgn.Next((l - 1) * (frequency - character.Level * 2), l * (frequency - character.Level * 2)), rgn.Next(10, 300), 70, 90, true, chair);
                chairOb.CollectibleImage = chair;
               chairList.Add(chairOb);
            }
            for (int l = 1; l <= currLvl.LvlList[character.Level - 1].IntenseMilk; l++)
            {
                collectOb = new Collectibles(rgn.Next((l - 1) * (frequency - character.Level * 2), l * (frequency - character.Level * 2)), rgn.Next(10, 300), 36, 70, true, milk);
                collectOb.CollectibleImage = milk;
                collectibleList.Add(collectOb);
            }
            for (int l = 1; l <= currLvl.LvlList[character.Level - 1].Ids; l++)
            {
                idOb = new Collectibles(rgn.Next((l - 1) * (frequency - character.Level * 2), l * (frequency - character.Level * 2)), rgn.Next(10, 300), 52, 70, true, id);
                idOb.CollectibleImage = id;
                idList.Add(idOb);
            }
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //song = Content.Load<Song>("Song");

            /*

            loadingScreen = Content.Load<Texture2D>("loading");*/

            tunnelWall1 = new Scrolling(Content.Load<Texture2D>("Tunnel Walls/Wall1"), new Rectangle(0, 0, 700, 400), 5);
            tunnelWall2 = new Scrolling(Content.Load<Texture2D>("Tunnel Walls/Wall2"), new Rectangle(700, 0, 700, 400), 5);

            startButton = Content.Load<Texture2D>("Buttons/Start");
            optionButton = Content.Load<Texture2D>("Buttons/Options");
            exitButton = Content.Load<Texture2D>("Buttons/Exit");
            charaSelButt = Content.Load<Texture2D>("Buttons/Selection");
            menuButton = Content.Load<Texture2D>("Buttons/Menu");
            background = Content.Load<Texture2D>("Buttons/bg");
            optionScreen = Content.Load<Texture2D>("optionScreen");
            kateSprite = Content.Load<Texture2D>("kateSprite");
            kate = Content.Load<Texture2D>("kate");
            norman = Content.Load<Texture2D>("norman");
            //title = Content.Load<Texture2D>("Buttons/title");
            ground = Content.Load<Texture2D>("ground");
            healthBarThree = Content.Load<Texture2D>("3Life");
            healthBarTwo = Content.Load<Texture2D>("2Life");
            healthBarOne = Content.Load<Texture2D>("1Life");
            chair = Content.Load<Texture2D>("Obstacles/chair");
            milk = Content.Load<Texture2D>("Collectibles/INTENSE Milk");
            id = Content.Load<Texture2D>("Collectibles/ID Card");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            normanSprite = Content.Load<Texture2D>("normanSprite");

            snow = Content.Load<Texture2D>("snow");
            gameOver = Content.Load<Texture2D>("gameOver");

            prologue = Content.Load<Texture2D>("prologueScreen");
            creditScreen = Content.Load<Texture2D>("creditScreen");
            menu = Content.Load<Texture2D>("menuScreen");
            selScreen = Content.Load<Texture2D>("selectionScreen");

            character.CharacterSprite = Content.Load<Texture2D>("kateSprite");//default sprite
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
            frameElapsed = (int)(gameTime.TotalGameTime.TotalMilliseconds / (timePerFrame)); //alter the # in front of "timePerFrame" to change the speed of the animation
            frame = frameElapsed % numFrames + 1;

            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            switch (gameState)
            {
                case GameState.Prologue:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    break;
                case GameState.Credit:
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
                    break;
                case GameState.Menu:
                    character.Level = 0;
                    msState = Mouse.GetState();
                    if (previousMsState.LeftButton == ButtonState.Pressed && msState.LeftButton == ButtonState.Released)
                    {
                        MouseClick(msState.X, msState.Y);
                    }
                    previousMsState = msState;
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
                    if (character.Health > 0)//game is only running when player is alive
                    {
                        //MediaPlayer.Play(song);
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
                        for (int i = 0; i < collectibleList.Count; i++) //making collectibles
                        {
                            collectibleList[i].Speed = tunnelWall1.movingSpeed;
                            collectibleList[i].Moving();
                        }
                        for (int i = 0; i < idList.Count; i++) //making ids move
                        {
                            idList[i].Speed = tunnelWall1.movingSpeed;
                            idList[i].Moving();
                        }
                        // Check for collisions between character and chairList
                        foreach (Obstacles obstacle in chairList)
                        {
                            if (character.Health >= 1)//only check for collision if player still have any lives
                            {
                                if (obstacle.CheckCollision(character))
                                {
                                    character.Health--;
                                    if (score >= 50) //only removes 50 if there are 50 points to remove
                                    {
                                        score = score - 50;
                                    }
                                    else //otherwise sets score to 0. prevents score from going negative
                                    {
                                        score = 0;
                                    }
                                }
                            }
                        }
                        //milk adds health
                        foreach (Collectibles collectible in collectibleList)
                        {
                            if (character.Health >= 1 && character.Health < 3) //only check if the character has 1 or 2 hearts
                            {
                                if (collectible.CheckCollision(character))
                                {
                                    character.Health++;
                                }
                            }
                        }
                        //id adds 50 to score
                        foreach (Collectibles ids in idList)
                        {
                            if (ids.CheckCollision(character))
                            {
                                score = score + 50;
                            }
                        }

                        //click 'Enter' to pause the game
                        if (SingleKeyPress(Keys.Enter))
                        {
                            gameState = GameState.Pause;
                        }
                        if (timer >= 2000) //if 1 second has passed
                        {
                            score += 2;
                            timer -= 1000;
                        }
                        if(chairList[chairList.Count-1].Active==false)
                        { NextLevel(); }
                    }
                    else
                    {
                        if(snowPos.X<0)
                        {
                            snowPos.X += 5;
                        }
                        else
                        { gameState = GameState.GameOver; }
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
                    if (timer >= 2000) //if 1 second has passed
                    {
                        timer -= 2000;
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
            switch (gameState)
            {
                case GameState.Prologue:
                    spriteBatch.Draw(prologue, new Rectangle(0, 0, 700, 400), Color.White);
                    break;
                case GameState.Credit:
                    spriteBatch.Draw(creditScreen, new Rectangle(0, 0, 700, 400), Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(620.0f, 350.0f), Color.White);
                    break;
                case GameState.Menu:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, 700, 400), Color.White);               
                    break;
                case GameState.CharacterSelection:
                    spriteBatch.Draw(selScreen, new Rectangle(0, 0, 700, 400), Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(620.0f, 350.0f), Color.White);
                    break;
                case GameState.Playing:
                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);
                    spriteBatch.Draw(character.CharacterSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H), Color.White);
                    spriteBatch.DrawString(spriteFont, "Level: " + character.Level, new Vector2(250.0f, 0.0f), Color.White);
                    spriteBatch.Draw(ground, new Rectangle(0, 380, 700, 20), Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(630.0f, 0.0f), Color.White);
                    spriteBatch.DrawString(spriteFont, "Score: " + score, new Vector2(350.0f, 0.0f), Color.White); 
                    for (int i = 0; i <chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < collectibleList.Count; i++)
                    {
                        collectibleList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i <idList.Count; i++)
                    {
                        idList[i].Draw(spriteBatch);
                    }
                    switch (character.Health)
                    {
                        case 3:
                            spriteBatch.Draw(healthBarThree, new Rectangle(5, 1, 90, 22), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(healthBarTwo, new Rectangle(5, 1, 90, 22), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(healthBarOne, new Rectangle(5, 1, 90, 22), Color.White);
                            break;
                    }
                    spriteBatch.Draw(snow, new Rectangle((int)snowPos.X, (int)snowPos.Y, 900, 400), Color.White);
                    break;
                case GameState.Options:
                    spriteBatch.Draw(optionScreen, backgroundPos, Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(620.0f, 0.0f), Color.White);
                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(gameOver, new Rectangle(0,0,900,400), Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(620.0f, 0.0f), Color.White);
                    break;
                case GameState.Pause:
                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);

                    spriteBatch.Draw(character.CharacterSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H), Color.White);
                    spriteBatch.DrawString(spriteFont, "Level: " + character.Level, new Vector2(250.0f, 0.0f), Color.White);
                    spriteBatch.Draw(ground, new Rectangle(0, 380, 700, 20), Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(620.0f, 0.0f), Color.White);
                    spriteBatch.DrawString(spriteFont, "Score: " + score, new Vector2(50, 50), Color.White);
                    

                    for (int i = 0; i < chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < collectibleList.Count; i++) //makes collectibles display while game is paused
                    {
                        collectibleList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i <idList.Count; i++) //makes ids display while game is paused
                    {
                        idList[i].Draw(spriteBatch);
                    }

                    switch (character.Health)
                    {
                        case 3:
                            spriteBatch.Draw(healthBarThree, new Rectangle(5, 3, 39, 10), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(healthBarTwo, new Rectangle(5, 3, 39, 10), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(healthBarOne, new Rectangle(5, 3, 39, 10), Color.White);
                            break;
                    }
                    break;
                case GameState.Resume:
                    tunnelWall1.Draw(spriteBatch);
                    tunnelWall2.Draw(spriteBatch);
                    //need a switch statement later, so that the sprite sheet can change
                    spriteBatch.Draw(character.CharacterSprite, character.Position, new Rectangle(frame * PLAYER_W, 0, PLAYER_W, PLAYER_H), Color.White);
                    spriteBatch.DrawString(spriteFont, "Level: " + character.Level, new Vector2(250.0f, 0.0f), Color.White);
                    spriteBatch.Draw(ground, new Rectangle(0, 380, 700, 20), Color.White);
                    spriteBatch.DrawString(spriteFont, "Menu", new Vector2(620.0f, 0.0f), Color.White);

                    for (int i = 0; i < chairList.Count; i++)
                    {
                        chairList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < collectibleList.Count; i++) 
                    {
                        collectibleList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < idList.Count; i++) 
                    {
                        idList[i].Draw(spriteBatch);
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
            bool sel = false;//for testing purpose
            Rectangle mouseClick = new Rectangle(x, y, 10, 10);
            Rectangle menuButtRect = new Rectangle(620, 1, 60, 20);
            Rectangle menuButtRect1 = new Rectangle(620, 350, 60, 20);
            

            if (gameState == GameState.Prologue)
            {
                Rectangle nextButtonRect = new Rectangle(555, 360, 84, 30);
                if (mouseClick.Intersects(nextButtonRect))
                {
                    gameState = GameState.Menu;
                }
            }
            if (gameState == GameState.Menu)
            {
                Rectangle startButtonRect = new Rectangle((int)startButtPos.X, (int)startButtPos.Y, 120, 43);
                Rectangle exitButtonRect = new Rectangle((int)exitButtPos.X, (int)exitButtPos.Y, 90, 35);
                Rectangle optionButtonRect = new Rectangle((int)optionButtPos.X, (int)optionButtPos.Y, 158, 41);
                Rectangle charaSelButtRect = new Rectangle((int)charaSelButtPos.X, (int)charaSelButtPos.Y, 190, 50);

                if (mouseClick.Intersects(startButtonRect))
                {
                    gameState = GameState.Playing;
                    NextLevel();
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
                    sel = true;
                }
            }
            if (gameState == GameState.CharacterSelection)
            {
                //selecting which character to use
                Rectangle normanRect = new Rectangle((int)normanPos.X, (int)normanPos.Y, 116, 178);
                Rectangle kateRect = new Rectangle((int)katePos.X, (int)katePos.Y, 116, 138);
                if (mouseClick.Intersects(normanRect))
                {
                    character.CharacterSprite = normanSprite;
                    ResetGame();
                    gameState = GameState.Playing;
                }
                if (mouseClick.Intersects(kateRect))
                {
                    character.CharacterSprite = kateSprite;
                    ResetGame();
                    gameState = GameState.Playing;
                }
                if (mouseClick.Intersects(menuButtRect1))
                {
                    gameState = GameState.Menu;
                }
            }
            if(gameState==GameState.Playing)
            {
                if(mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                    ResetGame();
                }
            }
            if (gameState == GameState.Options)
            {
                Rectangle creditButt = new Rectangle(317, 178, 55, 20 );
                Rectangle prologueButt = new Rectangle(314, 200, 63, 21);
                if (mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                    ResetGame();
                }
                if (mouseClick.Intersects(creditButt))
                {
                    gameState = GameState.Credit;
                }
                if (mouseClick.Intersects(prologueButt))
                {
                    gameState = GameState.Prologue;
                }
            }
            if(gameState == GameState.Credit)
            {
                if (mouseClick.Intersects(menuButtRect1))
                {
                    gameState = GameState.Menu;
                }
            }
            if (gameState == GameState.GameOver)
            {
                if (mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                    ResetGame();
                }
            }
            if(gameState==GameState.Pause)
            {
                if (mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                    ResetGame();
                }
            }
            if(gameState==GameState.Resume)
            {
                if (mouseClick.Intersects(menuButtRect))
                {
                    gameState = GameState.Menu;
                    ResetGame();
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
            idList.Clear();
            collectibleList.Clear();
            character.Health = 3;
            character.Level = 0;
            score = 0;
            snowPos.X = -900;
            NextLevel();
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

