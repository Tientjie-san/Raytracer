using System;
using OpenTK;

namespace Template
{
    public class Light
    {
        public Vector2 location;
        public float brightness;
        public Vector3 color;

        public Light(Vector2 location, Vector3 color, float brightness)
        {
            this.location = location;
            this.color = color;
            this.brightness = brightness;
        }
    }
}

