using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {
	
	public int MinDetails;
	public int MaxDetails;
	public int PrototypeIndex = 0;
	public bool TestHeightMode = false;
	public Camera cam;
	public bool PlaceOnlyOverLevel = false;
	public bool PlaceOnlyUnderLevel = false;
	public float Level = 0.0f;
	
	void Update()
	{
		if (TestHeightMode && Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject.name == "Terrain")
				{
					Debug.Log(GetComponent<Terrain>().SampleHeight(hit.point));
				}
			}
		}
	}
	
	[ContextMenu("PLACE GRASS")]
	void AddGrass()
	{
		Terrain terrain = GetComponent<Terrain>();
		TerrainData data = terrain.terrainData;         
		if (data.detailPrototypes.Length == 0)
		{
			Debug.Log("ADD GRASS PROTOTYPE " + PrototypeIndex + " !");      
		}
		terrain.detailObjectDensity = 0.01f;
		terrain.detailObjectDistance = 50.0f;
		int[,] ta = new int[data.detailWidth, data.detailHeight];
		Debug.Log(data.detailWidth + " " + data.detailHeight);
		for (int x = 0; x < ta.GetLength(0); x++)
			for (int z = 0; z < ta.GetLength(1); z++)
		{                                                                                       
			if (PlaceOnlyOverLevel) 
			{
				if(terrain.SampleHeight(new Vector3(x, 0.0f, z)) >= Level)
					ta[x, z] = Random.Range(MinDetails, MaxDetails);
				if(terrain.SampleHeight(new Vector3(x, 0.0f, z)) < Level)
					ta[x, z] = 0;
			}
			
			if (PlaceOnlyUnderLevel) 
			{
				if(terrain.SampleHeight(new Vector3(x, 0.0f, z)) <= Level)
					ta[x, z] = Random.Range(MinDetails, MaxDetails);
				if(terrain.SampleHeight(new Vector3(x, 0.0f, z)) > Level)
					ta[x, z] = 0;
			}
			
			if (!PlaceOnlyOverLevel && !PlaceOnlyUnderLevel) 
			{
				ta[x, z] = Random.Range(MinDetails, MaxDetails);
			}
		}
		
		data.SetDetailLayer(0, 0, PrototypeIndex, ta);
		
		//TerrainData.SetDetailLayer(начальная позиция X, начальная позиция Y,
		//                          номер прототипа, количество деталей в клетке);
	}
	
	[ContextMenu("DELETE GRASS")]
	void DeleteGrass()
	{
		Terrain terrain = GetComponent<Terrain>();
		TerrainData data = terrain.terrainData;         
		if (data.detailPrototypes.Length == 0)
		{
			Debug.Log("ADD GRASS PROTOTYPE " + PrototypeIndex + " !");      
		}
		int[,] ta = new int[data.detailWidth, data.detailHeight];
		for (int x = 0; x < ta.GetLength(0); x++)
			for (int z = 0; z < ta.GetLength(1); z++)
		{                       
			ta[x, z] = 0;
		}
		
		data.SetDetailLayer(0, 0, PrototypeIndex, ta);
	}
	
}