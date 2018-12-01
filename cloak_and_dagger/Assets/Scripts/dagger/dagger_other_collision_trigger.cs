using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(dagger_data_carrier), typeof(network_id))]
public class dagger_other_collision_trigger : MonoBehaviour {

	public int_event_object to_trigger_on_collision;

	public gameplay_config gameplay_Config;

    public Sound_manager Sfx;

    [SerializeField]
    GameObject particles;


	private void OnTriggerEnter2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		network_id network_Id = GetComponent<network_id>();
		string tag = collided_with.tag;
		if (tag == "Player") {
            return;
		}

		if (tag == "Wall"
            && !(gameplay_Config.bool_options[gameplay_bool_option.daggers_pierce_walls])) {
            Sfx.sfx_trigger.Invoke("Dagger_hit_wall");
			to_trigger_on_collision.Invoke(network_Id.val);
            Instantiate(particles, transform.position, transform.rotation);
		} else if (tag == "Dagger"
            && (gameplay_Config.bool_options[gameplay_bool_option.daggers_destroy_daggers])) {
            Sfx.sfx_trigger.Invoke("Dagger_hit_dagger");
			to_trigger_on_collision.Invoke(network_Id.val);
            Instantiate(particles, transform.position, transform.rotation);
        } else if (tag == "Fireball"
            && (gameplay_Config.bool_options[gameplay_bool_option.fireballs_destroy_daggers])) {
            Sfx.sfx_trigger.Invoke("Dagger_hit_fireball");
            to_trigger_on_collision.Invoke(network_Id.val);
            Instantiate(particles, transform.position, transform.rotation);
        }
	}
}