using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class TCell : Sprite
    {
        int ticks = 0;

        public static TCell Build()
        {
            return new TCell(GameAssets.TCell1, new Rectangle(0, 0, 128, 128));
        }

        public TCell(Texture2D texture, Rectangle drawBounds)
            : base (texture, drawBounds)
        {
            this.Type = SpriteType.TCell;
            this.Speed = 0.5F;
        }

        public override void Update(SceneGraph graph)
        {
            base.Update(graph);

            ticks = ticks % 120;
            if (ticks == 0)
            {
                 Velocity = RNG.RandomUnitVector();
            }

            ticks++;
        }
    }
}
