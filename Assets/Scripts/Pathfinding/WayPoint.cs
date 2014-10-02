using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WayPoint
{
		public Vector3 position;
		public List<WayPoint> neighbors = null;
		public WayPoint parent = null;
		
		public float F = 0;
		public float H = 0;
		public float G = 0;
		public int ID = 0;

		public WayPoint ()
		{

		}

		public WayPoint(Vector3 p, int id, WayPoint wpParent = null, List<WayPoint> n = null, float f = 0, float g = 0, float h = 0)
		{
			position = p;
			ID = id;
			parent = wpParent;
			neighbors = n;
			F = f;
			G = g;
			H = h;
		}
}

