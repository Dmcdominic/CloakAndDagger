using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class trigger_death : NetworkBehaviour {

	[SerializeField]
	collision_event_object trigger;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
			trigger.e.AddListener(on_dagger_collision);
			
	}
	
	private void on_dagger_collision(GameObject dagger, Collision2D collision) {
		if (collision.gameObject.Equals(this.gameObject)) {
			die();
		}
	}

	private void die() {
		Destroy(this.gameObject);
	}
}
