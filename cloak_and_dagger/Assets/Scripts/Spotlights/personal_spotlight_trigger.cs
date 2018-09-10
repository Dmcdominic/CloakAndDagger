using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class personal_spotlight_trigger : MonoBehaviour {

	[SerializeField]
	event_object global_heartbeat_trigger;

	[SerializeField]
	event_object global_spotlight_off_trigger;

	[SerializeField]
	event_object global_spotlight_on_trigger;

	private Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		if (global_heartbeat_trigger) {
			global_heartbeat_trigger.e.AddListener(heartbeat);
		}
		if (global_spotlight_off_trigger) {
			global_spotlight_off_trigger.e.AddListener(off);
		}
		if (global_spotlight_on_trigger) {
			global_spotlight_on_trigger.e.AddListener(on);
		}
	}
	
	private void heartbeat() {
		animator.Play("Heartbeat");
	}

	private void on() {
		animator.Play("On");
	}

	private void off() {
		animator.Play("Off");
	}

}
