using System;
using OpenTK;
namespace Template
{
    class MyApplication
    {
        // member variables
        public Surface screen;
        public Scene scene = new Scene();
        // initialize
        public void Init()
        {
            //scene vullen
            Vector3 yellow = new Vector3(1, 1, 0f);
            Vector3 red = new Vector3(1, 0f, 0f);
            Vector3 gray = new Vector3(0.5f, 0.5f, 0.5f);
            Vector3 green = new Vector3(0f, 1, 0f);
            Vector3 blue = new Vector3(0, 0, 1);
            Vector3 white = new Vector3(1, 1, 1);
            Vector3 orange = new Vector3(1, 0.5f, 0.1f);
            Vector3 pink = new Vector3(1, 0.75f, 0.8f);

            scene.Add_sphere(new Vector2(0f, 0.4f), 0.1f);
            scene.Add_sphere(new Vector2(0f, -0.4f), 0.1f);
            scene.Add_sphere(new Vector2(0.4f, 0f), 0.1f);
            scene.Add_sphere(new Vector2(-0.4f, 0f), 0.1f);


            scene.Add_square(new Vector2(-0.7f, -0.4f), 0.2f, rechts : false);
            scene.Add_square(new Vector2(-0.7f, 0.4f), 0.2f, onder: false);
            scene.Add_square(new Vector2(0.7f, -0.4f), 0.2f, boven: false);
            scene.Add_square(new Vector2(0.7f, 0.4f), 0.2f, links: false);

            scene.Add_light(new Vector2(-0.7f, 0.4f), green, 6);
            scene.Add_light(new Vector2(-0.7f, -0.4f), blue, 6);
            scene.Add_light(new Vector2(0.7f, 0.4f), yellow, 6);
            scene.Add_light(new Vector2(0.7f, -0.4f), red, 6);
            scene.Add_light(new Vector2(0f, 0f), white, 6);


        }
        // tick: renders one frame
        public void Tick()
        {
            screen.Clear( 0 );

            for (int x = 0; x < screen.width; x++)
                for (int y = 0; y < screen.height; y++)
                {
                    Vector3 color = new Vector3(0, 0, 0);
                    color = Trace(ToWorldCoordinate(x,y), scene, color);
                    screen.Plot(x, y, ConvertColor(color));
                }
        }

        public Vector3 Trace(Vector2 pixelcoordinate, Scene scene, Vector3 color)
        {

            foreach (Light light in scene.light_sources)
            {
                Vector2 direction = (light.location - pixelcoordinate).Normalized();
                float distance = Vector2.Distance(pixelcoordinate, light.location);
                Ray ray = new Ray(pixelcoordinate, direction, distance); 
                bool occluded = scene.Occluded(ray);

                if (!occluded)
                {
                    // als brightness groter is dan is de attenuation groter, als afstand groter wordt dan is de attenuation lager
                    float attenuation = (light.brightness / (distance*60));
                    color += light.color * attenuation;
                }
            }
            return color;
        }
        int ConvertColor(Vector3 rgb)
        {
            rgb = rgb * 256.0f;
            for (int i = 0; i < 3; i++)
            {
                if (rgb[i] < 0)
                {
                    rgb[i] = 0;
                }
                else if (rgb[i] > 255)
                {
                    rgb[i] = 255;
                }
            }
            return MixColor((int)rgb[0], (int)rgb[1], (int)rgb[2]);
        }


        // vertaalt pixel coordinaten naar world space coordinaten 

        Vector2 ToWorldCoordinate(int x, int y)
        {
            float worldX;
            float worldY;
            if (screen.width < screen.height)
            {
                worldX = (x - screen.width / 2f) / (screen.width / 2f) / screen.height/screen.width;
                worldY = (-1 * y + screen.height / 2f) / (screen.height / 2f);
                return new Vector2(worldX, worldY);
            }
            else if (screen.width > screen.height)
            {
                worldX = (x - screen.width / 2f) / (screen.width / 2f);
                worldY = (-1 * y + screen.height / 2f) / (screen.height / 2f) / ((float)screen.width / screen.height);
                return new Vector2(worldX, worldY);
            }
            else // CHECK
            {
                worldX = (x - screen.width / 2f) / (screen.width / 2f);
                worldY = (-1 * y + screen.height / 2f) / (screen.height / 2f);
                return new Vector2(worldX, worldY);
            }
            
            
        }
        // kleur in decimal code
        int MixColor(int red, int green, int blue)
        {
            return (red << 16) + (green << 8) + blue;
        }

    }

    
}
