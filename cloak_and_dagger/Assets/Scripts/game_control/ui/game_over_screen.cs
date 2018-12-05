using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_over_screen : MonoBehaviour {

	public event_object game_over;


	private void Awake() {
		if (game_over) {
			game_over.e.AddListener(on_game_over);
		}
		
	}

    private void OnEnable()
    {
        set_children_active(false);
    }

    private void on_game_over() {
		set_children_active(true);
	}

	private void set_children_active(bool active) {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(active);
		}
	}
}
