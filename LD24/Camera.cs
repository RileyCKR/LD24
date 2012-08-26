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

        public Camera(Rectangle screen)
        {
            this.screen = screen;
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
    }
}
