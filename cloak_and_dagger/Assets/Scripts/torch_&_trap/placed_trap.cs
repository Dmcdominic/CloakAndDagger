using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placed_trap : MonoBehaviour {

	public gameplay_config gameplay_Config;

	private Animator animator;
	private new Light light;

	private float wait_time_remaining;
	private float hold_time_remaining;

	[HideInInspector]
	public int placer_id;
	[HideInInspector]
	public float buffer_time = 0.2f;

	private bool holding = false;


	// Initialization
	private void Awake() {
		animator = GetComponent<Animator>();
		light = GetComponentInChildren<Light>();
		light.enabled = false;
		light.range = gameplay_Config.float_options[gameplay_float_option.trap_light_range];
		wait_time_remaining = gameplay_Config.float_options[gameplay_float_option.trap_waiting_duration];
	}

	// Update is called once per frame
	void Update() {
		if (buffer_time > 0) {
			buffer_time -= Time.deltaTime;
		}

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

	public bool catch_player(int player_id) {
		if (buffer_time > 0 && player_id == placer_id) {
			return false;
		}

		hold_time_remaining = gameplay_Config.float_options[gameplay_float_option.trap_hold_duration];
		holding = true;

		GetComponent<Collider2D>().enabled = false;
		light.enabled = true;

		animator.SetTrigger("catch_player");
		// todo - sfx for catching the player goes here
		return true;
	}

	public void release_player() {
		// todo - releasing the player needs to do anything?
		destroy_trap();
	}

	private void destroy_trap() {
		Destroy(gameObject);
	}
}
