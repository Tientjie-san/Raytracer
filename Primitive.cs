using OpenTK;

namespace Template
{
    public abstract class Primitive
    {
        public Vector3 position;
        public Vector3 color;
        public abstract Intersection Intersect(Ray ray);
    }

}
