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
        /*
        public Vector3 ComputeColor(Vector3 objectcolor, Vector2 normal, Vector2 Lightdirection, float distance)
        {
            float attenuation = 1 - (1 / (distance * distance * 2));
            // hoe groter de afstand van het licht hoe zwakker de energie is, en de dot(normal, lightdirection) geeft aan waar het licht tegenover de intersectie is.
            return objectcolor * this.color * attenuation * this.brightness * Vector2.Dot(normal, Lightdirection);

        }*/
    }
}

