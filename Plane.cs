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
            //if the ray is not parralel to the plane, calculate the intersection point
            float dotNormalRay = Vector3.Dot(this.normal, ray.direction);
            if (dotNormalRay < 0)
            {
                Vector3 locatie = ray.origin + (-(Vector3.Dot(ray.origin, this.normal) + this.position.Length) / dotNormalRay) * ray.direction;
                return new Intersection(this, locatie, this.normal,  (locatie - ray.origin).Length);
            }
            else 
            { 
                return null; 
            }
        }
    }
}
