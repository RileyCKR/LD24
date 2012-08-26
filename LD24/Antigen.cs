using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class Antigen : Sprite
    {
        public static Antigen Build()
        {
            return new Antigen(GameAssets.Antigen1, new Rectangle(0, 0, 32, 32));
        }

        public Antigen(Texture2D texture, Rectangle drawBounds)
            : base (texture, drawBounds)
        {
            this.Type = SpriteType.Antigen;
            Velocity = RNG.RandomUnitVector();
            this.Speed = 1;
        }
    }
}
