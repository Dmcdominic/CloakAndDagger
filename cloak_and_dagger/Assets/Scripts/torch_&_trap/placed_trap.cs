using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placed_trap : MonoBehaviour {

	public gameplay_config gameplay_Config;

	private Animator animator;

	private float wait_time_remaining;
	private float hold_time_remaining;

	private bool holding = false;


	// Initialization
	private void Awake() {
		animator = GetComponent<Animator>();
		Light light = GetComponentInChildren<Light>();
		light.enabled = false;
		light.range = gameplay_Config.float_options[gameplay_float_option.trap_light_range];
		wait_time_remaining = gameplay_Config.float_options[gameplay_float_option.trap_waiting_duration];
	}

	// Update is called once per frame
	void Update() {
		if (holding) {
			hold_time_remaining -= Time.deltaTime;
			if (hold_time_remaining <= 0) {
				release_player();
			}
		} else {
			wait_time_remaining -= Time.deltaTime;
			if (wait_time_remaining <= 0) {
				destroy_trap();
			}
		}
	}

	public void catch_player() {
		hold_time_remaining = gameplay_Config.float_options[gameplay_float_option.trap_hold_duration];
		holding = true;
		animator.SetTrigger("catch_player");
		// todo - sfx for catching the player goes here
	}

	public void release_player() {
		// todo - releasing the player needs to do anything?
		destroy_trap();
	}

	private void destroy_trap() {
		Destroy(gameObject);
	}
}
