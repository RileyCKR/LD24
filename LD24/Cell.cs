using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WickedEngine;

namespace LD24
{
    class Cell
    {
        public Texture2D Texture { get; protected set; }
        public Color Tint { get; set; }
        public Vector2 Position { get; set; }

        public Cell(Texture2D texture)
        {
            this.Tint = Color.White;
            this.Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Texture.Bounds, Color.White);
        }
    }
}