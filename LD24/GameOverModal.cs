using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class GameOverModal : Sprite
    {
        public static int SelectedIndex = 0;
        private SpriteFont Font;

        public static GameOverModal Build()
        {
            return new GameOverModal(GameAssets.Backdrop, new Rectangle(0, 0, 512, 256), GameAssets.FontArial);
        }

        public GameOverModal(Texture2D texture, Rectangle drawBounds, SpriteFont font)
            : base (texture, drawBounds)
        {
            this.Font = font;
        }

        public override void Update(SceneGraph graph)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle map, Camera camera)
        {
            int xOffset = (camera.screen.Width - this.DrawBounds.Width) / 2;
            int yOffset = (camera.screen.Height - this.DrawBounds.Height) / 2;
            this.Position = new Vector2(xOffset, yOffset);

            spriteBatch.Draw(
                    Texture,
                    Position,
                    DrawBounds,
                    Tint,
                    Rotation,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0f);

            Vector2 textOffset = new Vector2(32, 32);
            string text = GetText();
            spriteBatch.DrawString(Font, text, Position + textOffset, Color.White);
        }

        private string GetText()
        {
            string text = "Game Over" + Environment.NewLine + Environment.NewLine;
            text += "Looks like the immune system wins today." + Environment.NewLine;
            text += "But you will be back and stronger than ever " + Environment.NewLine;
            text += "before!" + Environment.NewLine + Environment.NewLine;
            text += "(Press Esc to exit)";

            return text;
        }
    }
}