using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(dagger_data_carrier), typeof(network_id))]
public class dagger_other_collision_trigger : MonoBehaviour {

	public int_event_object to_trigger_on_collision;


	private void OnTriggerEnter2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		string tag = collided_with.tag;
		if (tag == "Player") {
			return;
		}

		if (tag == "Wall") {
			to_trigger_on_collision.Invoke(gameObject.GetInstanceID());
		} else if (tag == "Dagger") {
			to_trigger_on_collision.Invoke(gameObject.GetInstanceID());
		} else if (tag == "Dead-Player") {
			to_trigger_on_collision.Invoke(gameObject.GetInstanceID());
		}
	}

}
