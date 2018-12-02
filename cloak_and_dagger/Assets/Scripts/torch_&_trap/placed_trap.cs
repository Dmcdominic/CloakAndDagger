using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(network_id))]
public class placed_trap : MonoBehaviour {

	public gameplay_config gameplay_Config;

	public int_event_object trap_catch_event;

	private bool unlimited_wait_time = false;
	private float wait_time_remaining;
	private float hold_time_remaining;

	[HideInInspector]
	public int placer_id;
	[HideInInspector]
	public float buffer_time = 0.2f;

	private bool holding = false;

	private Animator animator;
	private new Light light;
	private network_id network_Id;


	// Initialization
	private void Awake() {
		animator = GetComponent<Animator>();
		network_Id = GetComponent<network_id>();
		light = GetComponentInChildren<Light>();
		light.enabled = false;
		light.range = gameplay_Config.float_options[gameplay_float_option.trap_light_range];
		wait_time_remaining = gameplay_Config.float_options[gameplay_float_option.trap_waiting_duration];
		unlimited_wait_time = (wait_time_remaining == 0);
		trap_catch_event.e.AddListener(on_trap_catch_event);
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
		} else if (!unlimited_wait_time) {
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
		when_player_caught();
		return true;
	}

	public void on_trap_catch_event(int trap_id) {
		if (trap_id != network_Id.val || holding) {
			return;
		}
		when_player_caught();
	}

	private void when_player_caught() {
		hold_time_remaining = gameplay_Config.float_options[gameplay_float_option.trap_hold_duration];
		holding = true;

		GetComponent<Collider2D>().enabled = false;
		light.enabled = true;

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

	public void set_network_id(int new_id) {
		if (!network_Id) {
			network_Id = GetComponent<network_id>();
		}
		network_Id.val = new_id;
	}

	public int get_network_id() {
		return network_Id.val;
	}
}
