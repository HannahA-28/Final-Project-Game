using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project_Game
{
    enum Screen
    {
        Intro,
        game,
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

        Texture2D rectangleTexture;
        Rectangle rectangleRect;

        Texture2D introTexture;
        Texture2D gameTexture;
        Texture2D endScreenTexture;

        MouseState mouseState;
        MouseState prevMouseState;

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
            rectangleRect = new Rectangle();

            screen = Screen.Intro;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            introTexture = Content.Load<Texture2D>("Intro1");
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            gameTexture = Content.Load<Texture2D>("Game");
            startTexture = Content.Load<Texture2D>("Start");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            this.Window.Title = $"({mouseState.X}, {mouseState.Y})";
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton = ButtonState.Pressed)
                {
                    if (rectangleRect.Contains(mouseState.Position))
                        screen = Screen.game;
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
            }
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
