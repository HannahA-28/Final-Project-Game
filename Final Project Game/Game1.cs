using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project_Game
{
    enum Screen
    {
        Intro,
        Game,
        Dead,
        EndScreen
    }

    public class Game1 : Game
    {

        Screen screen;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D startTexture;
        Rectangle startRect;

        Texture2D exitTexture;
        Rectangle exitRect;

        Texture2D retryTexture;
        Rectangle retryRect;

        Texture2D rectangleTexture;
        Rectangle rectangleRect;

        Rectangle exitClikerRect;

        Rectangle retryClickerRect;

        Texture2D introTexture;
        Texture2D gameTexture;
        Texture2D deadTexture;
        Texture2D endScreenTexture;

        Texture2D birdTexture;
        Rectangle birdRect;
        Vector2 birdSpeed;

        Texture2D pipe1UpTexture;
        Rectangle pipe1UpRect;
        Rectangle pipe2UpRect;
        Rectangle pipe3UpRect;
        Rectangle pipe4UpRect;

        Texture2D pipe1DownTexture;
        Rectangle pipe1DownRect;
        Rectangle pipe2DownRect;
        Rectangle pipe3DownRect;
        Rectangle pipe4DownRect;

        Vector2 pipeSpeed;

        int pipeCounter;

        MouseState mouseState;

        KeyboardState keyboardState;

        SpriteFont titleFont;

        SoundEffect backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            startRect = new Rectangle(500, 350, 175, 175);
            exitRect = new Rectangle(50, 60, 400, 450);
            retryRect = new Rectangle(375, 100, 375, 375);
            rectangleRect = new Rectangle(523, 393, 132, 81);
            exitClikerRect = new Rectangle(117, 230, 260, 110);
            retryClickerRect = new Rectangle(426, 230, 270, 115);
            birdRect = new Rectangle(5, 85, 35, 35);
            pipe1UpRect = new Rectangle(75, 150, 65, 500);
            pipe1DownRect = new Rectangle(75, -40, 65, 100);
            pipe2UpRect = new Rectangle(300, 250, 65, 400);
            pipe2DownRect = new Rectangle(300, 0, 65, 125);
            pipe3UpRect = new Rectangle(500, 350, 65, 400);
            pipe3DownRect = new Rectangle(500, 0, 65, 225);
            pipe4UpRect = new Rectangle(700, 400, 65, 550);
            pipe4DownRect = new Rectangle(700, 0, 65, 300);
            pipeCounter = 0;

            pipeSpeed = new Vector2(-1, 0);

            screen = Screen.Intro;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            introTexture = Content.Load<Texture2D>("Intro1");
            exitTexture = Content.Load<Texture2D>("exit");
            retryTexture = Content.Load<Texture2D>("tryAgain");
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            gameTexture = Content.Load<Texture2D>("Game");
            startTexture = Content.Load<Texture2D>("Start");
            titleFont = Content.Load<SpriteFont>("Titlefont");
            birdTexture = Content.Load<Texture2D>("flappyBird");
            deadTexture = Content.Load<Texture2D>("black");
            pipe1UpTexture = Content.Load<Texture2D>("upPipe");
            pipe1DownTexture = Content.Load<Texture2D>("downPipe");
            endScreenTexture = Content.Load<Texture2D>("youWin");
            backgroundMusic = Content.Load<SoundEffect>("music");
        }

        protected override void Update(GameTime gameTime)
        {
            this.Window.Title = $"({mouseState.X}, {mouseState.Y})";
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            birdSpeed = new Vector2();
            keyboardState = Keyboard.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (rectangleRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Game;
                        backgroundMusic.Play();
                    }
                }
            }
            else if (screen == Screen.Game)
            {
                birdSpeed = Vector2.Zero;
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    birdSpeed.Y -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    birdSpeed.Y += 2;
                }
                birdRect.Y += (int)birdSpeed.Y;

                pipe1UpRect.X += (int)pipeSpeed.X;
                if (pipe1UpRect.Right <= 0)
                {
                    pipe1UpRect.X = 800;
                    pipe1DownRect.X = 800;
                    pipeCounter += 1;
                }
                pipe1DownRect.X += (int)pipeSpeed.X;

                pipe2UpRect.X += (int)pipeSpeed.X;
                if (pipe2UpRect.Right <= 0)
                {
                    pipe2UpRect.X = 800;
                    pipe2DownRect.X = 800;
                    pipeCounter += 1;
                }
                pipe2DownRect.X += (int)pipeSpeed.X;

                pipe3UpRect.X += (int)pipeSpeed.X;
                if (pipe3UpRect.Right <= 0)
                {
                    pipe3UpRect.X = 800;
                    pipe3DownRect.X = 800;
                    pipeCounter += 1;
                }
                pipe3DownRect.X += (int)pipeSpeed.X;

                pipe4UpRect.X += (int)pipeSpeed.X;
                if (pipe4UpRect.Right <= 0)
                {
                    pipe4UpRect.X = 800;
                    pipe4DownRect.X = 800;
                    pipeCounter += 1;
                }
                pipe4DownRect.X += (int)pipeSpeed.X;

                if (birdRect.Intersects(pipe1UpRect))
                {
                    screen = Screen.Dead;
                }
                if (birdRect.Intersects(pipe1DownRect))
                {
                    screen = Screen.Dead;
                }

                if (birdRect.Intersects(pipe2UpRect))
                {
                    screen = Screen.Dead;
                }
                if (birdRect.Intersects(pipe2DownRect))
                {
                    screen = Screen.Dead;
                }

                if (birdRect.Intersects(pipe3UpRect))
                {
                    screen = Screen.Dead;
                }
                if (birdRect.Intersects(pipe3DownRect))
                {
                    screen = Screen.Dead;
                }

                if (birdRect.Intersects(pipe4UpRect))
                {
                    screen = Screen.Dead;
                }
                if (birdRect.Intersects(pipe4DownRect))
                {
                    screen = Screen.Dead;
                }
                if (pipeCounter == 20)
                {
                    screen = Screen.EndScreen;
                }
            }
            else if (screen == Screen.Dead)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (exitClikerRect.Contains(mouseState.Position))
                        Exit();
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (retryClickerRect.Contains(mouseState.Position))
                    {
                        startRect = new Rectangle(500, 350, 175, 175);
                        rectangleRect = new Rectangle(523, 393, 132, 81);
                        birdRect = new Rectangle(5, 60, 35, 35);
                        pipe1UpRect = new Rectangle(75, 150, 65, 500);
                        pipe1DownRect = new Rectangle(75, -40, 65, 100);
                        pipe2UpRect = new Rectangle(300, 250, 65, 400);
                        pipe2DownRect = new Rectangle(300, 0, 65, 125);
                        pipe3UpRect = new Rectangle(500, 350, 65, 400);
                        pipe3DownRect = new Rectangle(500, 0, 65, 225);
                        pipe4UpRect = new Rectangle(700, 400, 65, 550);
                        pipe4DownRect = new Rectangle(700, 0, 65, 300);
                        exitRect = new Rectangle(50, 60, 400, 450);
                        exitClikerRect = new Rectangle(117, 230, 250, 110);
                        retryRect = new Rectangle(375, 100, 375, 375);
                        retryClickerRect = new Rectangle(426, 230, 250, 110);
                        screen = Screen.Game;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(rectangleTexture, rectangleRect, Color.White);
                _spriteBatch.Draw(introTexture, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.Draw(startTexture, startRect, Color.White);
                _spriteBatch.DrawString(titleFont, "Instructions:", new Vector2(75, 350), Color.Black);
                _spriteBatch.DrawString(titleFont, "You will use the up and down arrows", new Vector2(25, 375), Color.Black);
                _spriteBatch.DrawString(titleFont, "to navigate through the pipes trying", new Vector2(25, 400), Color.Black);
                _spriteBatch.DrawString(titleFont, "not to hit any. You can quit or retry", new Vector2(25, 425), Color.Black);
                _spriteBatch.DrawString(titleFont, "anytime you hit a pipe. Good Luck!", new Vector2(25, 450), Color.Black);
                _spriteBatch.DrawString(titleFont, "The goal is to make it to the end.", new Vector2(25, 475), Color.Black);
                _spriteBatch.DrawString(titleFont, "To make it to the end, pass 20 pipes.", new Vector2(25, 500), Color.Black);
                _spriteBatch.DrawString(titleFont, "CLICK START", new Vector2(25, 525), Color.Black);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(birdTexture, birdRect, Color.White);
                _spriteBatch.Draw(pipe1UpTexture, pipe1UpRect, Color.White);
                _spriteBatch.Draw(pipe1DownTexture, pipe1DownRect, Color.White);
                _spriteBatch.Draw(pipe1UpTexture, pipe2UpRect, Color.White);
                _spriteBatch.Draw(pipe1DownTexture, pipe2DownRect, Color.White);
                _spriteBatch.Draw(pipe1UpTexture, pipe3UpRect, Color.White);
                _spriteBatch.Draw(pipe1DownTexture, pipe3DownRect, Color.White);
                _spriteBatch.Draw(pipe1UpTexture, pipe4UpRect, Color.White);
                _spriteBatch.Draw(pipe1DownTexture, pipe4DownRect, Color.White);
            }
            else if (screen == Screen.Dead)
            {
                _spriteBatch.Draw(rectangleTexture, exitClikerRect, Color.White);
                _spriteBatch.Draw(rectangleTexture, retryClickerRect, Color.White);
                _spriteBatch.Draw(deadTexture, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.Draw(exitTexture, exitRect, Color.White);
                _spriteBatch.Draw(retryTexture, retryRect, Color.White);
            }
            else if (screen == Screen.EndScreen)
            {
                _spriteBatch.Draw(endScreenTexture, new Rectangle(0, 0, 800, 600), Color.White);
            }
                _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
