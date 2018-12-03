using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pre_countdown_overlay : MonoBehaviour {

	public int_event_object countdown_event;

	private Animator animator;

	private bool hidden = false;


	private void Awake() {
		animator = GetComponent<Animator>();
		if (countdown_event) {
			countdown_event.e.AddListener(on_countdown_event);
		}
	}

	private void OnEnable() {
		animator.SetTrigger("show_overlay");
		hidden = false;
	}

	private void on_countdown_event(int seconds_left) {
		//gameObject.SetActive(false);
		if (!hidden) {
			animator.SetTrigger("hide_overlay");
			hidden = true;
		}
	}
}
