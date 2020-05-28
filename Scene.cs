using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.ES11;

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

        public void Add_square(Vector2 centre, float lengte, bool boven = true, bool onder = true, bool links = true, bool rechts = true)
        {

            float off = lengte / 2f;
            if (boven)
            {
                // boven lijn
                primitives.Add(new Line(new Vector2(centre.X - off, centre.Y + off), new Vector2(centre.X + off, centre.Y + off)));
            }
            if (onder)
            {
                // onderlijn
                primitives.Add(new Line(new Vector2(centre.X - off, centre.Y - off), new Vector2(centre.X + off, centre.Y - off)));
            }
            if (links)
            {
                //linkerlijn
                primitives.Add(new Line(new Vector2(centre.X - off, centre.Y + off), new Vector2(centre.X - off, centre.Y - off)));
            }
            if (rechts)
            {
                // rechter lijn
                primitives.Add(new Line(new Vector2(centre.X + off, centre.Y + off), new Vector2(centre.X + off, centre.Y - off)));
            }
        }



        public void Add_sphere(Vector2 position, float radius)
        {
            primitives.Add(new Sphere(position, radius));
        }

        public void Add_line(Vector2 start, Vector2 end)
        {
            primitives.Add(new Line(start, end));
        }

        public void Add_light(Vector2 position, Vector3 color, float brightness)
        {
            light_sources.Add(new Light(position, color, brightness));
        }

        public void Add_Arealight(float center, Vector3 color)
        {
            for(float i = 0; i < 6; i++)
            {
                Add_light(new Vector2((center-0.15f+(0.05f*i)), 0.4f), color, 4);
            }

            //--> make the arguments of this method specify the perimeters of the area light region
            //--> then actaul create a square light: just a set of 
            //--> and set the random perimeters to what is given in these arguments
            //--> and allow the random values to be changed every time the trace function is called
        }

        public bool Occluded(Ray shadowray)
        {
            foreach (Primitive primitive in primitives)
            {
                if (primitive.Intersect(shadowray))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
