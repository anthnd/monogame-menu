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
        GameObject buttonStart, buttonExit, buttonResume, buttonOutline;

        // Game state control
        const int STATE_MENU = 0;
        const int STATE_PLAYING = 1;
        const int STATE_PAUSED = 2;
        const int STATE_GAMEOVER = 3;
        int GameState = STATE_MENU;


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
            Texture2D start = Content.Load<Texture2D>("menu/button_start");
            Texture2D exit = Content.Load<Texture2D>("menu/button_exit");
            Texture2D resume = Content.Load<Texture2D>("menu/button_resume");
            Texture2D outline = Content.Load<Texture2D>("menu/button_outline");
            buttonStart = new GameObject(start, HAlignedTextureRectangle(start, 170));
            buttonExit = new GameObject(exit, HAlignedTextureRectangle(exit, 270));
            buttonResume = new GameObject(resume, HAlignedTextureRectangle(resume, 170));
            buttonOutline = new GameObject(outline, HAlignedTextureRectangle(outline, 0));
            buttonResume.Disable(spriteBatch);
            buttonOutline.Disable(spriteBatch);
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
            KeyboardState keyboardState = Keyboard.GetState();

            if (ButtonIntersects(ref mouseState, buttonStart) || ButtonIntersects(ref mouseState, buttonExit) || ButtonIntersects(ref mouseState, buttonResume))
                buttonOutline.Enable(spriteBatch);
            else
                buttonOutline.Disable(spriteBatch);

            if (buttonStart.BoundingBox.Contains(mouseState.Position))
                buttonOutline.Position.Y = 170;
            if (buttonExit.BoundingBox.Contains(mouseState.Position))
                buttonOutline.Position.Y = 270;
            if (buttonResume.BoundingBox.Contains(mouseState.Position))
                buttonOutline.Position.Y = 170;

            switch(GameState)
            {
                case STATE_MENU:
                    if (Clicked(ref mouseState, buttonStart))
                        GameState = STATE_PLAYING;
                    if (Clicked(ref mouseState, buttonExit))
                        Exit();
                    break;
                case STATE_PLAYING:
                    buttonStart.Disable(spriteBatch);
                    buttonExit.Disable(spriteBatch);
                    buttonResume.Disable(spriteBatch);
                    if (keyboardState.IsKeyDown(Keys.P) || keyboardState.IsKeyDown(Keys.Escape))
                        GameState = STATE_PAUSED;
                    break;
                case STATE_PAUSED:
                    buttonResume.Enable(spriteBatch);
                    buttonExit.Enable(spriteBatch);
                    if (Clicked(ref mouseState, buttonResume))
                        GameState = STATE_PLAYING;
                    if (Clicked(ref mouseState, buttonExit))
                        Exit();
                    break;
                case STATE_GAMEOVER:
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
            GraphicsDevice.Clear(new Color(15,15,15));

            spriteBatch.Begin();
            buttonStart.Draw(spriteBatch);
            buttonExit.Draw(spriteBatch);
            buttonResume.Draw(spriteBatch);
            buttonOutline.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool Clicked(ref MouseState mouseState, GameObject button)
        {
            return ButtonIntersects(ref mouseState, button) && (mouseState.LeftButton == ButtonState.Pressed);
        }

        private bool ButtonIntersects(ref MouseState mouseState, GameObject button)
        {
            if (GameState == STATE_MENU || GameState == STATE_PAUSED)
                return button.BoundingBox.Contains(mouseState.Position);
            return false;
        }

        private void UpdateState()
        {
            switch(GameState) {
		        case STATE_MENU:
                    break;
                default:
                    break;
	        }
        }

        private Vector2 HAlignedTextureRectangle(Texture2D texture, int height)
        {
            return new Vector2(
                (GraphicsDevice.Viewport.Width - texture.Width) / 2,
                height
            );
        }
    }
}
