using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace LD24
{
    internal static class GameAssets
    {
        internal static Texture2D Cell1 { get; private set; }
        internal static Texture2D Virus1 { get; private set; }
        internal static Texture2D TCell1 { get; private set; }
        internal static Texture2D Antigen1 { get; private set; }
        internal static Texture2D Bubble1 { get; private set; }
        internal static Texture2D Backdrop { get; private set; }

        internal static SpriteFont FontArial { get; private set; }

        internal static SoundEffect HitSound { get; private set; }
        internal static SoundEffect UpgradeSound { get; private set; }
        internal static SoundEffect BirthSound { get; private set; }

        internal static Song Music { get; private set; }

        internal static void Load(ContentManager contentManager)
        {
            Cell1 = contentManager.Load<Texture2D>(@"Cell1");
            Virus1 = contentManager.Load<Texture2D>(@"Virus1");
            TCell1 = contentManager.Load<Texture2D>(@"TCell1");
            Antigen1 = contentManager.Load<Texture2D>(@"Antigen1");
            Bubble1 = contentManager.Load<Texture2D>(@"Bubble1");
            Backdrop = contentManager.Load<Texture2D>(@"Backdrop");

            FontArial = contentManager.Load<SpriteFont>(@"Fonts\Arial");

            HitSound = contentManager.Load<SoundEffect>(@"Sounds\Hit");
            UpgradeSound = contentManager.Load<SoundEffect>(@"Sounds\Upgrade");
            BirthSound = contentManager.Load<SoundEffect>(@"Sounds\Birth");

            Music = contentManager.Load<Song>(@"Music");
        }
    }
}