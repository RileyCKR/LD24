using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WickedEngine;

namespace LD24
{
    class Virus
    {
        public Texture2D Texture { get; protected set; }
        public Color Tint { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public Rectangle DrawBounds { get; set; }
        public Vector2 Center { get; set; }

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

        public Virus(Texture2D texture)
        {
            this.Tint = Color.White;
            this.Texture = texture;
            this.Speed = 0.15F;
            DrawBounds = new Rectangle(0, 0, 64, 64);
            Center = new Vector2(32, 32);
        }

        public void Update()
        {
            if (Velocity != Vector2.Zero)
            {
                Vector2 newPosition = Position;

                newPosition.X += Velocity.X * Speed;
                newPosition.Y += Velocity.Y * Speed;
                Position = newPosition;
                Rotate();
                ApplyFriction();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, Position, Texture.Bounds, Color.White);
            //Vector2 CenterPoint = Vector2.Zero;
            spriteBatch.Draw(
                    Texture,
                    Position,
                    DrawBounds,
                    Tint,
                    Rotation,
                    Center,
                    1.0f,
                    SpriteEffects.None,
                    0f);
        }

        public void ApplyThrust(Vector2 moveVector)
        {
            if (moveVector != Vector2.Zero)
            {
                moveVector.Normalize();
                this.Velocity += moveVector * Speed;
            }
        }

        public void ApplyFriction()
        {
            Vector2 friction = this.Velocity / 100;
            this.Velocity -= friction;
        }

        public virtual void Rotate()
        {
            if (this.Velocity != Vector2.Zero)
            {
                this.Rotation = (float)Math.Atan2(this.Velocity.X, -this.Velocity.Y);
            }
        }
    }
}