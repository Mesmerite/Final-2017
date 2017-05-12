using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Item {

	[XmlAttribute("type")]
	public int type;

	[XmlElement("posX")]
	public float posX;

	[XmlElement("posY")]
	public float posY;

}

