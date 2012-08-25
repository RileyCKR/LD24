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
        public Vector2 Center { get; set; }
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

        public Sprite(Texture2D texture, Rectangle drawBounds)
        {
            this.Tint = Color.White;
            this.Texture = texture;
            DrawBounds = drawBounds;
            Center = new Vector2(drawBounds.Width / 2, drawBounds.Height / 2);
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
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
    }
}
