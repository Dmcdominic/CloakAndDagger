using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(dagger_data_carrier))]
public class dagger_other_collision_trigger : NetworkBehaviour {

	[SerializeField]
	network_sided_condition trigger_only_on;

	[SerializeField]
	dagger_collision_event_object to_trigger_on_collision;

	private dagger_data_carrier data_carrier;


	private void Start() {
		data_carrier = GetComponent<dagger_data_carrier>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		GameObject collided_with = collision.gameObject;
		string tag = collided_with.tag;
		if (tag == "Player") {
			return;
		}

		GameObject collided_with_safe = collided_with.GetComponent<NetworkIdentity>() ? collided_with : null;

		switch (trigger_only_on) {
			case network_sided_condition.locally:
				to_trigger_on_collision.Invoke(this.gameObject, data_carrier.dagger_Data, collided_with, tag);
				break;
			case network_sided_condition.server_to_all_clients:
				if (isServer) {
					Rpc_trigger_collision(this.gameObject, data_carrier.dagger_Data, collided_with_safe, tag);
				}
				break;
			case network_sided_condition.local_player_to_all_clients:
				if (isLocalPlayer) {
					Cmd_trigger_collision(this.gameObject, data_carrier.dagger_Data, collided_with_safe, tag);
				}
				break;
		}
	}

	[Command]
	private void Cmd_trigger_collision(GameObject dagger, dagger_data dagger_Data, GameObject collided_with, string tag) {
		Rpc_trigger_collision(dagger, dagger_Data, collided_with, tag);
	}

	[ClientRpc]
	private void Rpc_trigger_collision(GameObject dagger, dagger_data dagger_Data, GameObject collided_with, string tag) {
		to_trigger_on_collision.Invoke(dagger, dagger_Data, collided_with, tag);
	}

}
