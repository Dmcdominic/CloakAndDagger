using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class heartbeat_repeater : MonoBehaviour {

	[SerializeField]
	private gameplay_config gameplay_Config;

	[SerializeField]
	private int_event_object countdown_event;

	[SerializeField]
	private event_object game_started;

	[SerializeField]
	private event_object global_heartbeat_to_trigger;


	private void Awake() {
		if (countdown_event) {
			countdown_event.e.AddListener(on_countdown_event);
		}
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
			global_heartbeat_to_trigger.Invoke();
		}
	}

	private void on_countdown_event(int seconds_left) {
		if (seconds_left == 1 && gameplay_Config.bool_options[gameplay_bool_option.initial_reveal]) {
			global_heartbeat_to_trigger.Invoke();
		}
	}

}
