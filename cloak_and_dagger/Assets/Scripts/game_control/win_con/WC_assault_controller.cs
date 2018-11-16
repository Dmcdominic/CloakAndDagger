using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_assault_controller : win_condition_controller {
	public override win_condition win_Condition {
		get { return win_condition.assault; }
	}
	public override bool free_for_all_compatible {
		get { return false; }
	}

	public int_event_object payload_pickup;
	public int_event_object payload_dropped;
	public int_event_object payload_delivery;
	public event_object spawn_payload;


	// This is called right when the scene is loaded, after the base Awake.
	// Use this for initialization instead of Awake.
	protected override void init() {
		// Set up payload event listeners
		if (payload_pickup) {
			payload_pickup.e.AddListener(on_payload_pickup);
		}
		if (payload_dropped) {
			payload_dropped.e.AddListener(on_payload_dropped);
		}
		if (payload_delivery) {
			payload_delivery.e.AddListener(on_payload_delivery);
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
		player_stats player = player_stats_dict[(byte)playerID];
		player.payload_pickups++;
		team_stats_dict[player.teamID].payload_pickups++;
	}

	private void on_payload_dropped(int playerID) {
		// Anything needed here?
	}

	private void on_payload_delivery(int playerID) {
		player_stats player = player_stats_dict[(byte)playerID];
		player.payload_deliveries++;
		team_stats team = team_stats_dict[player.teamID];
		team.payload_deliveries++;

		int payload_delivery_limit = WCAP.win_Con_Config.int_options[winCon_int_option.payload_delivery_limit];
		if (team.payload_deliveries >= payload_delivery_limit) {
			end_game_general(false, team.teamID);
		}

		StartCoroutine(spawn_payload_delayed());
	}

	IEnumerator spawn_payload_delayed() {
		yield return new WaitForSeconds(WCAP.win_Con_Config.float_options[winCon_float_option.payload_respawn_delay]);
		spawn_payload.Invoke();
		yield return null;
	}

	protected override void on_timeout() {
		List<byte> winning_teams = new List<byte>();
		int max_deliveries = 0;
		foreach (team_stats team in team_stats_dict.Values) {
			if (team.payload_deliveries == max_deliveries) {
				winning_teams.Add(team.teamID);
			} else if (team.payload_deliveries > max_deliveries) {
				winning_teams.Clear();
				winning_teams.Add(team.teamID);
				max_deliveries = team.payload_deliveries;
			}
		}
		end_game_general(true, winning_teams);
	}

}
