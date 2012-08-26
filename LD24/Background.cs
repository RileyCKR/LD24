using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class Background
    {
        private GraphicsDevice graphicsDevice;
        private Color bgColor;
        private int ticks;
        private bool colorAscending;

        public Background(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.bgColor = new Color(0, 180, 200);
            this.colorAscending = true;
        }

        public void Draw()
        {
            ticks++;
            if (ticks % 6 == 0)
            {
                ticks = 0;
                if (colorAscending)
                {
                    if (bgColor.G < 230)
                    {
                        bgColor.G++;
                    }
                    else
                    {
                        this.colorAscending = false;
                    }
                }
                else
                {
                    if (bgColor.G > 180)
                    {
                        bgColor.G--;
                    }
                    else
                    {
                        this.colorAscending = true;
                    }
                }
            }

            graphicsDevice.Clear(bgColor);
        }
    }
}
