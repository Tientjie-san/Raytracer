using System;
using OpenTK;


namespace Template
{
    public struct Ray
    {
        public Vector3 origin, direction;
        public float distance;
        public Ray(Vector3 o, Vector3 d, float t)
        {
            origin = o;
            direction = d.Normalized();
            distance = t;
        }


    }
}
