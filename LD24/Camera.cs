using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LD24
{
    class Camera
    {
        public Vector2 Position;
        public Vector2 Center;
        public Rectangle screen;
        public Rectangle map;

        public Rectangle ScreenBounds
        {
            get
            {
                Rectangle rect = new Rectangle((int)Position.X, (int)Position.Y, screen.Width, screen.Height);
                Point loc = rect.Location;
                loc.X -= screen.Width / 2;
                loc.Y -= screen.Height / 2;
                rect.Location = loc;
                return rect;
            }
        }

        public Camera(Rectangle screen, Rectangle map)
        {
            this.screen = screen;
            this.map = map;
        }

        public void Lock(Vector2 lockPosition, Rectangle screen)
        {
            this.screen = screen;
            Position = lockPosition;
            Center = Position - new Vector2(screen.Width / 2, screen.Height / 2);
        }

        public Matrix CreateTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-Center, 0f));
        }

        //TODO: Need to account for edge cases when map is wrapping
        public bool IsOnScreen(Vector2 pos)
        {
            Vector2 drawPosition = GetDrawPosition(pos);

            if (ScreenBounds.Contains(new Point((int)drawPosition.X, (int)drawPosition.Y)))
            {
                return true;
            }
            else return false;
        }

        public bool IsOnScreen(Rectangle bounds)
        {
            Vector2 boundsPosition = new Vector2(bounds.X, bounds.Y);
            Vector2 drawPosition = GetDrawPosition(boundsPosition);
            Rectangle cloneRect = new Rectangle((int)drawPosition.X, (int)drawPosition.Y, bounds.Width, bounds.Height);

            if (ScreenBounds.Contains(cloneRect))
            {
                return true;
            }
            else return false;
        }

        public Vector2 GetDrawPosition(Vector2 pos)
        {
            Vector2 drawPosition = pos;

            if (this.Position.X < this.screen.Width)
            {
                if ((map.Width - pos.X) < this.screen.Width)
                {
                    drawPosition.X = pos.X - map.Width;
                }
            }
            else if (map.Width - this.Position.X < this.screen.Width)
            {
                if (pos.X < this.screen.Width)
                {
                    drawPosition.X = map.Width + pos.X;
                }
            }

            if (this.Position.Y < this.screen.Height)
            {
                if ((map.Height - pos.Y) < this.screen.Height)
                {
                    drawPosition.Y = pos.Y - map.Height;
                }
            }
            else if (map.Height - this.Position.Y < this.screen.Height)
            {
                if (pos.Y < this.screen.Height)
                {
                    drawPosition.Y = map.Height + pos.Y;
                }
            }

            return drawPosition;
        }

    }
}
