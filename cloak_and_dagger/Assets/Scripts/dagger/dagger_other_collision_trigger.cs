using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(dagger_data_carrier), typeof(network_id))]
public class dagger_other_collision_trigger : NetworkBehaviour {

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
		to_trigger_on_collision.Invoke(this.gameObject, data_carrier.dagger_Data, collided_with, tag);
	}

}
