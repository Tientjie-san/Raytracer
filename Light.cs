using System;
using OpenTK;

namespace Template
{
    public class Light
    {
        public Vector3 location;
        public float brightness;
        public Vector3 color;

        public Light(Vector3 location, Vector3 color, float brightness)
        {
            this.location = location;
            this.color = color;
            this.brightness = brightness;
        }

        public Vector3 ComputeColor(Vector3 objectcolor, Vector3 normal, Vector3 Lightdirection, float distance)
        {
            float attenuation = 1 - (1 / (distance * distance * 2));

            return objectcolor * this.color * attenuation * this.brightness * Vector3.Dot(normal, Lightdirection);

        }
    }
}

