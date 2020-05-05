using System;
using OpenTK;


namespace Template
{
    public class Sphere : Primitive
    {
        public float radius;
        public float r2;
        public Vector3  center;

        public Sphere(Vector3 position ,float r, Vector3 c)
        {
            center = position;
            radius = r;
            r2 = r * r;
            color = c;
            
            
        }

        // werkt alleen voor sphere die licht alleen weerkaatst dus niet voor een glazen bol.
        public override Intersection Intersect(Ray ray)
        {
          
            Vector3 c = this.center - ray.origin; 
            float distance = Vector3.Dot(c, ray.direction); 
            Vector3 q = c - distance * ray.direction; 
            float p2 = Vector3.Dot(q, q);
            if (p2 > this.r2) 
            { 
                return null; 
            }
            distance -= (float)Math.Sqrt(this.r2 - p2);
            // als de afstand kleiner is dan de primtive ray en groter is dan 0
            if ((distance < ray.distance) && (distance > 0))
            {
                Vector3 locatie = ray.origin + distance * ray.direction;
                // normal at location of intersection
                Vector3 normal = (locatie - position).Normalized();
                return new Intersection(this, locatie, normal, distance);
            }
            else return null;
                
        }
    }
}

