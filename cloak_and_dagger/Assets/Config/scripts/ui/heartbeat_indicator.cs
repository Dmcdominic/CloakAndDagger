using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class heartbeat_indicator : MonoBehaviour {

	[SerializeField]
	private gameplay_config gameplay_Config;

	[SerializeField]
	private event_object global_heartbeat_event;

	[SerializeField]
	private event_object game_started;

	[SerializeField]
	private bool_var ingame_state;

	[SerializeField]
	private Image fill_overlay;

	private Animator animator;

	private float timer;
	private float drain_timer;
	private float interval;
	private float drain_time;
	private bool active = false;
	private bool draining = false;


	// Initialization
	private void Awake() {
		fill_overlay.fillAmount = 0;
		if (!gameplay_Config.bool_options[gameplay_bool_option.heartbeat]) {
			return;
		}
		animator = GetComponent<Animator>();
		interval = gameplay_Config.float_options[gameplay_float_option.heartbeat_interval];
		drain_time = interval / 12f;
		global_heartbeat_event.e.AddListener(on_heartbeat_event);
		game_started.e.AddListener(on_game_started);
	}

	private void OnEnable() {
		fill_overlay.fillAmount = 0;
		active = false;
	}

	// Update is called once per frame
	private void Update () {
		if (!gameplay_Config.bool_options[gameplay_bool_option.heartbeat]) {
			return;
		}

		if (!active) {
			return;
		}

		timer += Time.deltaTime;
		if (draining) {
			drain_timer += Time.deltaTime;
			if (drain_timer < drain_time && fill_overlay.fillAmount > timer / interval) {
				fill_overlay.fillAmount = (drain_time - drain_timer) / drain_time;
				return;
			} else {
				draining = false;
			}
		}
		
		fill_overlay.fillAmount = timer / interval;
	}

	private void on_heartbeat_event() {
		if (!ingame_state) {
			return;
		}
		timer = 0;
		drain_timer = 0;
		animator.SetTrigger("pulse");
		draining = true;
	}

	private void on_game_started() {
		timer = 0;
		drain_timer = 0;
		fill_overlay.fillAmount = 0;
		active = true;
	}
}
