using UnityEngine;
using System.Collections;

public class PathFinding : MonoBehaviour {

	public Vector2 start;
	public Vector2 end;
	public double terrainHeight;
	public double rayHeightOffset;
	public int numOfNodes;
	public float gizmoCubeSize;
	public bool drawGizmos;
	public int[,] map = new int[500,500];
	public float graphUpdateFrequency;
	private double cellWidth;
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		drawGizmos = false;
		end -= start;
		start = Vector2.zero;
		Debug.DrawRay(Vector3.zero, Vector3.up, Color.green);
		cellWidth = end.x / numOfNodes;
		Debug.Log(cellWidth);
	}
	
	// Update is called once per frame
	void Update () {
		if (graphUpdateFrequency > 0 && Time.time - startTime > graphUpdateFrequency)
		{
			MapScanning();
			startTime = Time.time;
		}
	}

	void OnDrawGizmos()
	{
		if (drawGizmos)
			for (int i = 0; i < numOfNodes; i++)
			{
				for (int j = 0; j < numOfNodes; j++)
				{
					Vector3 currentNodePos = new Vector3((float)(cellWidth * i), (float)(terrainHeight), (float)(cellWidth * j));
					if (map[i,j] == 1)
						Gizmos.color = Color.green;
					else
						Gizmos.color = Color.red;
					Gizmos.DrawCube(currentNodePos, new Vector3(gizmoCubeSize,gizmoCubeSize,gizmoCubeSize));
				}
			}
	}
	
	void AStar(Vector2 start, Vector2 goal)
	{

	}

	void MapScanning()
	{
		for (int i = 0; i < numOfNodes; i++)
		{
			for (int j = 0; j < numOfNodes; j++)
			{
				map[i,j] = 0;
			}
		}
		Vector2 currentNode = Vector2.zero;
		for (int i = 0; i < numOfNodes; i++)
		{
			for (int j = 0; j < numOfNodes; j++)
			{
				RaycastHit hit;
				Vector3 currentNodePos = new Vector3((float)(cellWidth * i), (float)(terrainHeight + rayHeightOffset), (float)(cellWidth * j));
				Ray ray = new Ray(currentNodePos, Vector3.down);/*
				Debug.DrawRay(currentNodePos, Vector3.down);
				Debug.Log(currentNodePos);*/
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "Terrain")
					{
						//Debug.Log(i);
						map[i,j] = 1;
					}
				}
			}
		}
	}
}
