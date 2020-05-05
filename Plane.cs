using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Template
{
    public class Plane : Primitive
    {
        public Vector3 normal;
        public Plane(Vector3 position, Vector3 normal, Vector3 color)
        {
            this.position = position;
            this.normal = normal;
            this.color = color;
        }
        public override Intersection Intersect(Ray ray)
        {
            return null;
        }
    }
}
