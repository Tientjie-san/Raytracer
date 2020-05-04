using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Template
{
	class MyApplication
	{
		// member variables
		public Surface screen; Surface map;
		float[,] h;
		public Camera camera = new Camera(new Vector3(0, 0, -1));
		public Scene scene = new Scene();



		// initialize
		public void Init()
		{
			// tutorial coin
			map = new Surface("../../assets/coin.png");
			h = new float[256, 256];
			for (int y = 0; y < 256; y++)
			{
				for (int x = 0; x < 256; x++)
				{
					h[x, y] = ((float)(map.pixels[x + y * 256] & 255)) / 256;
				}
			}
			//scene vullen
			scene.Add_primtive(new Sphere(new Vector3(-0.4f, 0.3f, 0), 0.1f, 0xFF0000));

		}


		// tick: renders one frame
		public void Tick()
		{
			screen.Clear(0);
//			for (int x = 0; x < 100; x++) for (int y = 0; y < 100; y++) screen.Plot(x + 200, y + 200, 0xFF0000);

			// voor ieder pixel schiet een primary ray. 
			for (int x = 0; x < screen.width; x++)
				for (int y = 0; y < screen.height; y++)
				{
					int color = 0;
					Ray primaryray = new Ray(camera.postion, ToWorldCoordinate(x, y) - camera.postion, float.MaxValue);
					Intersection nearest_intersection = scene.getNearestIntersection(primaryray);
					// als de nearest intersectie null is betekent het dat er geen primitive geraakt is dus worrdt er zwarte pixel geplot
					if (nearest_intersection == null) 
					{
						screen.Plot(x, y, color);
					}
					else
					{
						screen.Plot(x, y, (int)nearest_intersection.primitive.color);
						/*
						// schiet een shadowray naar de licht sources. vanaf de locatie van de intersectie. 
						foreach(Light light in scene.light_sources)
						{
							
							// shadowray maken, origin moet een offset hebben x * direction, distance moet 2 * de afstand van de offset
							// ingekort worden. 
							Vector3 origin = nearest_intersection.location;
							Vector3 direction = (light.location - origin).Normalized();
							float distance = Vector3.Distance(origin, light.location);
							Vector3 offset = 0.5f *direction;
							float distance_offset = 2 * Vector3.Distance(origin, origin + offset);
							Ray shadowray = new Ray(origin+offset, direction, distance - distance_offset );

							// kijk of er een occluder is.
							bool occluded = scene.Occluded(shadowray);



						}*/
					}
					
						

					// laatste stap
			//		screen.Plot(x, y, color);
				}
			

		}

	

		// vertaalt pixel coordinaten naar world space coordinaten hier is van uitgegaan dat view direction (0,0,1) is. slide 12 raytracing
		// de view direction variable zit in de camera
		Vector3 ToWorldCoordinate(int x,int y)
		{
			float worldX= (x-screen.width/2f) / (screen.width/2f);
			float worldy= (-1 * y + screen.height/2f) / (screen.height/2f) / ((float)screen.width/screen.height);
			return new Vector3(worldX, worldy, 0);
		}
		// kleur in decimal code
		int MixColor(int red, int green, int blue)
		{
			return (red << 16) + (green << 8) + blue;
		}

	

		public void RenderGL()
		{

		}
	}
}
