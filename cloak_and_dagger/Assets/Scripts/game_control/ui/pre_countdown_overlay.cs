using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pre_countdown_overlay : MonoBehaviour {

	public int_event_object countdown_event;
	public event_object done_initing;

	public Text loading_text;

	private Animator animator;

	private bool hidden = false;


	private void Awake() {
		animator = GetComponent<Animator>();
		if (countdown_event) {
			countdown_event.e.AddListener(on_countdown_event);
		}
		if (done_initing) {
			done_initing.e.AddListener(on_done_initing);
		}
	}

	private void OnEnable() {
		animator.SetTrigger("show_overlay");
		loading_text.text = "L O A D I N G   M A P . . .";
		hidden = false;
	}

	private void on_countdown_event(int seconds_left) {
		if (!hidden) {
			animator.SetTrigger("hide_overlay");
			hidden = true;
		}
	}

	private void on_done_initing() {
		loading_text.text = "C O N N E C T I N G   T O   P A R T Y . . .";
	}
}
