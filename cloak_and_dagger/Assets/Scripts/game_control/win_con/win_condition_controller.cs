using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class win_condition_controller : MonoBehaviour {
	public abstract win_condition win_Condition { get; }
	public abstract bool free_for_all_compatible { get; }

	//[HideInInspector]
	public win_condition_assets_packet WCAP;

	protected Dictionary<byte, player_stats> player_stats_dict;
	protected Dictionary<byte, team_stats> team_stats_dict;

	private float time_limit;
#pragma warning disable 0414
	private bool timed_out = false;


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
	
	// Manages the game timer.
	// If you want to use update, make sure to call base.Update() as well.
	protected void Update () {
		if (WCAP.ingame_state.val) {
			WCAP.game_timer.val += Time.deltaTime;
			if (time_limit != 0 && time_limit < WCAP.game_timer.val) {
				timed_out = true;
				on_timeout();
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
		player_stats killed = player_stats_dict[death_data.playerID];
		killed.death_count++;
		team_stats_dict[killed.teamID].death_count++;
		if (death_data.death_Type != death_type.suicide) {
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
