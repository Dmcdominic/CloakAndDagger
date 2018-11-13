using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_assault_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.assault; }
	}

	protected Dictionary<byte, player_assault_stats> player_stats_dict;
	protected Dictionary<byte, team_assault_stats> team_stats_dict;
	protected override player_stats get_player_stats(byte playerID) {
		return player_stats_dict[playerID];
	}
	protected override team_stats get_team_stats(byte teamID) {
		return team_stats_dict[teamID];
	}


	// This is called right when the scene is loaded, after the base Awake.
	// Use this for initialization instead of Awake.
	protected override void init() {
		// TODO - intialize the player_stats_dict and team_stats_dict
		// based on info passed here from the lobby or host or whatever
		player_stats_dict = new Dictionary<byte, player_assault_stats>();
		team_stats_dict = new Dictionary<byte, team_assault_stats>();
	}

	// This is called when the game is started, at the end of the countdown
	protected override void on_game_start() {
	}

	// This is called when a player is killed,
	// AFTER the stats dicts are updated with kill & death counters
	protected override void on_player_killed(death_event_data death_data) {
	}

	private void on_bomb_pickup() {
		// Todo
	}

	private void on_bomb_delivered() {
		// Todo
	}

}


// These are player stat and team stat classes for you to store additional win condition info
public class player_assault_stats : player_stats {
	int bomb_pickups;
	int bomb_deliveries;
}

public class team_assault_stats : team_stats {
	int bomb_pickups;
	int bomb_deliveries;
}
