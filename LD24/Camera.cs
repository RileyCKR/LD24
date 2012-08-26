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

        public void Lock(Vector2 lockPosition, Rectangle screen)
        {
            Position = lockPosition;
            Center = Position - new Vector2(screen.Width / 2, screen.Height / 2);
        }

        public Matrix CreateTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-Center, 0f));
        }
    }
}
