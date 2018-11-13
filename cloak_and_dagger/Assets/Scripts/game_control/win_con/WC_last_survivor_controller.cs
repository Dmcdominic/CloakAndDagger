using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_last_survivor_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.last_survivor; }
	}

	protected Dictionary<byte, player_last_survivor_stats> player_stats_dict;
	protected Dictionary<byte, team_last_survivor_stats> team_stats_dict;
	protected override player_stats get_player_stats(byte playerID) {
		return player_stats_dict[playerID];
	}
	protected override team_stats get_team_stats(byte teamID) {
		return team_stats_dict[teamID];
	}


	// This is called right when the scene is loaded, after the base Awake.
	// Use this for initialization instead of Awake.
	protected override void init() {
		int starting_lives = WCAP.win_Con_Config.int_options[winCon_int_option.lives];

		player_stats_dict = new Dictionary<byte, player_last_survivor_stats>();
		team_stats_dict = new Dictionary<byte, team_last_survivor_stats>();

		foreach (byte player in WCAP.teams) {
			byte team = (byte)WCAP.teams[player];
			player_stats_dict.Add(player, new player_last_survivor_stats(player, team, starting_lives));

			if (team_stats_dict.ContainsKey(team)) {
				team_stats_dict[team].lives_remaining += starting_lives;
			} else {
				team_stats_dict.Add(team, new team_last_survivor_stats(team, starting_lives));
			}
		}
	}

	// This is called when the game is started, at the end of the countdown
	protected override void on_game_start() {
	}

	// This is called when a player is killed,
	// AFTER the stats dicts are updated with kill & death counters
	protected override void on_player_killed(death_event_data death_data) {
		if (WCAP.win_Con_Config.int_options[winCon_int_option.lives] == 0) {
			return;
		}

		player_last_survivor_stats killed = player_stats_dict[(byte)death_data.playerID];
		killed.lives_remaining--;
		team_stats_dict[killed.teamID].lives_remaining--;

		int remaining_teams = 0;
		foreach (team_last_survivor_stats team in team_stats_dict.Values) {
			if (team.lives_remaining > 0) {
				remaining_teams++;
			}
		}
		if (remaining_teams <= 1) {
			end_game_general(false);
		}
	}

}


// These are player stat and team stat classes for you to store additional win condition info
public class player_last_survivor_stats : player_stats {
	public int lives_remaining;
	public player_last_survivor_stats(byte _playerID, byte _teamID, int starting_lives) : base(_playerID, _teamID) {
		lives_remaining = starting_lives;
	}
}

public class team_last_survivor_stats : team_stats {
	public int lives_remaining;
	public team_last_survivor_stats(byte _teamID, int starting_lives_total) : base(_teamID) {
		lives_remaining = starting_lives_total;
	}
}
