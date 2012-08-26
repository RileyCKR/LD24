using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WickedEngine;

namespace LD24
{
    class Cell : Sprite
    {
        public enum CellState
        {
            Living,
            Sick,
            Dead
        }

        public int Energy { get; set; }
        public CellState State;

        public Cell(Texture2D texture, Rectangle drawBounds)
            : base (texture, drawBounds)
        {
            this.Type = SpriteType.Cell;
            this.Energy = 10;
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

                if (Energy <= 5)
                {
                    State = CellState.Sick;
                    Tint = new Color(200, 200, 200, 255);
                }
                if (Energy <= 0)
                {
                    State = CellState.Dead;
                    this.Tint = new Color(50, 50, 50, 128);
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}