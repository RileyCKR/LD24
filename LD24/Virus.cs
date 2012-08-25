using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class Virus : Sprite
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        public Virus(Texture2D texture, Rectangle drawBounds) 
            : base (texture, drawBounds)
        {
            this.Speed = 0.15F;
        }

        public override void Update()
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

        public void ApplyThrust(Vector2 moveVector)
        {
            if (moveVector != Vector2.Zero)
            {
                moveVector.Normalize();
                this.Velocity += moveVector * Speed;
            }
        }

        private void ApplyFriction()
        {
            Vector2 friction = this.Velocity / 100;
            this.Velocity -= friction;
        }

        private void Rotate()
        {
            if (this.Velocity != Vector2.Zero)
            {
                this.Rotation = (float)Math.Atan2(this.Velocity.X, -this.Velocity.Y);
            }
        }
    }
}