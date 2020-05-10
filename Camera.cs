using System;
using System.Diagnostics;
using OpenTK;
namespace Template
{
    public class Camera
    {
        public Vector3 postion;

        public Camera(Vector3 pos)
        {
            postion = pos;
        }

        public Vector3 Trace(Ray primaryray, Scene scene, Vector3 color)
        {

			Intersection nearest_intersection = scene.getNearestIntersection(primaryray);
			// als de nearest intersectie null is betekent het dat er geen primitive geraakt is dus worrdt er zwarte pixel geplot
			if (nearest_intersection == null)
				{
					return color;
				}
			// schiet een shadowray naar de licht sources. vanaf de locatie van de intersectie. 
			foreach (Light light in scene.light_sources)
			{
				
					// shadowray maken, origin moet een offset hebben x * direction, distance moet 2 * de afstand van de offset
					// ingekort worden. 
					Vector3 origin = nearest_intersection.location;
					Vector3 direction = (light.location - origin).Normalized();
					float distance = Vector3.Distance(origin, light.location);
					Vector3 offset = 0.005f * direction;
					float distance_offset = 2 * Vector3.Distance(origin, origin + offset);
					Ray shadowray = new Ray(origin + offset, direction, distance - distance_offset);

					// kijk of er een occluder is.
					bool occluded = scene.Occluded(shadowray);
					if (!occluded)
					{
						color += light.ComputeColor(nearest_intersection.primitive.color, nearest_intersection.normal, direction,
							distance);
					}
			}

			return color;
		}
	}
}


