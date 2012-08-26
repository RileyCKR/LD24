﻿using System;
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
        Camera Camera;
        SceneGraph SceneGraph;
        DebugHud DebugHud;
        Background Background;
        EvolutionModal EvoModal;
        GameOverModal GameOverModal;
        VictoryModal VictoryModal;

        int deathCounter = 0;
        bool gameOver;
        bool victory;

        public ModeGameplay(Game game, InputState inputState, GraphicsDevice graphicsDevice)
        {
            this.Game = game;
            this.InputState = inputState;
            Background = new Background(graphicsDevice);
            Camera = new Camera(graphicsDevice.Viewport.Bounds);
            SoundManager.camera = Camera;
        }

        public void Initialize()
        {
            Rectangle map = new Rectangle(0, 0, 5000, 5000);
           
            SceneGraph = new SceneGraph(map, Camera);
            
            DebugHud = new DebugHud(new Vector2(16, 16), GameAssets.FontArial, Color.Black, Color.Black, Color.Black);

            EvoModal = EvolutionModal.Build();
            
            SeedLevel(map);

            SoundManager.PlayMusic();
        }

        public void Update(GameTime gameTime)
        {
            if (this.Game.IsActive)
            {
                if (InputState.KeyDown(Keys.M))
                {
                    SoundManager.ToggleMusic();
                }

                if (InputState.KeyDown(Keys.OemTilde))
                {
                    DebugHud.ShowDebugInfo = !DebugHud.ShowDebugInfo;
                }

                if (!GameStats.NeedsEvolve)
                {
                    Virus player = GameStats.PlayerVirus;
                    if (player.VirusMode != LD24.Virus.Mode.Dead)
                    {
                        if (InputState.KeyPressed(Keys.W))
                        {
                            player.ApplyThrust(-Vector2.UnitY);
                        }
                        else if (InputState.KeyPressed(Keys.S))
                        {
                            player.ApplyThrust(Vector2.UnitY);
                        }

                        if (InputState.KeyPressed(Keys.A))
                        {
                            player.ApplyThrust(-Vector2.UnitX);
                        }
                        else if (InputState.KeyPressed(Keys.D))
                        {
                            player.ApplyThrust(Vector2.UnitX);
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
                                GameStats.PlayerVirus = newVirus;
                            }
                            else
                            {
                                GameOver();
                            }
                        }
                    }

                    SceneGraph.Update();
                }
                else
                {
                    //Show evolution screen
                    if (InputState.KeyDown(Keys.Up) || InputState.KeyDown(Keys.W))
                    {
                        EvoModal.MoveCaratDown();
                    }
                    else if (InputState.KeyDown(Keys.Down) || InputState.KeyDown(Keys.S))
                    {
                        EvoModal.MoveCaratUp();
                    }
                    else if (InputState.KeyDown(Keys.Enter))
                    {
                        EvoModal.Select();
                    }
                }

                if (GameStats.DeadCellCount >= 50)
                {
                    Victory();
                }

                if (gameOver || victory)
                {
                    if (InputState.KeyPressed(Keys.Enter) || InputState.KeyPressed(Keys.Escape))
                    {
                        this.Game.Exit();
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            //TODO: Move lock to update?
            Camera.Lock(GameStats.PlayerVirus.Position, screenBounds);

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

            if (GameStats.NeedsEvolve)
            {
                spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    null,
                    null,
                    null);

                EvoModal.Draw(spriteBatch, SceneGraph.Map, Camera);

                spriteBatch.End();
            }

            if (gameOver)
            {
                spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    null,
                    null,
                    null);

                GameOverModal.Draw(spriteBatch, SceneGraph.Map, Camera);

                spriteBatch.End();
            }

            if (victory)
            {
                spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    null,
                    null,
                    null);

                VictoryModal.Draw(spriteBatch, SceneGraph.Map, Camera);

                spriteBatch.End();
            }

            DebugHud.Draw(gameTime, spriteBatch);
        }

        private void GameOver()
        {
            gameOver = true;
            GameOverModal = GameOverModal.Build();
            //TODO: Implement
            //this.Game.Exit();
        }

        private void Victory()
        {
            victory = true;
            VictoryModal = VictoryModal.Build();
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

            Virus player = Virus.Build();
            SceneGraph.Add(player);
            GameStats.PlayerVirus = player;

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