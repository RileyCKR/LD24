using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24
{
    class SceneGraph
    {
        private List<Sprite> VirusGraph = new List<Sprite>();
        private List<Sprite> CellGraph = new List<Sprite>();
        private List<Sprite> TCellGraph = new List<Sprite>();
        private List<Sprite> AntigenGraph = new List<Sprite>();
        private List<Sprite> BubbleGraph = new List<Sprite>();

        int NodesCulled;
        int DrawCalls;
        int CollisionChecks;

        public Rectangle Map;
        public Camera Camera;

        public SceneGraph(Rectangle map, Camera camera)
        {
            this.Map = map;
            this.Camera = camera;
        }

        public void Add(Sprite obj)
        {
            switch (obj.Type)
            {
                case SpriteType.Virus:
                    VirusGraph.Add(obj as Virus);
                    break;
                case SpriteType.Cell:
                    CellGraph.Add(obj as Cell);
                    break;
                case SpriteType.TCell:
                    TCellGraph.Add(obj as TCell);
                    break;
                case SpriteType.Antigen:
                    AntigenGraph.Add(obj as Antigen);
                    break;
                case SpriteType.Bubble:
                    BubbleGraph.Add(obj as Bubble);
                    break;
                default:
                    throw new ApplicationException("Unrecognized Cell Type: " + obj.Type.ToString());
            }
        }

        public void Update()
        {
            UpdateRecursive(VirusGraph);
            UpdateRecursive(CellGraph);
            UpdateRecursive(TCellGraph);
            UpdateRecursive(AntigenGraph);
            UpdateRecursive(BubbleGraph);

            DebugHud.CountVirus = VirusGraph.Count;
            DebugHud.CountCell = CellGraph.Count;
            DebugHud.CountTCell = TCellGraph.Count;
            DebugHud.CountAntigen = AntigenGraph.Count;

            ProcessCollisionsFirstLoop();
        }

        private void UpdateRecursive(List<Sprite> layer)
        {
            //TODO: Items can be added to this list while the enumeration is happening,
            //should a buffer be implemented?
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                node.Update(this);

                node.Position = LockToMap(node.Position);
                
                //UpdateRecursive(node.Children);
            }
        }

        private void ProcessCollisionsFirstLoop()
        {
            CollisionChecks = 0;

            //check virus to cell
            ProcessCollisionsOuterLoop(VirusGraph, CellGraph);

            //check virus to tcell
            ProcessCollisionsOuterLoop(VirusGraph, TCellGraph);

            //check virus to antigen
            ProcessCollisionsOuterLoop(VirusGraph, AntigenGraph);
            
            //check tcell to tcell
            //ProcessCollisionsOuterLoop(TCellGraph, TCellGraph);

            //check tcell to cell
            //ProcessCollisionsOuterLoop(TCellGraph, CellGraph);

            //for (int x = 0; x < layer.Count; x++)
            //{
            //    Sprite node = layer[x];

            //    ProcessCollisionsRecursive(Graph, node);
            //}

            DebugHud.CountCollisionChecks = CollisionChecks;
        }

        private void ProcessCollisionsOuterLoop(List<Sprite> layer, List<Sprite> check)
        {
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];
                ProcessCollisions(check, node);
            }
        }

        private void ProcessCollisions(List<Sprite> layer, Sprite caller)
        {
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                //Don't check for collisions with yourself
                if (node != caller)
                {
                    CollisionChecks++;
                    if (node.CollisionBox.Intersects(caller.CollisionBox))
                    {
                        caller.OnCollision(node);
                        break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            NodesCulled = 0;
            DrawCalls = 0;

            DrawRecursive(TCellGraph, spriteBatch);
            DrawRecursive(CellGraph, spriteBatch);
            DrawRecursive(VirusGraph, spriteBatch);
            DrawRecursive(AntigenGraph, spriteBatch);
            DrawRecursive(BubbleGraph, spriteBatch);

            DebugHud.CountSpritesDrawn = DrawCalls;
            DebugHud.CountSpritesCulled = NodesCulled;
        }

        private void DrawRecursive(List<Sprite> layer, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                //TODO: Add draw culling
                //BoundingBox nodeBox = node.DrawBounds();
                if (true)
                {
                    DrawCalls++;
                    node.Draw(spriteBatch, Map, Camera);
                    //DrawRecursive(node.Children);
                }
                else
                {
                    NodesCulled++;
                }
            }
        }

        public Virus FindLivingVirus()
        {
            foreach (Sprite sprite in VirusGraph)
            {
                if ((sprite as Virus).VirusMode != Virus.Mode.Dead)
                {
                    return sprite as Virus;
                }
            }

            return null;
        }

        public Vector2 LockToMap(Vector2 pos)
        {
            Vector2 newPos = Vector2.Zero;

            if (pos.X < 0)
            {
                newPos.X = Map.Width + pos.X;
            }
            else
            {
                newPos.X = pos.X % Map.Width;
            }

            if (pos.Y < 0)
            {
                newPos.Y = Map.Height + pos.Y;
            }
            else
            {
                newPos.Y = pos.Y % Map.Height;
            }

            return newPos;
        }
    }
}