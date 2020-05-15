using OpenTK;

namespace Template
{
    public abstract class Primitive
    {
        public Vector2 position;
        public abstract bool Intersect(Ray ray);
    }

}
