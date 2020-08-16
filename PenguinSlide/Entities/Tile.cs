﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide
{
    public class Tile: ICollidable
    {
        protected Texture2D Texture;
        public Vector2 Position;
        private Rectangle collisionRectangle;
        public Rectangle CollisionRectangle { get => collisionRectangle; set => collisionRectangle = value; }

        public Tile(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.collisionRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.Position = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
