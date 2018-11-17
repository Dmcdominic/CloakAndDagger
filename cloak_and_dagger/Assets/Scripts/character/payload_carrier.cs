using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(network_id))]
public class payload_carrier : MonoBehaviour {

	[SerializeField]
	private int_event_object payload_pickup;
	[SerializeField]
	private int_event_object payload_dropped;
	[SerializeField]
	private int_event_object payload_delivered;

	private GameObject payload_carried;
	private network_id network_Id;

	
	void Start () {
		network_Id = GetComponent<network_id>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Payload") && payload_carried == null) {
			payload_carried = collision.gameObject;
			
			payload_carried.transform.SetParent(transform);
			payload_carried.transform.localPosition = new Vector3(0.5f, 0);
			payload_carried.transform.rotation = Quaternion.identity;

			payload_carried.GetComponent<Collider2D>().enabled = false;
			payload_pickup.Invoke(network_Id.val);
		} else if (collision.gameObject.CompareTag("Payload_delivery_zone") && payload_carried != null) {
			payload_delivery_zone PDZ = collision.gameObject.GetComponent<payload_delivery_zone>();
			if (PDZ && PDZ.try_deliver_here((byte)network_Id.val)) {
				Destroy(payload_carried);
				payload_carried = null;
				payload_delivered.Invoke(network_Id.val);
			}
		}
	}

	private void OnDisable() {
		if (payload_carried != null) {
			payload_carried.transform.SetParent(null);
			payload_carried.GetComponent<Collider2D>().enabled = true;
			payload_carried = null;
			payload_dropped.Invoke(network_Id.val);
		}
	}

}
