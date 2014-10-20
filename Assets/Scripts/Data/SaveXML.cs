using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

public class SaveXML : MonoBehaviour {

	private static Dictionary<int,GameObject> prefabs = new Dictionary<int,GameObject>();
	private static Dictionary<int,Vector3> outBsPos = new Dictionary<int,Vector3>();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.S))
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode rootNode = xmlDoc.CreateElement("ObjectData");
			xmlDoc.AppendChild(rootNode);
			
			XmlNode outB;

			foreach (var item in outBsPos)
			{
				XmlAttribute attribute;
				xmlDoc.AppendChild(rootNode);

				outB = xmlDoc.CreateElement (StringToVector3.makeValidName(prefabs[item.Key].name));
				outB.InnerText = item.Value.ToString();

				attribute = xmlDoc.CreateAttribute("Type");
				attribute.Value = "outB";
				outB.Attributes.Append(attribute);
				attribute = xmlDoc.CreateAttribute("Id");
				attribute.Value = item.Key.ToString();
				outB.Attributes.Append(attribute);

				rootNode.AppendChild(outB);
			}

			xmlDoc.Save("Save.xml");
		}

		if (Input.GetKeyUp (KeyCode.L)) 
		{
			XmlTextReader reader = new XmlTextReader("Save.xml");
			while (reader.Read())
			{
				if(!reader.IsEmptyElement && reader.GetAttribute("Type") == "outB")
				{
					Debug.Log(StringToVector3.string2int(reader.GetAttribute("Id")));
					GameObject ghost = (GameObject)Instantiate(prefabs[StringToVector3.string2int(reader.GetAttribute("Id"))], StringToVector3.string2vector3(reader.ReadString()),Quaternion.identity);
				}
			}
		}
	}

	public static void save(string type, int id, Vector3 pos, GameObject g_object)
	{
		if (type == "outB") 
		{
			outBsPos.Add(id,pos);
			prefabs.Add(id,g_object);
		}
	}
}
