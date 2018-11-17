using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class bump_reveal : MonoBehaviour {

	[SerializeField]
	gameplay_config gameplay_Config;

	[SerializeField]
	light_spawn_event_object light_spawn_trigger;


	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			// todo - replace this with a reveal float var that only reveals the players to each other locally
			Vector2 position = (collision.transform.position + transform.position) / 2f;
			light_spawn_data light_data = new light_spawn_data(position, gameplay_Config.float_options[gameplay_float_option.bump_reveal_time]);
			light_spawn_trigger.Invoke(light_data);
		}
	}

}
