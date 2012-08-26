using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class VictoryModal : Sprite
    {
        public static int SelectedIndex = 0;
        private SpriteFont Font;

        public static VictoryModal Build()
        {
            return new VictoryModal(GameAssets.Backdrop, new Rectangle(0, 0, 512, 256), GameAssets.FontArial);
        }

        public VictoryModal(Texture2D texture, Rectangle drawBounds, SpriteFont font)
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
            string text = "Victory!" + Environment.NewLine + Environment.NewLine;
            text += "You have brought forth an infection that the" + Environment.NewLine;
            text += "world will not soon forget.  Soon all of" + Environment.NewLine;
            text += "humanity will be crippled with illness" + Environment.NewLine + Environment.NewLine;
            text += "(Press Esc to exit)";

            return text;
        }
    }
}