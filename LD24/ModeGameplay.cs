using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD24
{
    class ModeGameplay
    {
        Game Game;
        InputState InputState;
        Cell[] Cells;
        Virus Virus;
        Camera Camera;

        public ModeGameplay(Game game, InputState inputState)
        {
            this.Game = game;
            this.InputState = inputState;
        }

        public void Initialize()
        {
            Virus = new Virus(GameTextures.Virus1);
            Cells = new Cell[]
            {
                new Cell(GameTextures.Cell1) { Position = new Vector2(200, 200) },
                new Cell(GameTextures.Cell1) { Position = new Vector2(-100, -300) },
                new Cell(GameTextures.Cell1) { Position = new Vector2(600, 100) },
                new Cell(GameTextures.Cell1) { Position = new Vector2(1000, 1000) }
            };
            Camera = new Camera();
        }

        public void Update(GameTime gameTime)
        {
            if (InputState.KeyPressed(Keys.W))
            {
                Virus.ApplyThrust(-Vector2.UnitY);
            }
            else if (InputState.KeyPressed(Keys.S))
            {
                Virus.ApplyThrust(Vector2.UnitY);
            }

            if (InputState.KeyPressed(Keys.A))
            {
                Virus.ApplyThrust(-Vector2.UnitX);
            }
            else if (InputState.KeyPressed(Keys.D))
            {
                Virus.ApplyThrust(Vector2.UnitX);
            }

            Virus.Update();
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Camera.CreateTransformation());

            //TODO: Move lock to update?
            Camera.Lock(Virus.Position, screenBounds);

            Virus.Draw(spriteBatch);
            foreach (Cell cell in Cells)
            {
                cell.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
