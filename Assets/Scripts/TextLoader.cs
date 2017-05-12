using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextLoader : MonoBehaviour {

	protected string path;
	protected List<string> strings = new List<string>();
	protected Text textField;
	protected float messageTime = 0;
	protected int nextMessage = 0;

	public bool logText = false;

	void Start () {
		path = "XMLScript";
		TextContainer ic = TextContainer.Load(path);
		textField = GetComponent<Text>();
		foreach (Line line in ic.lines)
		{
			strings.Add(line.name);
		}
	}

	void Update() {
		if (nextMessage < strings.Count) {
			if (Time.time >= messageTime) {
				textField.text = strings [nextMessage];
				messageTime = Time.time + 5f;
				nextMessage++;
			}
		} else if (nextMessage == strings.Count && Time.time >= messageTime) {
			SceneManager.LoadScene (1);
		}
	}	

}
