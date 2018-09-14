using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Currently UNUSED.
// Originally built for player-dagger collisions but designed to be generalized.
// Now player-dagger collisions are managed by player_dagger_collision_triger.
public class trigger_on_collision : NetworkBehaviour {

	[SerializeField]
	network_sided_condition trigger_only_on;

	[SerializeField]
	collision_event_object to_trigger_on_collision;

	// To only trigger when colliding with objects of certain tags:
	[SerializeField]
	private bool limit_collisions_to_tag_list;

	[SerializeField]
	private List<string> tags;


	private void OnCollisionEnter2D(Collision2D collision) {
		string tag = collision.gameObject.tag;
		if (limit_collisions_to_tag_list && !tags.Contains(tag)) {
			return;
		}

		switch (trigger_only_on) {
			case network_sided_condition.locally:
				to_trigger_on_collision.Invoke(this.gameObject, collision);
				break;
			case network_sided_condition.server_to_all_clients:
				if (isServer) {
					// Rpc called here
				}
				break;
			case network_sided_condition.local_player_to_all_clients:
				if (isClient) {
					// Command called here
				}
				break;
		}
	}

	// Need an Rpc, and a command, and need to change the parameters of the event to be serializable

	//[ClientRpc]
	//private void Rpc_
	//to_trigger_on_collision.Invoke(this.gameObject, collision);

}
