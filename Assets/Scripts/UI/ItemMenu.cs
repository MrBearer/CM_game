using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

    public GameObject pivot;
    public GameObject item;
    public Texture2D[] thumbs;

	// Use this for initialization
	void Start () {
        Building bldScript = (Building)pivot.GetComponent<Building>();
        GameObject[] buildings = bldScript.GetBuildings();
        int i = 0;
        foreach (GameObject bld in buildings)
        {
            GameObject newItem = (GameObject)Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
            newItem.transform.parent = transform;
            Text text = (Text)newItem.GetComponentInChildren<Text>();
            text.text = bld.name;
            GameObject thumbGO = (GameObject)GameObject.FindGameObjectWithTag("thumb");
            thumbGO.tag = "Untagged";
            Image thumb = (Image)thumbGO.GetComponentInChildren<Image>();
            thumb.sprite = Sprite.Create(thumbs[i], new Rect(0, 0, 256, 256), new Vector2(0, 0));
            ButtonID b_id = (ButtonID)newItem.GetComponent<ButtonID>();
            b_id.id = i;
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
