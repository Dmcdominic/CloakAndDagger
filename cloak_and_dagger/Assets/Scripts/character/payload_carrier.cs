using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum payload_event { pickup, delivery }

[System.Serializable]
public struct payload_event_struct {
	public payload_event event_type;
	public payload_event_struct(payload_event _event_type) {
		event_type = _event_type;
	}
}

[RequireComponent(typeof(Collider2D))]
public class payload_carrier : sync_behaviour<payload_event_struct> {

	[SerializeField]
	private int_event_object payload_pickup;
	[SerializeField]
	private int_event_object payload_dropped;
	[SerializeField]
	private int_event_object payload_delivered;

	[SerializeField]
	private int_event_object pre_local_death;

	[SerializeField]
	private gameobject_var global_payload;

	private payload _payload;
	private payload payload {
		get {
			if (!_payload) {
				if (!global_payload.val) {
					return null;
				}
				_payload = global_payload.val.GetComponent<payload>();
			}
			return _payload;
		}
	}

	public bool carrying { get { return payload && payload.carried && payload.carrier_id == gameObject_id.val; } }


	private void Awake() {
		if (pre_local_death) {
			pre_local_death.e.AddListener(on_pre_local_death);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!is_local) {
			return;
		}

		if (collision.gameObject.CompareTag("Payload") && !carrying) {
			// For payload pickup
			try_pickup_payload(Time.time);
			send_state(new payload_event_struct(payload_event.pickup));
		} else if (collision.gameObject.CompareTag("Payload_delivery_zone") && carrying) {
			// For payload delivery
			payload_delivery_zone PDZ = collision.gameObject.GetComponent<payload_delivery_zone>();
			if (PDZ && PDZ.try_deliver_here((byte)gameObject_id.val)) {
				deliver_payload();
				send_state(new payload_event_struct(payload_event.delivery));
			}
		}
	}

	// Triggered when a player dies, before the standard death event is triggered.
	// Payload is dropped here.
	private void on_pre_local_death(int player_id) {
		if (player_id == gameObject_id.val && carrying) {
			drop_payload();
		}
	}

	// Receive a pickup or delivery event
	public override void rectify(float t, payload_event_struct state) {
		if (state.event_type == payload_event.pickup) {
			try_pickup_payload(t);
		} else if (state.event_type == payload_event.delivery) {
			deliver_payload();
		}
	}

	private void try_pickup_payload(float t) {
		if (!payload.carried || t < payload.last_pickup_time) {
			pickup_payload(t);
		}
	}

	private void pickup_payload(float t) {
		payload.transform.SetParent(transform);
		payload.transform.localPosition = new Vector3(0.5f, 0);
		payload.transform.rotation = Quaternion.identity;

		payload.pick_up(gameObject_id.val, t);
		payload_pickup.Invoke(gameObject_id.val);
	}

	private void drop_payload() {
		payload.transform.SetParent(null);
		payload.drop();
		payload_dropped.Invoke(gameObject_id.val);
	}

	private void deliver_payload() {
		payload.deliver();
		payload_delivered.Invoke(gameObject_id.val);
	}

}
