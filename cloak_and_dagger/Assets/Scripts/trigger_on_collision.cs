using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class trigger_on_collision : NetworkBehaviour {

	[SerializeField]
	collision_event_object to_trigger_on_collision;

	private void OnCollisionEnter2D(Collision2D collision) {
		to_trigger_on_collision.Invoke(this.gameObject, collision);

		// TEMPORARY:
		Destroy(gameObject);
	}

}
