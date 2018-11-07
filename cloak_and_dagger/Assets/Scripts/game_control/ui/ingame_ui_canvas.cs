using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_ui_canvas : MonoBehaviour {

	public event_object enable_trigger;
	public event_object disable_trigger;


	private void Awake() {
		DontDestroyOnLoad(gameObject);
		if (enable_trigger) {
			enable_trigger.e.AddListener(enable);
		}
		if (disable_trigger) {
			disable_trigger.e.AddListener(disable);
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
