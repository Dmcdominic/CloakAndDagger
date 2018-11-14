using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_assault_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.assault; }
	}

	public int_event_object payload_pickup;
	public int_event_object payload_dropped;
	public int_event_object payload_delivery;
	public event_object spawn_payload;

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
		if (payload_pickup) {
			payload_pickup.e.AddListener(on_payload_pickup);
		}
		if (payload_dropped) {
			payload_dropped.e.AddListener(on_payload_dropped);
		}
		if (payload_delivery) {
			payload_delivery.e.AddListener(on_payload_delivery);
		}

		player_stats_dict = new Dictionary<byte, player_assault_stats>();
		team_stats_dict = new Dictionary<byte, team_assault_stats>();

		foreach (byte player in WCAP.teams) {
			byte team = (byte)WCAP.teams[player];
			player_stats_dict.Add(player, new player_assault_stats(player, team));

			if (!team_stats_dict.ContainsKey(team)) {
				team_stats_dict.Add(team, new team_assault_stats(team));
			}
		}
	}

	// This is called when the game is started, at the end of the countdown
	protected override void on_game_start() {
		spawn_payload.Invoke();
	}

	// This is called when a player is killed,
	// AFTER the stats dicts are updated with kill & death counters
	protected override void on_player_killed(death_event_data death_data) {
	}

	private void on_payload_pickup(int playerID) {
		// Todo - update stats
	}

	private void on_payload_dropped(int playerID) {
		// Anything needed here?
	}

	private void on_payload_delivery(int playerID) {
		// todo - update stats
		StartCoroutine(spawn_payload_delayed());
	}

	IEnumerator spawn_payload_delayed() {
		yield return new WaitForSeconds(WCAP.win_Con_Config.float_options[winCon_float_option.payload_respawn_delay]);
		spawn_payload.Invoke();
		yield return null;
	}

}


// These are player stat and team stat classes for you to store additional win condition info
public class player_assault_stats : player_stats {
	int payload_pickups = 0;
	int payload_deliveries = 0;
	public player_assault_stats(byte _playerID, byte _teamID) : base(_playerID, _teamID) {
	}
}

public class team_assault_stats : team_stats {
	int payload_pickups = 0;
	int payload_deliveries = 0;
	public team_assault_stats(byte _teamID) : base(_teamID) {
	}
}
