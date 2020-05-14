using System;
using System.Collections.Generic;
using OpenTK;

namespace Template
{
    public class Scene
    {
        public List<Primitive> primitives;
        public List<Light> light_sources;

        public Scene()
        {
            light_sources = new List<Light>();
            primitives = new List<Primitive>();
        }

        public void Add_square(Vector2 centre, float lengte)
        {
            float off = lengte / 2f;

            primitives.Add(new Line(new Vector2(centre.X-off,centre.Y + off), new Vector2(centre.X + off, centre.Y + off)));
            primitives.Add(new Line(new Vector2(centre.X - off, centre.Y - off), new Vector2(centre.X + off, centre.Y -off)));
            primitives.Add(new Line(new Vector2(centre.X -off, centre.Y + off), new Vector2(centre.X - off, centre.Y - off)));
            primitives.Add(new Line(new Vector2(centre.X +off, centre.Y ), new Vector2(centre.X + off, centre.Y- off)));

        }

        public void Add_primitive(Primitive primitive)
        {
            primitives.Add(primitive);
        }

        public void Add_light(Light light)
        {
            light_sources.Add(light);
        }

        public bool Occluded(Ray shadowray)
        {
            foreach (Primitive primitive in primitives)
            {
                if (primitive.Intersect(shadowray) != null)
                {
                    return true;
                }
            }
            return false;
        }
        public Intersection getNearestIntersection(Ray ray)
        {
            float minDistance = ray.distance;
            Intersection intersection = null;
            Intersection closest = null;
            foreach (Primitive primitive in primitives)
            {
                intersection = primitive.Intersect(ray);
                if (intersection != null && intersection.distance < minDistance)
                {
                    minDistance = intersection.distance;
                    closest = intersection;
                }
            }
            return closest;

        }


    }
}
