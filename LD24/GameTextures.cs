using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    internal static class GameTextures
    {
        internal static Texture2D Cell1 { get; private set; }
        internal static Texture2D Virus1 { get; private set; }
        internal static Texture2D TCell1 { get; private set; }
        internal static void Load(ContentManager contentManager)
        {
            Cell1 = contentManager.Load<Texture2D>("Cell1");
            Virus1 = contentManager.Load<Texture2D>("Virus1");
            TCell1 = contentManager.Load<Texture2D>("TCell1");
        }
    }
}