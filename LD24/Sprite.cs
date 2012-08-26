using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class Sprite
    {
        public Texture2D Texture { get; protected set; }
        public Color Tint { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle DrawBounds { get; set; }
        public Point Center { get; set; }
        public SpriteType Type { get; protected set; }
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        private float _Rotation;
        public float Rotation
        {
            get { return _Rotation; }
            set
            {
                _Rotation = value;
                _Rotation = MathHelper.WrapAngle(_Rotation);
            }
        }

        public Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int)Position.X, (int)Position.Y, DrawBounds.Width, DrawBounds.Height);
                box.Offset(-Center.X, -Center.Y);
                return box;
            }
        }

        public Sprite(Texture2D texture, Rectangle drawBounds)
        {
            this.Tint = Color.White;
            this.Texture = texture;
            DrawBounds = drawBounds;
            Center = new Point(drawBounds.Width / 2, drawBounds.Height / 2);
        }

        public virtual void Update(SceneGraph graph)
        {
            if (Velocity != Vector2.Zero)
            {
                Vector2 newPosition = Position;

                newPosition.X += Velocity.X * Speed;
                newPosition.Y += Velocity.Y * Speed;
                Position = newPosition;
            }
        }

        public virtual void OnCollision(Sprite caller)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle map, Camera camera)
        {
            Vector2 drawPosition = Position;
            Rectangle viewport = camera.screen;

            if (camera.Position.X < viewport.Width)
            {
                if ((map.Width - Position.X) < viewport.Width)
                {
                    drawPosition.X = Position.X - map.Width;
                }
            }
            else if (map.Width - camera.Position.X < viewport.Width)
            {
                if (Position.X < viewport.Width)
                {
                    drawPosition.X = map.Width + Position.X;
                }
            }

            if (camera.Position.Y < viewport.Height)
            {
                if ((map.Height - Position.Y) < viewport.Height)
                {
                    drawPosition.Y = Position.Y - map.Height;
                }
            }
            else if (map.Height - camera.Position.Y < viewport.Height)
            {
                if (Position.Y < viewport.Height)
                {
                    drawPosition.Y = map.Height + Position.Y;
                }
            }

            spriteBatch.Draw(
                    Texture,
                    drawPosition,
                    DrawBounds,
                    Tint,
                    Rotation,
                    new Vector2(Center.X, Center.Y),
                    1.0f,
                    SpriteEffects.None,
                    0f);
        }
    }
}