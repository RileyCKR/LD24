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
        SceneGraph SceneGraph;
        DebugHud DebugHud;
        Background Background;
        public ModeGameplay(Game game, InputState inputState, GraphicsDevice graphicsDevice)
        {
            this.Game = game;
            this.InputState = inputState;
            Background = new Background(graphicsDevice);
        }

        public void Initialize()
        {
            DebugHud = new DebugHud(new Vector2(16, 16), GameAssets.FontArial, Color.Black, Color.Red, Color.Black);
            
            Virus = Virus.Build();
            Cells = new Cell[]
            {
                new Cell(GameAssets.Cell1, new Rectangle(0, 0, 64, 64)) { Position = new Vector2(200, 200) },
                new Cell(GameAssets.Cell1, new Rectangle(0, 0, 64, 64)) { Position = new Vector2(-100, -300) },
                new Cell(GameAssets.Cell1, new Rectangle(0, 0, 64, 64)) { Position = new Vector2(600, 100) },
                new Cell(GameAssets.Cell1, new Rectangle(0, 0, 64, 64)) { Position = new Vector2(1000, 1000) }
            };
            Camera = new Camera();

            SceneGraph = new SceneGraph();
            foreach (Cell cell in Cells)
            {
                SceneGraph.Add(cell);
            }
            SceneGraph.Add(Virus);

            TCell tCell = new TCell(GameAssets.TCell1, new Rectangle(0, 0, 128, 128)) { Position = new Vector2(512, 512) };
            SceneGraph.Add(tCell);

            Antigen antigen = new Antigen(GameAssets.Antigen1, new Rectangle(0, 0, 32, 32)) { Position = new Vector2(300, 250) };
            SceneGraph.Add(antigen);
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

            SceneGraph.Update();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            //TODO: Move lock to update?
            Camera.Lock(Virus.Position, screenBounds);

            Background.Draw();

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Camera.CreateTransformation());

            SceneGraph.Draw(spriteBatch);

            spriteBatch.End();

            DebugHud.Draw(gameTime, spriteBatch);
        }
    }
}
