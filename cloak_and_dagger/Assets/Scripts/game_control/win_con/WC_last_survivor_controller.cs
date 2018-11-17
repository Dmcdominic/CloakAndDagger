using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_last_survivor_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.last_survivor; }
	}
	public override bool free_for_all_compatible {
		get { return true; }
	}


	// This is called right when the scene is loaded, after the base Awake.
	// Use this for initialization instead of Awake.
	protected override void init() {
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

		player_stats killed = player_stats_dict[death_data.playerID];
		killed.lives_remaining--;
		team_stats_dict[killed.teamID].lives_remaining--;

		int remaining_teams = 0;
		List<byte> winning_teams = new List<byte>();
		foreach (team_stats team in team_stats_dict.Values) {
			if (team.lives_remaining > 0) {
				remaining_teams++;
				winning_teams.Add(team.teamID);
			}
		}
		if (remaining_teams <= 1) {
			end_game_general(winning_teams);
		}
	}

	protected override void on_timeout() {
		List<byte> winning_teams = new List<byte>();
		foreach (team_stats team in team_stats_dict.Values) {
			if (team.lives_remaining > 0) {
				winning_teams.Add(team.teamID);
			}
		}
		end_game_general(winning_teams);
	}

}
