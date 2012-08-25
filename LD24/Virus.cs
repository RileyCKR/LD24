using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class Virus : Sprite
    {
        public static Virus Build()
        {
            return new Virus(GameTextures.Virus1, new Rectangle(0, 0, 16, 32));
        }

        public enum Mode
        {
            Free,
            Infecting
        }

        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        public Mode VirusMode;
        public Cell InfectedCell;
        private int ticks;
        public int Energy;

        public Virus(Texture2D texture, Rectangle drawBounds) 
            : base (texture, drawBounds)
        {
            this.Speed = 0.15F;
            this.Type = SpriteType.Virus;
            this.Energy = 0;
        }

        public override void Update(SceneGraph graph)
        {
            if (VirusMode == Mode.Free)
            {
                if (Velocity != Vector2.Zero)
                {
                    Vector2 newPosition = Position;

                    newPosition.X += Velocity.X * Speed;
                    newPosition.Y += Velocity.Y * Speed;
                    Position = newPosition;
                    Rotate();
                    //ApplyFriction();
                }
            }
            else if (VirusMode == Mode.Infecting)
            {
                ticks++;
                if (ticks % 100 == 0)
                {
                    ticks = 0;
                    DrainEnergy();
                    BirthChild(graph);
                    Scatter();
                }
            }
        }

        public override void OnCollision(Sprite caller)
        {
            if (VirusMode != Mode.Infecting)
            {
                base.OnCollision(caller);

                Cell callerCell = caller as Cell;
                if (callerCell != null && callerCell.State != Cell.CellState.Dead)
                {
                    this.Velocity = Vector2.Zero;
                    this.Position = caller.Position;
                    this.InfectedCell = callerCell;
                    this.VirusMode = Mode.Infecting;
                }
            }
        }

        public void ApplyThrust(Vector2 moveVector)
        {
            if (moveVector != Vector2.Zero)
            {
                moveVector.Normalize();
                this.Velocity += moveVector * Speed;
            }
        }

        private void ApplyFriction()
        {
            Vector2 friction = this.Velocity / 100;
            this.Velocity -= friction;
        }

        private void Rotate()
        {
            if (this.Velocity != Vector2.Zero)
            {
                this.Rotation = (float)Math.Atan2(this.Velocity.X, -this.Velocity.Y);
            }
        }

        private void BirthChild(SceneGraph graph)
        {
            if (this.Energy > 0)
            {
                Virus childVirus = Virus.Build();
                childVirus.VirusMode = Mode.Infecting;
                childVirus.InfectedCell = this.InfectedCell;

                Vector2 childPosition = InfectedCell.Position;
                Vector2 offset = new Vector2();

                int halfWidth = InfectedCell.DrawBounds.Width / 2;
                int halfHeight = InfectedCell.DrawBounds.Height / 2;
                offset.X = RNG.NexVal(-halfWidth, halfWidth);
                offset.Y = RNG.NexVal(-halfHeight, halfHeight);

                childVirus.Position = InfectedCell.Position + offset;

                graph.Add(childVirus);
            }
        }

        private void DrainEnergy()
        {
            Energy += InfectedCell.DrainEnergy();
        }

        private void Scatter()
        {
            if (InfectedCell.State == Cell.CellState.Dead)
            {
                this.InfectedCell = null;
                this.VirusMode = Mode.Free;
                this.Velocity = RNG.RandomUnitVector();
            }
        }
    }
}