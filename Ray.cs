using System;
using OpenTK;


namespace Template
{
    public class Ray
    {
        public Vector2 origin, direction;
        public float distance;
        public Ray(Vector2 o, Vector2 d, float t)
        {
            origin = o;
            direction = d.Normalized();
            distance = t;
        }


    }
}
