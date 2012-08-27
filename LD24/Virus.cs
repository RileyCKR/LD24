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
            Virus virus = new Virus(GameAssets.Virus1, new Rectangle(0, 0, 16, 32));
            Animation anim = new Animation(2, 16, 32, 0, 0);
            virus.IsAnimating = true;
            virus.Animation = anim;
            anim.FramesPerSecond = 2;
            return virus;
        }

        public enum Mode
        {
            Free,
            Infecting,
            Dead
        }

        public Mode VirusMode;
        public Cell InfectedCell;
        private int ticks;
        public int Energy;
        private int DeathCounter = 255;
        private int health = 2;
        private int ImmunityCounter = 120;
        private int ImmunityTicks = 0;
        private bool IsImmune = false;
        static bool birthSoundPlayed;
        public bool IsPlayer;

        public Virus(Texture2D texture, Rectangle drawBounds) 
            : base (texture, drawBounds)
        {
            this.Speed = GameStats.VirusSpeed;
            this.Type = SpriteType.Virus;
            this.Energy = 0;
        }

        public override void Update(SceneGraph graph)
        {
            birthSoundPlayed = false;
            this.Speed = GameStats.VirusSpeed;

            base.Update(graph);

            if (VirusMode == Mode.Free)
            {
                if (Velocity != Vector2.Zero)
                {
                    Rotate();
                    if (IsPlayer)
                    {
                        ApplyFriction();
                    }
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
            else if (VirusMode == Mode.Dead)
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

            if (IsImmune)
            {
                ImmunityTicks++;

                if (ImmunityTicks >= ImmunityCounter)
                {
                    IsImmune = false;
                    this.Tint = Color.White;
                }
                else
                {
                    this.Tint = Color.Red;
                }
            }
        }

        public override void OnCollision(Sprite caller)
        {
            if (VirusMode == Mode.Free)
            {
                base.OnCollision(caller);

                if (caller.Type == SpriteType.Cell)
                {
                    Cell callerCell = caller as Cell;
                    if (callerCell.State != Cell.CellState.Dead)
                    {
                        this.Velocity = Vector2.Zero;
                        this.Position = caller.Position;
                        this.InfectedCell = callerCell;
                        this.VirusMode = Mode.Infecting;
                    }
                }
                else if (caller.Type == SpriteType.TCell)
                {
                    Wound();

                    if (VirusMode == Mode.Dead)
                    {
                        this.Velocity = Vector2.Zero;
                        this.Position = caller.Position;
                        Vector2 offset = GetRandomOffsetWithinBounds(caller.DrawBounds);
                        this.Position += offset;
                    }
                }
                else if (caller.Type == SpriteType.Antigen)
                {
                    Wound();
                }
            }
        }

        public void ApplyThrust(Vector2 moveVector)
        {
            if (VirusMode == Mode.Free && moveVector != Vector2.Zero)
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
            if (Energy >= GameStats.ChildEnergyCost)
            {
                if (!birthSoundPlayed)
                {
                    SoundManager.PlaySound(GameAssets.BirthSound, Position, 1F);
                    birthSoundPlayed = true;
                }

                Virus childVirus = Virus.Build();
                childVirus.VirusMode = Mode.Infecting;
                childVirus.InfectedCell = this.InfectedCell;

                Vector2 childPosition = InfectedCell.Position;
                Vector2 offset = GetRandomOffsetWithinBounds(InfectedCell.DrawBounds);

                childVirus.Position = InfectedCell.Position + offset;

                graph.Add(childVirus);
                Energy = 0;
            }
        }

        private Vector2 GetRandomOffsetWithinBounds(Rectangle bounds)
        {
            Vector2 offset = new Vector2();

            int halfWidth = bounds.Width / 2;
            int halfHeight = bounds.Height / 2;
            offset.X = RNG.NexVal(-halfWidth, halfWidth);
            offset.Y = RNG.NexVal(-halfHeight, halfHeight);

            return offset;
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
                if (GameStats.FlagQuickness)
                {
                    this.Velocity = this.Velocity * 3;
                }
            }
        }

        private void Wound()
        {
            if (!IsImmune)
            {
                SoundManager.PlaySound(GameAssets.HitSound, Position, 1F);
                health -= GameStats.DamageCost;

                if (health <= 0)
                {
                    this.VirusMode = Mode.Dead;
                    this.Tint = Color.Gray;
                    GameStats.IncrementDeadVirusCount();
                    this.IsAnimating = false;
                }
                else
                {
                    IsImmune = true;
                    ImmunityTicks = 0;
                }
            }
        }
    }
}