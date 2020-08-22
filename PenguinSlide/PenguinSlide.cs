using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PenguinSlide.GameState;
using PenguinSlide.Sound;

namespace PenguinSlide
{
    public class PenguinSlide : Game
    {
        private readonly GraphicsDeviceManager graphics;

        private State currentState;
        private SpriteBatch spriteBatch;

        public PenguinSlide()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void ChangeState(State state)
        {
            currentState = state;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            ChangeState(new MenuState(GraphicsDevice, Content, this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            SoundPlayer.JumpSound = Content.Load<SoundEffect>("sounds/jump");
            SoundPlayer.PickupSound = Content.Load<SoundEffect>("sounds/pickup");
            SoundPlayer.ButtonSound = Content.Load<SoundEffect>("sounds/button");
            SoundPlayer.DieSound = Content.Load<SoundEffect>("sounds/die");
            SoundPlayer.EndSound = Content.Load<SoundEffect>("sounds/finish");
            try
            {
                SoundPlayer.Music = Content.Load<Song>("sounds/music");

                MediaPlayer.Play(SoundPlayer.Music);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume -= 0.2f;
            }
            catch (Exception)
            {
                // Media Player not installed
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            currentState.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}