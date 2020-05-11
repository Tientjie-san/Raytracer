using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Net.Mail;

namespace Template
{
	class MyApplication
	{
		// member variables
		public Surface screen; Surface map;
		float[,] h;
		public Camera camera;
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
			Vector3 yellow = new Vector3(1, 1, 0.2f);
			Vector3 red = new Vector3(1, 0.1f, 0.1f);
			Vector3 gray = new Vector3(0.5f, 0.5f, 0.5f);
			Vector3 green = new Vector3(0.1f, 1, 0.1f);
			Vector3 blue = new Vector3(0.1f, 0.1f, 1);
			Vector3 white = new Vector3(1, 1, 1);
			Vector3 orange = new Vector3(1, 0.5f, 0.1f);


			scene.Add_primitive(new Sphere(new Vector3(0f, 0.75f, 1.5f), 0.25f, yellow));
			scene.Add_primitive(new Sphere(new Vector3(0.7f, 0f, 1.5f), 0.25f, red));
			scene.Add_primitive(new Sphere(new Vector3(-0.7f, 0.7f, 1.5f), 0.25f, gray));
			scene.Add_primitive(new Plane(new Vector3(0, -1, 0), new Vector3(0, 2, 0), orange));




			scene.Add_light(new Light(new Vector3(0, 1, -1f), white, 2f));
			scene.Add_light(new Light(new Vector3(0, 2, -2f), red, 1f));
			//scene.Add_light(new Light(new Vector3(0, 0, 2f), white, 2f));
			//scene.Add_light(new Light(new Vector3(2, 2, 2f), red, 2f));
			// y negatief kijk omhoog etc. 
			camera = new Camera(new Vector3(0, -0.05f, -2));

		}


		// tick: renders one frame
		public void Tick()
		{
			screen.Clear(0);

			// voor ieder pixel schiet een primary ray. 
			for (int x = 0; x < screen.width; x++)
				for (int y = 0; y < screen.height; y++)
				{
					Vector3 color = new Vector3(0, 0, 0);
					Ray primaryray = new Ray(camera.postion, ToWorldCoordinate(x, y) - camera.postion, float.MaxValue);
					color = camera.Trace(primaryray, scene, color);
					screen.Plot(x, y, ConvertColor(color));
				}
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


		// vertaalt pixel coordinaten naar world space coordinaten hier is van uitgegaan dat view direction (0,0,1) is. slide 12 raytracing

		Vector3 ToWorldCoordinate(int x, int y)
		{
			float worldX = (x - screen.width / 2f) / (screen.width / 2f);
			float worldy = (-1 * y + screen.height / 2f) / (screen.height / 2f) / ((float)screen.width / screen.height);
			// verander z om in en uit te zoomen
			return new Vector3(worldX, worldy, 0f);
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