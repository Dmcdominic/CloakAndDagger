using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class bump_reveal : MonoBehaviour {

	[SerializeField]
	gameplay_config gameplay_Config;

	//[SerializeField]
	//light_spawn_event_object light_spawn_trigger;


	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			//light_spawn_data light_data = new light_spawn_data(_origin.val, 2f);
			//light_spawn_trigger.Invoke(light_data);
		}
	}

}
