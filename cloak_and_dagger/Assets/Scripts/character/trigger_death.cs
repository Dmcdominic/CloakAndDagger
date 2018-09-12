using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class trigger_death : NetworkBehaviour {

	[SerializeField]
	collision_event_object trigger;

	[SerializeField]
	bool_var spectator_reveal;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			trigger.e.AddListener(on_dagger_collision);
		}

		// THIS vvv IS TEMPORARY. We will need a way to set spectator_reveal to false
		// at the start of every match, so long as the player is alive,
		// but set it/leave it as true if someone loaded in specifically as a spectator.
		spectator_reveal.val = false;
	}
	
	private void on_dagger_collision(GameObject dagger, Collision2D collision) {
		if (collision.gameObject.Equals(this.gameObject)) {
			die();
		}
	}

	private void die() {
		if (isLocalPlayer) {
			//spectator_reveal.val = true;
			NetworkServer.Destroy(gameObject);
		}
	}
}
