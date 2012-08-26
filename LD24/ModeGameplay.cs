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
        public static Virus Virus;
        Camera Camera;
        SceneGraph SceneGraph;
        DebugHud DebugHud;
        Background Background;

        int deathCounter = 0;

        public ModeGameplay(Game game, InputState inputState, GraphicsDevice graphicsDevice)
        {
            this.Game = game;
            this.InputState = inputState;
            Background = new Background(graphicsDevice);
            Camera = new Camera(graphicsDevice.Viewport.Bounds);
        }

        public void Initialize()
        {
            Rectangle map = new Rectangle(0, 0, 10000, 10000);
           
            SceneGraph = new SceneGraph(map, Camera);
            
            DebugHud = new DebugHud(new Vector2(16, 16), GameAssets.FontArial, Color.Black, Color.Red, Color.Black);

            
            SeedLevel(map);
        }

        public void Update(GameTime gameTime)
        {
            if (Virus.VirusMode != LD24.Virus.Mode.Dead)
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
            }
            else
            {
                deathCounter++;
                if (deathCounter % 180 == 0)
                {
                    Virus newVirus = SceneGraph.FindLivingVirus();
                    if (newVirus != null)
                    {
                        Virus = newVirus;
                    }
                    else
                    {
                        GameOver();
                    }
                }
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

        private void GameOver()
        {
            //TODO: Implement
            this.Game.Exit();
        }

        private void SeedLevel(Rectangle map)
        {
            int numCells = 100;
            int numTCells = 20;
            int numAntigens = 50;
            int numBubbles = 5;

            //TODO: Check for collisions before adding to sceneGraph

            for (int x = 0; x < numTCells; x++)
            {
                TCell tcell = TCell.Build();
                tcell.Position = RNG.RandomVectorWithinBounds(map);
                SceneGraph.Add(tcell);
            }

            for (int x = 0; x < numCells; x++)
            {
                Cell cell = Cell.Build();
                cell.Position = RNG.RandomVectorWithinBounds(map);
                SceneGraph.Add(cell);
            }

            Virus = Virus.Build();
            SceneGraph.Add(Virus);

            for (int x = 0; x < numAntigens; x++)
            {
                Antigen antigen = Antigen.Build();
                antigen.Position = RNG.RandomVectorWithinBounds(map);
                SceneGraph.Add(antigen);
            }

            for (int x = 0; x < numBubbles; x++)
            {
                Bubble bubble = Bubble.Build();
                bubble.Position = RNG.RandomVectorWithinBounds(Camera.ScreenBounds);
                SceneGraph.Add(bubble);
            }
        }
    }
}
