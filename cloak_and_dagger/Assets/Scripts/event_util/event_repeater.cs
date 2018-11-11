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

	private float interval = 10f;


	private void Awake() {
		start_repeater();
	}

	public void start_repeater() {
		stop_repeater();
		StartCoroutine(triggerEvent());
	}

	public void stop_repeater() {
		StopAllCoroutines();
	}

	IEnumerator triggerEvent() {
		while (true) {
			yield return new WaitForSeconds(interval);
			event_to_trigger.Invoke();
		}
	}

}
