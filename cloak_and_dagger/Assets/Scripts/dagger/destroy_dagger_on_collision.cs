using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class destroy_dagger_on_collision : NetworkBehaviour {

	[SerializeField]
	dagger_collision_event_object player_collision_trigger;

	[SerializeField]
	dagger_collision_event_object other_collision_trigger;

	private void Start() {
		player_collision_trigger.e.AddListener(on_player_collision);
		other_collision_trigger.e.AddListener(on_other_collision);
	}

	private void on_player_collision(GameObject dagger, dagger_data dagger_Data, GameObject collided_with, string tag) {
		if (this.GetComponent<NetworkIdentity>().netId != dagger.GetComponent<NetworkIdentity>().netId) {
			return;
		}

		if (!dagger_Data.collaterals) {
			Destroy(this.gameObject);
		}
	}

	private void on_other_collision(GameObject dagger, dagger_data dagger_Data, GameObject collided_with, string tag) {
		if (this.GetComponent<NetworkIdentity>().netId != dagger.GetComponent<NetworkIdentity>().netId) {
			return;
		}

		if (tag == "Wall") {
			Destroy(this.gameObject);
		} else if (tag == "Dagger") {
			Destroy(this.gameObject);
		}
	}

}
