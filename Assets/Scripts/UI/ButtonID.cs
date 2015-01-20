using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonID : MonoBehaviour {

    public int id;

	// Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Select());
	}
	
	// Update is called once per frame
	void Update () {
	}

    void Select()
    {
        GameObject.FindGameObjectWithTag("CameraPivot").GetComponent<Building>().Select(id);
    }

}
