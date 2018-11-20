using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(network_id))]
public class payload_carrier : sync_behaviour<unit> {

	[SerializeField]
	private int_event_object payload_pickup;
	[SerializeField]
	private int_event_object payload_dropped;
	[SerializeField]
	private int_event_object payload_delivered;

	[SerializeField]
	private int_event_object pre_local_death;

	private payload payload_carried;


	private void Awake() {
		if (pre_local_death) {
			pre_local_death.e.AddListener(on_pre_local_death);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Payload") && payload_carried == null) {
			payload_carried = collision.gameObject.GetComponent<payload>();

			payload_carried.pick_up();
			
			payload_carried.transform.SetParent(transform);
			payload_carried.transform.localPosition = new Vector3(0.5f, 0);
			payload_carried.transform.rotation = Quaternion.identity;
			payload_pickup.Invoke(gameObject_id.val);
		} else if (collision.gameObject.CompareTag("Payload_delivery_zone") && payload_carried != null) {
			payload_delivery_zone PDZ = collision.gameObject.GetComponent<payload_delivery_zone>();
			if (PDZ && PDZ.try_deliver_here((byte)gameObject_id.val)) {
				Destroy(payload_carried);
				payload_carried = null;
				payload_delivered.Invoke(gameObject_id.val);
			}
		}
	}

	private void on_pre_local_death(int player_id) {
		if (is_local && payload_carried != null) {
			payload_carried.transform.SetParent(null);
			payload_carried.drop();
			payload_carried = null;
			payload_dropped.Invoke(gameObject_id.val);
		}
	}

}
