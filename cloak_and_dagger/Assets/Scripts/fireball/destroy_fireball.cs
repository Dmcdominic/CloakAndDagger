using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(network_id))]
public class destroy_fireball : sync_behaviour<unit> {

	public int_event_object trigger_destroy;

	public GameObject incineration_prefab;


	private void Awake() {
		if (trigger_destroy) {
			trigger_destroy.e.AddListener(on_trigger_destroy);
		}
	}

	private void on_trigger_destroy(int fireballID) {
		network_id network_Id = GetComponent<network_id>();
		if (network_Id.val != fireballID) {
			return;
		}
        
		send_state(new unit());
		destroy_this();
	}

	public override void rectify(float t, unit state) {
		destroy_this();
	}

	public void destroy_this() {
		GameObject incineration = Instantiate(incineration_prefab);
		incineration.transform.position = gameObject.transform.position;
		Destroy(gameObject);
	}

}
