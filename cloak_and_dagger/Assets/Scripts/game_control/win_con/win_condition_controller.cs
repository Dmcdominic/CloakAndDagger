﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct win_con_init_struct {
	public bool loaded_and_ready_msg;
	public bool start_countdown_msg;

	public win_con_init_struct (bool _loaded_and_ready_msg, bool _start_countdown_msg) {
		loaded_and_ready_msg = _loaded_and_ready_msg;
		start_countdown_msg = _start_countdown_msg;
	}
}

public abstract class win_condition_controller : MonoBehaviour {
	public abstract win_condition win_Condition { get; }
	public abstract bool free_for_all_compatible { get; }

	//[HideInInspector]
	public win_condition_assets_packet WCAP;

	protected Dictionary<byte, player_stats> player_stats_dict;
	protected Dictionary<byte, team_stats> team_stats_dict;

	private int loaded_and_ready_msgs_received = 0;
	private bool countdown_triggered = false;
	private float min_loading_timer;

	private float time_limit;
#pragma warning disable 0414
	private bool timed_out = false;

    [SerializeField]
    event_object die;


	// Initialization
	private void Awake() {
		// Set up listeners
		if (WCAP.win_Con_Config.win_Condition != win_Condition) {
			gameObject.SetActive(false);
			return;
		}
		if (WCAP.player_killed) {
			WCAP.player_killed.e.AddListener(on_player_killed_general);
		}
		if (WCAP.done_initing) {
			WCAP.done_initing.e.AddListener(on_done_initing);
		}
		if (WCAP.in_event) {
			WCAP.in_event.e.AddListener(fake_rectify);
		}

		// Initialize stats dictionaries and set WCAP references
		player_stats_dict = new Dictionary<byte, player_stats>();
		team_stats_dict = new Dictionary<byte, team_stats>();
		WCAP.game_Stats.player_Stats = player_stats_dict;
		WCAP.game_Stats.team_Stats = team_stats_dict;

		// Populate the stats dictionaries
		int starting_lives = WCAP.win_Con_Config.int_options[winCon_int_option.lives];
		foreach (byte player in WCAP.teams) {
			byte team;

			bool free_for_all_enabled = WCAP.win_Con_Config.bool_options[winCon_bool_option.free_for_all];
			if (free_for_all_compatible && free_for_all_enabled) {
				team = player;
			} else {
				team = (byte)WCAP.teams[player];
			}

			player_stats new_player_stats = new player_stats(player, team, starting_lives);
			player_stats_dict.Add(player, new_player_stats);

			if (team_stats_dict.ContainsKey(team)) {
				team_stats_dict[team].lives_remaining += starting_lives;
			} else {
				team_stats new_team_stats = new team_stats(team, starting_lives);
				team_stats_dict.Add(team, new_team_stats);
			}
		}

		// Initialize remaining variables
		WCAP.ingame_state.val = false;
		WCAP.game_timer.val = 0;
		time_limit = WCAP.win_Con_Config.float_options[winCon_float_option.time_limit];

		// Let the WC subclass do its initialization
		init();
	}

	// Once the scene has fully loaded and the client is done initing.
	private void on_done_initing() {
		if (WCAP.host.val) {
			loaded_and_ready_msgs_received++;
			StartCoroutine(backup_start_host_only());
			min_loading_timer = WCAP.readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.min_loading_time];
			check_if_all_ready();
		} else {
			WCAP.out_event.Invoke(0, new win_con_init_struct(true, false), 0);
		}
       
	}

	public void fake_rectify(float t, object state, int placeholder) {
		win_con_init_struct init_struct = (win_con_init_struct)state;
		if (init_struct.loaded_and_ready_msg && WCAP.host.val) {
			loaded_and_ready_msgs_received++;
			check_if_all_ready();
		}

		if (init_struct.start_countdown_msg && !WCAP.host.val) {
			StartCoroutine(starting_countdown());
		}
	}

	// This should be called by the host once it is ready.
	// It will send the start_countdown_msg after a delay, if not sent already.
	IEnumerator backup_start_host_only() {
		if (WCAP.host.val) {
			yield return new WaitForSeconds(WCAP.readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.host_backup_start_time]);
			if (!countdown_triggered) {
				on_all_ready_host_only();
			}
		}
	}

	private void check_if_all_ready() {
		if (loaded_and_ready_msgs_received >= player_stats_dict.Count) {
			StartCoroutine(on_all_ready_host_only());
		}
	}

	// This should be called by the host once everyone is ready.
	IEnumerator on_all_ready_host_only() {
		if (WCAP.host.val) {
			yield return new WaitUntil(() => min_loading_timer <= 0);
			WCAP.out_event.Invoke(0, new win_con_init_struct(false, true), 0);
			countdown_triggered = true;
			yield return new WaitForSeconds(WCAP.readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.host_start_delay]);
			StartCoroutine(starting_countdown());
		}
	}

	// Start this coroutine to activate the countdown.
	// Will call on_game_start_general after the countdown.
	IEnumerator starting_countdown() {
        
        int seconds_left = 5;
		while (seconds_left > 0) {
			WCAP.countdown_event.Invoke(seconds_left);
			yield return new WaitForSeconds(WCAP.readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.countdown_time_interval]);
			seconds_left--;
		}
		on_game_start_general();
		yield return null;
        //transform.SetParent(null);
    }

	// Is called at the end of the countdown
	private void on_game_start_general() {
		WCAP.ingame_state.val = true;
		WCAP.trigger_on_game_start.Invoke();
		on_game_start();
	}

	// Manages the game timer.
	// If you want to use update, make sure to call base.Update() as well.
	protected void Update() {
		if (min_loading_timer > 0) {
			min_loading_timer -= Time.deltaTime;
		}

		if (WCAP.ingame_state.val) {
			WCAP.game_timer.val += Time.deltaTime;
			if (time_limit != 0 && time_limit < WCAP.game_timer.val) {
				timed_out = true;
				on_timeout();
			}
		}
	}

	// Is called by the player_killed event
	private void on_player_killed_general(death_event_data death_data) {
		player_stats killed = player_stats_dict[death_data.playerID];
		killed.death_count++;
		team_stats_dict[killed.teamID].death_count++;
		if (death_data.playerID != death_data.killerID) {
			player_stats killer = player_stats_dict[death_data.killerID];
			killer.kill_count++;
		}
		on_player_killed(death_data);
	}

	protected abstract void init(); // Initialize your controller values using the win_Con_Config
	protected abstract void on_game_start(); // Start the game

	// This will be called when a player_death_event is received,
	// AFTER updating the player_stats and team_stats with basic kill and death counter increments
	protected abstract void on_player_killed(death_event_data death_data);

	// This will be called if time runs out.
	// end_game_general must be called from here with the proper winning team,
	// or -1 if there was a tie.
	protected abstract void on_timeout();

	// Should be called when you want to end the game (i.e. when the win con is met or time runs out)
	// Note that this may be called by a timeout, so if you need anything to occur before the game ends,
	// You should override this and then call base.end_game_general(timeout) at the end.
	protected void end_game_general(List<byte> winning_teams) {
		WCAP.ingame_state.val = false;

		// Update winner variables in teams and players
		foreach (byte team in winning_teams) {
			team_stats_dict[team].winner = true;
		}
		foreach (player_stats player_Stats in player_stats_dict.Values) {
			if (winning_teams.Contains(player_Stats.teamID)) {
				player_Stats.winner = true;
			}
		}

		WCAP.trigger_on_game_over.Invoke(); 
		Destroy(gameObject);
	}
	protected void end_game_general(byte winning_team) {
		List<byte> winning_teams = new List<byte>();
		winning_teams.Add(winning_team);
		end_game_general(winning_teams);
	}
}


// Stores a single player's stats for this match
public class player_stats : team_stats {
	public byte playerID;
	public player_stats(byte _playerID, byte _teamID, int _starting_lives) : base(_teamID, _starting_lives) {
		playerID = _playerID;
		teamID = _teamID;
	}
}

// Stores a single team's stats for this match
public class team_stats {
	public byte teamID;
	public byte kill_count = 0;
	public byte death_count = 0;
	public bool winner = false;
    public float time_in_hill = 0.0f;
    public float time_as_king = 0.0f;

	public team_stats(byte _teamID, int _starting_lives) {
		teamID = _teamID;
		lives_remaining = _starting_lives;
	}

	// Last survivor
	public int lives_remaining;

	// Assault
	public int payload_pickups = 0;
	public int payload_deliveries = 0;
}
