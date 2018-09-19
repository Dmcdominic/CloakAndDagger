using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_val_display : MonoBehaviour {

	public Slider slider;

	private Text _Text;

	// Use this for initialization
	void Start () {
		_Text = GetComponent<Text>();
		_Text.text = slider.value.ToString();
		slider.onValueChanged.AddListener(update_text);
	}

	private void update_text(float new_val) {
		_Text.text = new_val.ToString();
	}

}
