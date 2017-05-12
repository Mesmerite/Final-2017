using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("TextCollection")]
public class TextContainer {

	[XmlArray("Lines")]
	[XmlArrayItem("Line")]
	public List<Line> lines = new List<Line>();

	public static TextContainer Load(string path) {
		TextAsset _xml = Resources.Load<TextAsset>(path);

		XmlSerializer serializer = new XmlSerializer(typeof(TextContainer));

		StringReader reader = new StringReader(_xml.text);

		TextContainer lines = serializer.Deserialize(reader) as TextContainer;

		return lines;
	}

}
