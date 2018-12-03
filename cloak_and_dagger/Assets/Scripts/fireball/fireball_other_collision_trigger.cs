using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(fireball_data_carrier), typeof(network_id))]
public class fireball_other_collision_trigger : MonoBehaviour {

	public int_event_object to_trigger_on_collision;

	public gameplay_config gameplay_Config;
	public readonly_camera_config camera_Config;

	public float_event_object camera_shake_event;

	public Sound_manager Sfx;

	private network_id network_Id;


	private void Awake() {
		network_Id = GetComponent<network_id>();
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		string tag = collided_with.tag;
		if (tag == "Player") {
			return;
		}

		if (tag == "Wall" && !gameplay_Config.bool_options[gameplay_bool_option.fireballs_pierce_walls]) {
            Sfx.sfx_trigger.Invoke("Fireball_hit_wall");
			on_any_collision();
		} else if (tag == "Dagger" && (gameplay_Config.bool_options[gameplay_bool_option.daggers_destroy_fireballs])) {
			// sound effect invoked in dagger_other_collision_trigger.cs
			on_any_collision();
		} else if (tag == "Fireball" && (gameplay_Config.bool_options[gameplay_bool_option.fireballs_destroy_fireballs])) {
            Sfx.sfx_trigger.Invoke("Fire_hit_fire");
			on_any_collision();
		}
	}

	private void on_any_collision() {
		to_trigger_on_collision.Invoke(network_Id.val);
		camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.fireball_hit_object_shake]);
	}
}
