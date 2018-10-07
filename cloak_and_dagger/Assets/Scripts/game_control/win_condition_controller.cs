using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class win_condition_controller : MonoBehaviour {
	public abstract win_condition win_Condition { get; }

	public win_con_config win_Con_Config;

	// Events to listen to
	public event_object game_start_trigger;
	public event_object player_killed; // TODO - This should be the death_event that we made on Ning's branch

	// Events to trigger
	public event_object trigger_on_game_over; // TODO - this needs to store some kind of game_over_data

	// Game variables
	public bool_var ingame_state;

	public float_var game_timer;
	private float time_limit;

	protected abstract player_stats get_player_stats(byte playerID);
	protected abstract team_stats get_team_stats(byte teamID);


	// Initialization
	protected void Awake() {
		if (win_Con_Config.win_Condition != win_Condition) {
			gameObject.SetActive(false);
			return;
		}
		if (player_killed) {
			player_killed.e.AddListener(on_player_killed);
		}
		if (game_start_trigger) {
			game_start_trigger.e.AddListener(on_game_start_general);
		}

		ingame_state.val = false;
		game_timer.val = 0;
		time_limit = win_Con_Config.float_options[winCon_float_option.time_limit];
		init();
	}
	
	protected void Update () {
		game_timer.val += Time.deltaTime;
		if (time_limit != 0 && time_limit < game_timer.val) {
			end_game_general(true);
		}
	}

	// Is called by the game_start_trigger event
	private void on_game_start_general() {
		ingame_state.val = true;
		on_game_start();
	}

	// Is called by the player_killed event
	// TODO - must take in the actual death_event struct
	private void on_player_killed_general() {
		byte killeD_placeholder = 1;
		player_stats killed = get_player_stats(killeD_placeholder);
		killed.death_count++;
		get_team_stats(killed.teamID).death_count++;
		if (true) { // TODO - replace this with condition if there is a killer or not
			byte killeR_placeholder = 2;
			player_stats killer = get_player_stats(killeR_placeholder);
			killer.kill_count++;
		}
		on_player_killed();
	}

	// Should be called when you want to end the game (i.e. when the win con is met or time runs out)
	protected void end_game_general(bool timeout) {
		ingame_state.val = false;
		// TODO - some other stuff here, like the leaderboard
	}

	protected abstract void init(); // Initialize your controller values using the win_Con_Config
	protected abstract void on_game_start(); // Start the game
	protected abstract void on_player_killed(); // Called when a player_death_event is received, after general_stats is updated
	
}


// Stores a single player's stats for this match
public class player_stats {
	public byte playerID;
	public byte teamID;
	public int kill_count;
	public int death_count;
}

// Stores a single team's stats for this match
public class team_stats {
	public byte teamID;
	public byte kill_count;
	public byte death_count;
}
