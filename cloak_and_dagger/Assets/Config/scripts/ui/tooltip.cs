using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltip : MonoBehaviour {

	public string_event_object tooltip_update;
	public int_event_object update_config;
	public Text text;


	private void Awake() {
		if (tooltip_update) {
			tooltip_update.e.AddListener(update_text);
		}
		if (update_config) {
			update_config.e.AddListener(clear_text);
		}
	}

	private void update_text(string new_text) {
		text.text = new_text;
	}
	private void clear_text(int int_event_requirement) {
		update_text("");
	}

}
