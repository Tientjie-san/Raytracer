using OpenTK;

namespace Template
{
    public abstract class Primitive
    {
        public Vector3 position;
        public float color;
        public abstract Intersection Intersect(Ray ray);
    }

}
