using OpenTK;

namespace Template
{
    public abstract class Primitive
    {
        public Vector2 position;
        public abstract Intersection Intersect(Ray ray);
    }

}
