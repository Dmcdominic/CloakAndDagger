using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_ui_canvas : MonoBehaviour {

	public event_object enable_trigger;
	public List<event_object> disable_triggers;


	private void Awake() {
		DontDestroyOnLoad(gameObject);
		if (enable_trigger) {
			enable_trigger.e.AddListener(enable);
		}
		foreach (event_object event_obj in disable_triggers) {
			event_obj.e.AddListener(disable);
		}
		disable();
	}

	private void enable() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
	}

	private void disable() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
	}

}
