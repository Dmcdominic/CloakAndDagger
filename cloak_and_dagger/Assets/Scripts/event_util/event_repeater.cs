using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class event_repeater : MonoBehaviour {

	[SerializeField]
	private gameplay_config gameplay_Config;

	[SerializeField]
	private event_object event_to_trigger;


	// Use this for initialization
	void Start() {
		// todo - start the repeater when the game start countdown ends
		if (gameplay_Config.bool_options[gameplay_bool_option.heartbeat]) {
			start_repeater();
		}
	}

	public void start_repeater() {
		StartCoroutine(triggerEvent());
	}

	public void stop_repeater() {
		StopAllCoroutines();
	}

	IEnumerator triggerEvent() {
		while (true) {
			yield return new WaitForSeconds(gameplay_Config.float_options[gameplay_float_option.heartbeat_interval]);
			event_to_trigger.Invoke();
		}
	}

}
