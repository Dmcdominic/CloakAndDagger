using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player_dagger_collision_trigger : NetworkBehaviour {

	[SerializeField]
	dagger_collision_event_object to_trigger_on_collision;


	private void OnCollisionEnter2D(Collision2D collision) {
		string tag = collision.gameObject.tag;
		if (!tag.Equals("Dagger") || !isLocalPlayer) {
			return;
		}

		dagger_data dagger_Data = collision.gameObject.GetComponent<dagger_data_carrier>().dagger_Data;
		Rpc_trigger_collision(collision.gameObject, dagger_Data, tag);
	}

	[ClientRpc]
	private void Rpc_trigger_collision(GameObject dagger, dagger_data dagger_Data, string tag) {
		to_trigger_on_collision.Invoke(dagger, dagger_Data, this.gameObject, tag);
	}

}
