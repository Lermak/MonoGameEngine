using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Quadtree
    {
        const int NUM_COLLIDERS = 10;
        const int NUM_CHILDREN = 4;

        public Collider[] Colliders;
        public Rectangle Area;

        public Quadtree Parent;
        public Quadtree[] Children;

        struct Rect
        {
            Vector2 Position;
            Vector2 Size;

            public Rect(Vector2 p, Vector2 s)
            {
                Position = p;
                Size = s;
            }

            public bool Intersections(Rect r)
            {
                return false;
            }
        }

        public Quadtree(Rectangle area, Quadtree parent)
        {
            Colliders = new Collider[NUM_COLLIDERS];
            Area = area;
            Parent = parent;
            Children = new Quadtree[NUM_CHILDREN];
        }

        public List<Quadtree> GetQuads(Rectangle r)
        {
            List<Quadtree> q = new List<Quadtree>();
            if(Colliders != null)
            {
                if (Area.Intersects(r))
                    q.Add(this);
            }
            else
            {
                for(int i = 0; i < NUM_CHILDREN; ++i)
                {
                    q.AddRange(Children[i].GetQuads(r));
                }
            }

            return q;                
        }

        public List<Collider> GetColliders()
        {
            List<Collider> colliders = new List<Collider>();
            if (Colliders != null)
            {
                for (int i = 0; i < NUM_COLLIDERS; ++i)
                {
                    if(Colliders[i] != null)
                        colliders.Add(Colliders[i]);
                }
            }
            else
            {
                for(int i = 0; i < NUM_CHILDREN; ++i)
                {
                    foreach (Collider co in Children[i].GetColliders())
                    {
                        if (!colliders.Contains(co))
                            colliders.Add(co);
                    }
                }
            }

            return colliders;
        }

        public void Insert(Collider c)
        {
            Rectangle r = new Rectangle(new Point((int)(c.Transform.Position.X - c.Transform.Width / 2), (int)(c.Transform.Position.Y - c.Transform.Height / 2)), new Point((int)c.Transform.Width, (int)c.Transform.Height));

            if (Area.Intersects(r))
            {
                if (Colliders != null)
                {
                    for (int i = 0; i < NUM_COLLIDERS; ++i)
                    {
                        if (Colliders[i] == null)
                        {
                            Colliders[i] = c;
                            return;
                        }
                    }

                    Children[0] = new Quadtree(new Rectangle(Area.X, Area.Y, Area.Width / 2, Area.Height / 2), this);
                    Children[1] = new Quadtree(new Rectangle(Area.X + Area.Width / 2, Area.Y, Area.Width / 2, Area.Height / 2), this);
                    Children[2] = new Quadtree(new Rectangle(Area.X, Area.Y + Area.Height / 2, Area.Width / 2, Area.Height / 2), this);
                    Children[3] = new Quadtree(new Rectangle(Area.X + Area.Width / 2, Area.Y + Area.Height / 2, Area.Width / 2, Area.Height / 2), this);

                    for (int i = 0; i < NUM_COLLIDERS; ++i)
                    {
                        Rectangle r1 = new Rectangle(new Point((int)(Colliders[i].Transform.Position.X - Colliders[i].Transform.Width / 2), (int)(Colliders[i].Transform.Position.Y - Colliders[i].Transform.Height / 2)), new Point((int)Colliders[i].Transform.Width, (int)Colliders[i].Transform.Height));

                        for (int x = 0; x < NUM_CHILDREN; ++x)
                        {
                            if (Children[x].Area.Intersects(r1))
                                for (int z = 0; z < NUM_COLLIDERS; ++z)
                                    if (Children[x].Colliders[z] == null)
                                    {
                                        Children[x].Colliders[z] = Colliders[i];
                                        break;
                                    }
                        }
                    }

                    Colliders = null;
                }
                for (int i = 0; i < NUM_CHILDREN; ++i)
                {
                    Children[i].Insert(c);
                }
            }
        }
    }
}
