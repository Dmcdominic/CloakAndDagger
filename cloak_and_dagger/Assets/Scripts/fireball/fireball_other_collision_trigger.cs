using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(fireball_data_carrier), typeof(network_id))]
public class fireball_other_collision_trigger : MonoBehaviour {

	public int_event_object to_trigger_on_collision;

	public gameplay_config gameplay_Config;

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

		if (tag == "Wall") {
            Sfx.sfx_trigger.Invoke("Fireball_hit_wall");
			to_trigger_on_collision.Invoke(network_Id.val);
		} else if (tag == "Dagger"
            && (gameplay_Config.bool_options[gameplay_bool_option.daggers_destroy_fireballs])) {
            // sound effect invoked in dagger_other_collision_trigger.cs
			to_trigger_on_collision.Invoke(network_Id.val);
		}
	}

}
