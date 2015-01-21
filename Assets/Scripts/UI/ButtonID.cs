using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonID : MonoBehaviour {

    public int id;
    public GameObject modeMenu;

	// Use this for initialization
    void Start()
    {
        if (id >= -1)
            GetComponent<Button>().onClick.AddListener(() => Select());
        else
            GetComponent<Button>().onClick.AddListener(() => Mode());
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void Select()
    {
        GameObject.FindGameObjectWithTag("CameraPivot").GetComponent<Building>().Select(id);
    }

    void Mode()
    {
        if (id == -2)
        {
            modeMenu.GetComponent<Canvas>().enabled = !modeMenu.GetComponent<Canvas>().enabled;
        }
    }
}
