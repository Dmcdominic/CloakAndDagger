using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_shake : MonoBehaviour {

	[SerializeField]
	private float_event_object trigger;

	[SerializeField]
	private readonly_camera_config camera_Config;

	private bool shaking;
	private Vector3 pre_shake_pos;

	private Vector3 vel;


	// Use this for initialization
	void Start () {
		if (trigger) {
			trigger.e.AddListener(on_trigger);
		}
	}

	private void on_trigger(float amount) {
		if (amount == 0) {
			return;
		}

		if (camera_Config.bool_options[readonly_camera_bool_option.camera_shake]) {
			if (shaking) {
				StopAllCoroutines();
				shaking = false;
				transform.position = pre_shake_pos;
			}
			StartCoroutine(shake(amount, camera_Config.float_options[readonly_camera_float_option.shake_duration]));
		}
	}

	IEnumerator shake(float mag, float dur) {
		shaking = true;
		pre_shake_pos = transform.position;

		float elapsedTime = 0f;
		Vector3 originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		int steps = 1000;
		float stepTime = 0;
		float stepSize = dur / steps;
		float percentComplete = elapsedTime / dur;
		float damper = 1.0f;

		float PosX = Random.value * 2.0f - 1.0f;
		float PosY = Random.value * 2.0f - 1.0f;

		PosX *= mag * damper * damper;
		PosY *= mag * damper * damper;
		Vector3 target = new Vector3(originalPos.x + PosX, originalPos.y + PosY, originalPos.z);

		while (elapsedTime < dur + mag * mag * mag + .1f * mag) {

			elapsedTime += Time.deltaTime;
			stepTime += Time.deltaTime;
			if (stepTime > stepSize) {
				stepTime = 0;
				percentComplete = elapsedTime / dur;
				damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

				PosX = Random.value * 2.0f - 1.0f;
				PosY = Random.value * 2.0f - 1.0f;

				PosX *= mag * damper * damper;
				PosY *= mag * damper * damper;
				target = new Vector3(originalPos.x + PosX, originalPos.y + PosY, originalPos.z);
			}

			transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, (elapsedTime / dur) * .01f, 500);

			//transform.Translate(PosX,PosY,originalPos.z);
			yield return null;
		}

		transform.position = pre_shake_pos;
		shaking = false;
	}
}
