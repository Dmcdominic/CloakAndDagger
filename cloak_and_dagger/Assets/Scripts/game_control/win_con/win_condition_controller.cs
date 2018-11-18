using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class win_condition_controller : MonoBehaviour {
	public abstract win_condition win_Condition { get; }

	[HideInInspector]
	public win_condition_assets_packet WCAP;
	
	private float time_limit;

	protected abstract player_stats get_player_stats(byte playerID);
	protected abstract team_stats get_team_stats(byte teamID);


	// Initialization
	private void Awake() {
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
		
		WCAP.ingame_state.val = false;
		WCAP.game_timer.val = 0;
		time_limit = WCAP.win_Con_Config.float_options[winCon_float_option.time_limit];
		init();
	}
	
	// Manages the game timer.
	// If you want to use update, make sure to call base.Update() as well.
	protected void Update () {
		if (WCAP.ingame_state.val) {
			WCAP.game_timer.val += Time.deltaTime;
			if (time_limit != 0 && time_limit < WCAP.game_timer.val) {
				end_game_general(true);
			}
		}
	}

	private void on_done_initing() {
		StartCoroutine(starting_countdown());
	}

	IEnumerator starting_countdown() {
		int seconds_left = 3;
		while (seconds_left > 0) {
			WCAP.countdown_event.Invoke(seconds_left);
			yield return new WaitForSeconds(1);
			seconds_left--;
		}
		on_game_start_general();
		yield return null;
	}

	// Is called at the end of the countdown
	private void on_game_start_general() {
		WCAP.ingame_state.val = true;
		WCAP.trigger_on_game_start.Invoke();
		on_game_start();
	}

	// Is called by the player_killed event
	private void on_player_killed_general(death_event_data death_data) {
		player_stats killed = get_player_stats(death_data.playerID);
		killed.death_count++;
		get_team_stats(killed.teamID).death_count++;
		if (death_data.death_Type != death_type.suicide) {
			player_stats killer = get_player_stats(death_data.killerID);
			killer.kill_count++;
		}
		on_player_killed(death_data);
	}

	protected abstract void init(); // Initialize your controller values using the win_Con_Config
	protected abstract void on_game_start(); // Start the game

	// This will be called when a player_death_event is received,
	// AFTER updating the player_stats and team_stats with basic kill and death counter increments
	protected abstract void on_player_killed(death_event_data death_data);

	// Should be called when you want to end the game (i.e. when the win con is met or time runs out)
	// Note that this may be called by a timeout, so if you need anything to occur before the game ends,
	// You should override this and then call base.end_game_general(timeout) at the end.
	protected void end_game_general(bool timeout) {
		WCAP.ingame_state.val = false;
		WCAP.trigger_on_game_over.Invoke();
		// TODO - some other stuff here, like the leaderboard?
		// Or that stuff can simply listen for the game_over event.
		Destroy(gameObject);
	}
}


// Stores a single player's stats for this match
public class player_stats {
	public byte playerID;
	public byte teamID;
	public int kill_count = 0;
	public int death_count = 0;
	public player_stats(byte _playerID, byte _teamID) {
		playerID = _playerID;
		teamID = _teamID;
	}
}

// Stores a single team's stats for this match
public class team_stats {
	public byte teamID;
	public byte kill_count = 0;
	public byte death_count = 0;
	public team_stats(byte _teamID) {
		teamID = _teamID;
	}
}
