using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace test_menu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Menu : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Menu textures
        Texture2D buttonStart, buttonExit, buttonResume;

        // Game state control
        const int START_MENU = 0;
        const int PLAYING = 1;
        const int PAUSED = 2;
        const int GAME_OVER = 3;

        int State = START_MENU;

        

        public Menu()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            // Load menu buttons
            buttonStart = Content.Load<Texture2D>("menu/button_start");
            buttonExit = Content.Load<Texture2D>("menu/button_exit");
            buttonResume = Content.Load<Texture2D>("menu/button_resume");
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

            MouseState mouseState = Mouse.GetState();

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
            spriteBatch.Draw(buttonStart, HAlignedTxtRect(buttonStart, 170), Color.White);
            spriteBatch.Draw(buttonExit, HAlignedTxtRect(buttonExit, 270), Color.White);
            spriteBatch.Draw(buttonResume, HAlignedTxtRect(buttonResume, 370), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateState()
        {
            
        }

        private Rectangle HAlignedTxtRect(Texture2D texture, int height)
        {
            return new Rectangle(
                (GraphicsDevice.Viewport.Width - texture.Width) / 2,
                height,
                texture.Width,
                texture.Height
            );
        }
    }
}
