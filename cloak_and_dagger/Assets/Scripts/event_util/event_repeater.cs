using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class event_repeater : NetworkBehaviour {

	[SerializeField]
	private bool serverOnly;

	[SerializeField]
	private bool autostart;

	[SerializeField]
	private float interval;

	[SerializeField]
	private event_object event_to_trigger;


	// Use this for initialization
	void Start() {
		if (autostart) {
			start_repeater();
		}
	}

	public void start_repeater() {
		if (serverOnly && !isServer) {
			return;
		}
		StartCoroutine(triggerEvent());
	}

	public void stop_repeater() {
		StopAllCoroutines();
	}

	IEnumerator triggerEvent() {
		while (true) {
			yield return new WaitForSeconds(interval);
			Rpc_trigger_event();
		}
	}

	// Triggers the event on all clients
	[ClientRpc]
	private void Rpc_trigger_event() {
		event_to_trigger.Invoke();
	}

}
