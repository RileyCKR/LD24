﻿using System;
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

        public int NodesCulled { get; private set; }

        public SceneGraph()
        {
            this.Graph = new List<Sprite>();
        }

        public void Add(Sprite obj)
        {
            Graph.Add(obj);
        }

        public void Update()
        {
            //Moves the nodes
            UpdateRecursive(Graph);

            ProcessCollisionsFirstLoop(Graph);
        }

        private void UpdateRecursive(IList<Sprite> layer)
        {
            //TODO: Items can be added to this list while the enumeration is happening,
            //should a buffer be implemented?
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                node.Update();

                //UpdateRecursive(node.Children);
            }
        }

        private void ProcessCollisionsFirstLoop(IList<Sprite> layer)
        {
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];

                ProcessCollisionsRecursive(Graph, node);
            }
        }

        private void ProcessCollisionsRecursive(IList<Sprite> layer, Sprite caller)
        {
            for (int x = 0; x < layer.Count; x++)
            {
                Sprite node = layer[x];
                //Don't check for collisions with the caller
                if (node != caller)
                {
                    if (node.CollisionBox.Intersects(caller.CollisionBox))
                    {
                        caller.OnCollision(node);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            NodesCulled = 0;

            DrawRecursive(Graph, spriteBatch);
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
                    node.Draw(spriteBatch);
                    //DrawRecursive(node.Children);
                }
                else
                {
                    NodesCulled++;
                }
            }
        }
    }
}