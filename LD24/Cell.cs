using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WickedEngine;

namespace LD24
{
    class Cell : Sprite
    {

        public Cell(Texture2D texture, Rectangle drawBounds)
            : base (texture, drawBounds)
        {
        }
    }
}