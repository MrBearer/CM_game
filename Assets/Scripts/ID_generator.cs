using UnityEngine;
using System.Collections;

public class ID_generator : MonoBehaviour {

	private static int id;

	// Use this for initialization
	void Start () {
		id = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int generate_ID()
	{
		return id++;
	}
}
