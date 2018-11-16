using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_kill_count_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.kill_count; }
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
		foreach (team_stats team in team_stats_dict.Values) {
			if (team.kill_count > WCAP.win_Con_Config.int_options[winCon_int_option.kill_limit]) {
				end_game_general(false, team.teamID);
			}
		}
	}

	protected override void on_timeout() {
		List<byte> winning_teams = new List<byte>();
		int max_kill_count = 0;
		foreach (team_stats team in team_stats_dict.Values) {
			if (team.kill_count == max_kill_count) {
				winning_teams.Add(team.teamID);
			} else if (team.kill_count > max_kill_count) {
				winning_teams.Clear();
				winning_teams.Add(team.teamID);
				max_kill_count = team.kill_count;
			}
		}
		end_game_general(true, winning_teams);
	}

}
