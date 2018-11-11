using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starting_countdown : MonoBehaviour {

	public int_event_object countdown_event;
	public event_object game_started;

	public Text text;


	private void Awake() {
		if (countdown_event) {
			countdown_event.e.AddListener(update_text);
		}
		if (game_started) {
			game_started.e.AddListener(on_game_started);
		}
	}

	private void update_text(int new_val) {
		set_children_active(true);
		text.text = new_val.ToString();
	}

	private void on_game_started() {
		set_children_active(false);
	}

	private void set_children_active(bool active) {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(active);
		}
	}

}
