using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Template
{
    public class Intersection
    {
        public Primitive primitive;
        public Vector2 location;
        public float distance;
        public Vector2 normal;
        public Intersection(Primitive primitive, Vector2 location, Vector2 normal, float distance)
        {
            this.primitive = primitive;
            this.location = location;
            this.distance = distance;
            this.normal = normal;

        }
    }
}
