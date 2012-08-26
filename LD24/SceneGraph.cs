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
        private List<Sprite> Graph { get; set; }

        int NodesCulled;
        int DrawCalls;
        int CollisionChecks;
        int virusCount;
        int cellCount;
        int tcellCount;
        int antigenCount;

        public Rectangle Map;
        public Camera Camera;

        public SceneGraph(Rectangle map, Camera camera)
        {
            this.Graph = new List<Sprite>();
            this.Map = map;
            this.Camera = camera;
        }

        public void Add(Sprite obj)
        {
            Graph.Add(obj);
        }

        public void Update()
        {
            virusCount = 0;
            cellCount = 0;
            tcellCount = 0;
            antigenCount = 0;

            UpdateRecursive(Graph);

            DebugHud.CountVirus = virusCount;
            DebugHud.CountCell = cellCount;
            DebugHud.CountTCell = tcellCount;
            DebugHud.CountAntigen = antigenCount;

            ProcessCollisionsFirstLoop(Graph);
        }

        private void UpdateRecursive(IList<Sprite> layer)
        {
            //TODO: Items can be added to this list while the enumeration is happening,
            //should a buffer be implemented?
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                node.Update(this);

                switch (node.Type)
                {
                    case SpriteType.Virus:
                        virusCount++;
                        break;
                    case SpriteType.Cell:
                        cellCount++;
                        break;
                    case SpriteType.TCell:
                        tcellCount++;
                        break;
                    case SpriteType.Antigen:
                        antigenCount++;
                        break;
                }

                node.Position = LockToMap(node.Position);
                
                //UpdateRecursive(node.Children);
            }
        }

        private void ProcessCollisionsFirstLoop(IList<Sprite> layer)
        {
            CollisionChecks = 0;

            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                ProcessCollisionsRecursive(Graph, node);
            }

            DebugHud.CountCollisionChecks = CollisionChecks;
        }

        private void ProcessCollisionsRecursive(IList<Sprite> layer, Sprite caller)
        {
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];
                //Don't check for collisions with the caller
                if (node != caller)
                {
                    if (node.Type == SpriteType.Virus && caller.Type == SpriteType.Virus)
                    {
                        continue;
                    }

                    CollisionChecks++;
                    if (node.CollisionBox.Intersects(caller.CollisionBox))
                    {
                        caller.OnCollision(node);
                        return;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            NodesCulled = 0;
            DrawCalls = 0;

            DrawRecursive(Graph, spriteBatch);

            DebugHud.CountSpritesDrawn = DrawCalls;
            DebugHud.CountSpritesCulled = NodesCulled;
        }

        private void DrawRecursive(IList<Sprite> layer, SpriteBatch spriteBatch)
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
            foreach (Sprite sprite in Graph)
            {
                if (sprite.Type == SpriteType.Virus && (sprite as Virus).VirusMode != Virus.Mode.Dead)
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