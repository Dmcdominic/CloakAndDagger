using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class trigger_death : NetworkBehaviour {

	[SerializeField]
	dagger_collision_event_object trigger;

	[SerializeField]
	bool_var spectator_reveal;
	[SerializeField]
	int_var lives;
	[SerializeField]
	event_object respawn_event;

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
	
	private void on_dagger_collision(GameObject dagger, dagger_data dagger_Data, GameObject player, string tag) {
		if (player.Equals(this.gameObject)) {
			die();
		}
	}

	private void die() {
		if (isLocalPlayer) {
			lives.val--;
			if(lives.val > 0)
				respawn_event.Invoke();
			else
				spectator_reveal.val = true; 
			NetworkServer.Destroy(gameObject);
		}
	}
}
