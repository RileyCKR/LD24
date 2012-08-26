using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class Cell : Sprite
    {
        public static Cell Build()
        {
            return new Cell(GameAssets.Cell1, new Rectangle(0, 0, 64, 64));
        }

        public enum CellState
        {
            Living,
            Sick,
            Dead
        }

        public int Energy { get; set; }
        public CellState State;

        private int DeathCounter = 255;

        public Cell(Texture2D texture, Rectangle drawBounds)
            : base (texture, drawBounds)
        {
            this.Type = SpriteType.Cell;
            this.Energy = 7;
            this.State = CellState.Living;
        }

        public void AddVirus(Virus virus)
        {

        }

        public int DrainEnergy()
        {
            if (State != CellState.Dead)
            {
                Energy--;

                if (Energy <= 4)
                {
                    State = CellState.Sick;
                }
                if (Energy <= 0)
                {
                    SoundManager.PlaySound(GameAssets.CellDeadSound, 1F);
                    State = CellState.Dead;
                    this.Tint = new Color(50, 50, 50, 128);
                    GameStats.IncrementDeadCellCount();
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override void Update(SceneGraph graph)
        {
            base.Update(graph);

            if (State == CellState.Dead)
            {
                if (DeathCounter > 0)
                {
                    DeathCounter--;
                    Tint = new Color(Tint.R, Tint.G, Tint.B, DeathCounter);
                }
                else
                {
                    graph.Remove(this);
                }
            }
            
        }

        
    }
}