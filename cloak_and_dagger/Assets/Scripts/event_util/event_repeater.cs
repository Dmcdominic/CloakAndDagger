using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class event_repeater : MonoBehaviour {

	[SerializeField]
	private gameplay_config gameplay_Config;

	[SerializeField]
	private event_object game_started;

	[SerializeField]
	private event_object event_to_trigger;


	private void Awake() {
		if (game_started) {
			game_started.e.AddListener(start_repeater);
		}
	}

	public void start_repeater() {
		stop_repeater();
		if (gameplay_Config.bool_options[gameplay_bool_option.heartbeat]) {
			StartCoroutine(triggerEvent());
		}
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
