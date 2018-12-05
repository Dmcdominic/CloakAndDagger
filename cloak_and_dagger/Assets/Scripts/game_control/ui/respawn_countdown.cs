using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class respawn_countdown : MonoBehaviour {

	public float min_respawn_time_to_use_bar;

	public win_con_config win_Con_Config;
	public game_stats game_Stats;
	public int_float_event kill;
	public int_event_object respawn;
	public float_var respawn_time_left;
	public int_var local_id;

	public GameObject skull;
	public Text text;
	public Animator animator;

	private bool respawning = false;
	private bool on_bar = false;
	private bool on_respawn_anim_triggered = false;
	private bool return_from_bar_triggered = false;


	// Initialization
	private void Awake() {
		kill.e.AddListener(on_kill_event);
		respawn.e.AddListener(on_respawn_event);
	}

	private void OnEnable() {
		hide_skull();
	}

	private void on_kill_event(int id, float respawn_delay) {
		if (id != local_id.val) {
			return;
		}

		if (!(win_Con_Config.win_Condition == win_condition.last_survivor) || game_Stats.player_Stats[(byte)id].lives_remaining > 0) {
			respawning = true;
			on_respawn_anim_triggered = false;
			return_from_bar_triggered = false;
			text.text = Mathf.CeilToInt(respawn_time_left.val).ToString();
			on_bar = true;
		}
		animator.SetTrigger("on_death");
		on_bar = on_bar || respawn_delay >= min_respawn_time_to_use_bar;
		animator.SetBool("descend_to_bar", on_bar);
		show_skull();
	}

	private void on_respawn_event(int id) {
		print("respawn_event_received");
		if (id != local_id.val) {
			return;
		}

		hide_skull();
		respawning = false;
	}

	private void Update() {
		if (respawning) {
			int seconds_left = Mathf.Max(1, Mathf.CeilToInt(respawn_time_left.val));
			text.text = seconds_left.ToString();
			if (on_bar && respawn_time_left.val <= 3f && !return_from_bar_triggered) {
				animator.SetTrigger("return_from_bar");
				return_from_bar_triggered = true;
			}
			if (respawn_time_left.val <= 0.66f && !on_respawn_anim_triggered) {
				animator.SetTrigger("on_respawn");
				on_respawn_anim_triggered = true;
			}
		} else {
			text.text = "";
		}
	}

	private void show_skull() {
		skull.SetActive(true);
	}

	private void hide_skull() {
		skull.SetActive(false);
	}
}
