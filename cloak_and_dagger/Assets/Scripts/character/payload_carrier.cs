﻿using System.Collections;
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

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Payload")) {
			payload_carried = collision.gameObject;
			payload_carried.transform.position = transform.position;
			payload_carried.transform.SetParent(transform);
			payload_carried.GetComponent<Collider2D>().enabled = false;
			payload_pickup.Invoke(network_Id.val);
		} else if (collision.gameObject.CompareTag("Payload_delivery_zone") && payload_carried != null) {
			// todo - check if it's the correct zone, based on team
			Destroy(payload_carried);
			payload_carried = null;
			payload_delivered.Invoke(network_Id.val);
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
