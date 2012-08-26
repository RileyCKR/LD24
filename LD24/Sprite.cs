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
        }

        public virtual void OnCollision(Sprite caller)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle map, Camera camera)
        {
            Vector2 drawPosition = Position;

            if (camera.Position.X < 2000)
            {
                if ((map.Width - Position.X) < 2000)
                {
                    drawPosition.X = Position.X - map.Width;
                }
            }
            else if (map.Width - camera.Position.X < 2000)
            {
                if (Position.X < 2000)
                {
                    drawPosition.X = map.Width + Position.X;
                }
            }

            if (camera.Position.Y < 2000)
            {
                if ((map.Height - Position.Y) < 2000)
                {
                    drawPosition.Y = Position.Y - map.Height;
                }
            }
            else if (map.Height - camera.Position.Y < 2000)
            {
                if (Position.Y < 2000)
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