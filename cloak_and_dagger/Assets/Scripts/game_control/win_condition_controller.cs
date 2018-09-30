using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class win_condition_controller : MonoBehaviour {
	public abstract win_condition win_Condition { get; }

	public win_con_config win_Con_Config;

	// Events to listen to
	public event_object game_start_trigger;
	public int_event_object player_killed; // TODO - should this be some kind of kill_data struct, that includes HOW they died, WHO killed them, etc.

	// Events to trigger
	public event_object trigger_on_game_over; // TODO - this needs to store some kind of game_over_data

	// Game variables
	private float timer = 0;
	private float time_limit;

	// TODO - should these player stat dictionaries be combined into one, that just stores some kind of player_stats struct for each player?
	// Should the player objects just store them themselves? This would make leaderboards and stuff really trick though...
	private Dictionary<int, int> player_kill_count; // TODO - Should the keys be ints, for player ID's? Or is there an abstracted datatype for player ID?
	private Dictionary<int, int> player_death_count;


	// Initialization
	protected void Awake() {
		if (win_Con_Config.win_Condition != win_Condition) {
			gameObject.SetActive(false);
			return;
		}

		time_limit = win_Con_Config.float_options[winCon_float_option.time_limit];

		if (game_start_trigger) {
			game_start_trigger.e.AddListener(on_game_start);
		}
	}
	
	protected void Update () {
		timer += Time.deltaTime;
		if (time_limit != 0 && time_limit < timer) {
			end_game();
		}
	}

	protected abstract void on_game_start();
	protected abstract void end_game(); // TODO - needs to account for time limit end, and other versions of game ending?

}
