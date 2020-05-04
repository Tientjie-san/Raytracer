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
        public Vector3 location;
        public float distance;
        public Intersection(Primitive primitive, Vector3 location, float distance)
        {
            this.primitive = primitive;
            this.location = location;
            this.distance = distance;
        }
    }
}
