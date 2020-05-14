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

            scene.Add_primitive(new Sphere(new Vector2(0.3f, 0f), 0.1f));
            scene.Add_primitive(new Sphere(new Vector2(0f, 0), 0.1f));
            scene.Add_primitive(new Sphere(new Vector2(-0.5f, 0.5f), 0.1f));
            //scene.Add_primitive(new Line(new Vector2(0.5f, 0.5f), new Vector2( 0.5f, -0.5f)));
            //scene.Add_square(new Vector2(0, 0), 0.2f);
            scene.Add_light(new Light(new Vector2(-0.5f, -0.5f), blue, 2f));
            scene.Add_light(new Light(new Vector2(0.5f, 0.5f), yellow, 2f));
            scene.Add_light(new Light(new Vector2(0.5f, -0.5f), red, 2f));
            scene.Add_light(new Light(new Vector2(-0.5f, 0.5f), green, 2f));

        }
        // tick: renders one frame
        public void Tick()
        {
            screen.Clear( 0 );

            // voor ieder pixel schiet een primary ray. 
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

            /*Intersection nearest_intersection = scene.getNearestIntersection(primaryray);
            // als de nearest intersectie null is betekent het dat er geen primitive geraakt is dus worrdt er zwarte pixel geplot
            if (nearest_intersection == null)
            {
                return color;
            }*/
            // schiet een shadowray naar de licht sources. vanaf de locatie van de intersectie. 
            foreach (Light light in scene.light_sources)
            {
                Vector2 direction = (light.location - pixelcoordinate).Normalized();
                float distance = Vector2.Distance(pixelcoordinate, light.location);
                Ray ray = new Ray(pixelcoordinate, direction, distance); 

                bool occluded = scene.Occluded(ray);

                if (!occluded)
                {
                    // als brightness groter is dan is de attenuation groter, als afstand groter wordt dan is de energie lager
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
