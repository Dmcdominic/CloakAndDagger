using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="WCAP")]
public class win_condition_assets_packet : ScriptableObject {

	// Config objects
	public win_con_config win_Con_Config;
	public gameplay_config gameplay_Config;
	public readonly_gameplay_config readonly_Gameplay_Config;

	// sync_events
	public sync_event in_event;
	public sync_event out_event;

	// Local player info
	public bool_var host;

	// Events to listen to
	public event_object done_initing;
	public death_event_object player_killed;

	// Events to trigger
	public int_event_object countdown_event;
	public event_object trigger_on_game_start;
	public event_object trigger_on_game_over;

	// Player stats and team stats SO
	public game_stats game_Stats;

	// Player-Team data
	public player_int teams;

	// Game variables
	public bool_var ingame_state;

	// Game timer - counts up from 0
	public float_var game_timer;

}
