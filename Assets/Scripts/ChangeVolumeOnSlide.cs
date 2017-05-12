using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeVolumeOnSlide : MonoBehaviour {

	private Slider slider;

	void Start () {
		slider = gameObject.GetComponent<Slider> ();
	}

	public void ChangeVolume(int type) {
		if (type == 0) {
			Debug.Log ("Master Volume set to " + slider.value);
			PlayerPrefs.SetFloat ("Master Volume", slider.value);
		} 
		else if (type == 1) {
			Debug.Log ("Music Volume set to " + slider.value);
			PlayerPrefs.SetFloat ("Music Volume", slider.value);
		}
		else if (type == 2) {
			Debug.Log ("SFX Volume set to " + slider.value);
			PlayerPrefs.SetFloat ("SFX Volume", slider.value);
		}
	}
}
