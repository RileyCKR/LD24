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
        public Antigen(Texture2D texture, Rectangle drawBounds)
            : base (texture, drawBounds)
        {
            this.Type = SpriteType.Antigen;
        }
    }
}
