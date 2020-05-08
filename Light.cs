using System;
using OpenTK;

namespace Template
{
    public class Light
    {
        public Vector3 location;
        public float brightness;
        public Vector3 color;
        public float ambientStrength;

        public Light(Vector3 location, Vector3 color, float ambientStrength)
        {
            this.location = location;
            this.color = color;
            this.ambientStrength = ambientStrength;
            this.brightness = 5;
        }

        public Vector3 ComputeColor(Vector3 objectcolor, Vector3 normal, Vector3 Lightdirection)
        {
            Vector3 ambient = ambientStrength * color;
            float diff = Math.Max(Vector3.Dot(normal, Lightdirection), 0);
            Vector3 diffuse = diff * color;
            return (ambient +diffuse) * brightness * objectcolor;
        }
    }
}
