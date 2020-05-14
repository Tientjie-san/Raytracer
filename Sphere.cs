using System;
using OpenTK;


namespace Template
{
    public class Sphere : Primitive
    {
        public float r2;
        public Sphere(Vector2 position, float r)
        {
            this.position = position;
            r2 = r * r;


        }

        // werkt alleen voor sphere die licht alleen weerkaatst dus niet voor een glazen bol.
        public override Intersection Intersect(Ray ray)
        {

            Vector2 c = this.position - ray.origin;
            float distance = Vector2.Dot(c, ray.direction);
            Vector2 q = c - distance * ray.direction;
            float p2 = Vector2.Dot(q, q);
            if (p2 > this.r2)
            {
                return null;
            }
            distance -= (float)Math.Sqrt(this.r2 - p2);
            // als de afstand kleiner is dan de primtive ray en groter is dan 0
            if ((distance < ray.distance) && (distance > 0))
            {
                Vector2 locatie = ray.origin + distance * ray.direction;
                // normal at location of intersection
                Vector2 normal = (locatie - this.position).Normalized();
                return new Intersection(this, locatie, normal, distance);
            }
            else return null;

        }
    }
}

