using System;
using OpenTK;

namespace Template
{
    public class Line : Primitive
    {
        public Vector2 start, end;

        public Line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }


        // checkt of q op line pr ligt
        static bool onSegment(Vector2 p, Vector2 q, Vector2 r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }


        // 0 --> p, q en r zijn colinear 
        // 1 --> met de klok mee
        // 2 --> tegen de klok in
        static int orientation(Vector2 p, Vector2 q, Vector2 r)
        {
            float v = (q.Y - p.Y) * (r.X - q.X) -(q.X - p.X) * (r.Y - q.Y);
            if (v == 0) return 0;

            return (v > 0) ? 1 : 2;
        }


        public override bool Intersect(Ray ray)
        {
            Vector2 p1 = ray.origin, p2 = start; 
            Vector2 q1 = ray.origin + ray.distance * ray.direction, q2 = end;

            int a1 = orientation(p1, q1, p2);
            int a2 = orientation(p1, q1, q2);
            int a3 = orientation(p2, q2, p1);
            int a4 = orientation(p2, q2, q1);
 
            if (a1 != a2 && a3 != a4)
                return true;

            if (a1 == 0 && onSegment(p1, p2, q1)) return true;
            if (a2 == 0 && onSegment(p1, q2, q1)) return true;
            if (a3 == 0 && onSegment(p2, p1, q2)) return true;
            if (a4 == 0 && onSegment(p2, q1, q2)) return true;

            return false;
        }
    }
}

