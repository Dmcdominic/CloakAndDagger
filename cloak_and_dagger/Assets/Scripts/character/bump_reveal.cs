using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class bump_reveal : MonoBehaviour {

	[SerializeField]
	gameplay_config gameplay_Config;

	[SerializeField]
	light_spawn_event_object light_spawn_trigger;



    [SerializeField]
    int_float_event trigger;


	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
            // todo - replace this with a reveal float var that only reveals the players to each other locally
            trigger.Invoke(gameObject.GetComponent<network_id>().val,1);
		}
	}
}
