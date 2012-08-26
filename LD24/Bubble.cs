using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace LD24
{
    class Bubble : Sprite
    {
        public static Bubble Build()
        {
            return new Bubble(GameAssets.Bubble1, new Rectangle(0, 0, 32, 32));
        }

        public Bubble(Texture2D texture, Rectangle drawBounds)
            : base(texture, drawBounds)
        {
            this.Type = SpriteType.Bubble;
            this.Speed = 0.25F;
            this.Velocity = RNG.RandomUnitVector();
        }
    }
}
