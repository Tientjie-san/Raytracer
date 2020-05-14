using OpenTK;

namespace Template
{
    public class Line:Primitive
    {
        public Vector2  start, end;

        public Line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public override Intersection Intersect(Ray ray)
        {
            float x1 = start.X;
            float y1 = start.Y;
            float x2 = end.X;
            float y2 = end.Y;

            float x3 = ray.origin.X;
            float y3 = ray.origin.Y;

            Vector2 locatie = ray.origin + ray.distance * ray.direction;
            float x4 = locatie.X;
            float y4 = locatie.Y;

            float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (denominator == 0)
            {
                return null;
            }
            else
            {
                float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
                float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;
                if (t > 0 && t < 1 && u > 0)
                {
                    Vector2 location = new Vector2(x1 + t*(x2-x1), y1+t*(y2-y1));
                    float distance = Vector2.Distance(ray.origin, location);

                    return new Intersection(this, location, new Vector2(0, 0), distance);
                }
                else { return null; }
            }

        ;
        }
        
    }
}
