using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_ui_canvas : MonoBehaviour {

	public GameObject pre_countdown_overlay;
	public event_object local_start;
	public float_event_object start_in;

	public event_object enable_trigger;
	public List<event_object> disable_triggers;


	private void Awake() {
		DontDestroyOnLoad(gameObject);
		if (local_start) {
			local_start.e.AddListener(enable_pre_countdown_overlay);
		}
		if (start_in) {
			start_in.e.AddListener(x => enable_pre_countdown_overlay());
		}
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

	private void enable_pre_countdown_overlay() {
		pre_countdown_overlay.SetActive(true);
	}

}
