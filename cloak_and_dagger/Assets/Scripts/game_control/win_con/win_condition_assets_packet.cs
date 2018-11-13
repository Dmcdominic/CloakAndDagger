using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="WCAP")]
public class win_condition_assets_packet : ScriptableObject {

	public win_con_config win_Con_Config;
	public gameplay_config gameplay_Config;

	// Events to listen to
	public event_object done_initing;
	public death_event_object player_killed;

	// Events to trigger
	public int_event_object countdown_event;
	public event_object trigger_on_game_start;
	public event_object trigger_on_game_over; // TODO - this needs to send some kind of game_over_data?

	// Player-Team data
	public player_int teams;

	// Game variables
	public bool_var ingame_state;

	public float_var game_timer;

}
