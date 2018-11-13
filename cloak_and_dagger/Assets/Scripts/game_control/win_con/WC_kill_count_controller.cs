using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_kill_count_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.kill_count; }
	}

	protected Dictionary<byte, player_kill_count_stats> player_stats_dict;
	protected Dictionary<byte, team_kill_count_stats> team_stats_dict;
	protected override player_stats get_player_stats(byte playerID) {
		return player_stats_dict[playerID];
	}
	protected override team_stats get_team_stats(byte teamID) {
		return team_stats_dict[teamID];
	}


	// This is called right when the scene is loaded, after the base Awake.
	// Use this for initialization instead of Awake.
	protected override void init() {
		player_stats_dict = new Dictionary<byte, player_kill_count_stats>();
		team_stats_dict = new Dictionary<byte, team_kill_count_stats>();

		foreach (byte player in WCAP.teams) {
			byte team = (byte)WCAP.teams[player];
			player_stats_dict.Add(player, new player_kill_count_stats(player, team));

			if (!team_stats_dict.ContainsKey(team)) {
				team_stats_dict.Add(team, new team_kill_count_stats(team));
			}
		}
	}

	// This is called when the game is started, at the end of the countdown
	protected override void on_game_start() {
	}

	// This is called when a player is killed,
	// AFTER the stats dicts are updated with kill & death counters
	protected override void on_player_killed(death_event_data death_data) {
		foreach (team_kill_count_stats team in team_stats_dict.Values) {
			if (team.kill_count > WCAP.win_Con_Config.int_options[winCon_int_option.kill_limit]) {
				end_game_general(false);
			}
		}
	}

}


// These are player stat and team stat classes for you to store additional win condition info
public class player_kill_count_stats : player_stats {
	public player_kill_count_stats(byte _playerID, byte _teamID) : base(_playerID, _teamID) {
	}
}

public class team_kill_count_stats : team_stats {
	public team_kill_count_stats(byte _teamID) : base(_teamID) {
	}
}
