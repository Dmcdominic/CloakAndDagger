using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


[RequireComponent(typeof(network_id))]
public class On_Action_Button : MonoBehaviour {

	[SerializeField]
	input_config input_Config;

	[SerializeField]
	gameplay_config gameplay_Config;

	// Dagger
	[SerializeField]
	int_float_event to_trigger_dagger;
	[SerializeField]
	player_bool dagger_on_cooldown;

	// Fireball
	[SerializeField]
	int_float_event to_trigger_fireball;
	[SerializeField]
	player_bool fireball_on_cooldown;

	// Blink/dash
	[SerializeField]
	int_float_event to_trigger_dash;
	[SerializeField]
	player_bool dash_on_cooldown;

	// Reflect
	[SerializeField]
	int_float_event to_trigger_reflect_cooldown;
	[SerializeField]
	int_float_event to_trigger_reflect_time;
	[SerializeField]
	player_bool reflect_on_cooldown;

	[SerializeField]
	bool_var ingame_state;

    [SerializeField]
    int_var local_id;

	private network_id network_Id;


	private void Awake() {
		network_Id = GetComponent<network_id>();
	}

	// Update is called once per frame
	void Update () {
		if (network_Id.val == local_id.val && ingame_state.val && input_Config)
		{
			if (input_Config.dagger && !dagger_on_cooldown[network_Id.val])
			{
				to_trigger_dagger.Invoke(network_Id.val,gameplay_Config.float_options[gameplay_float_option.dagger_cooldown]);
			}
			else if (input_Config.fireball && !fireball_on_cooldown[network_Id.val])
			{
				to_trigger_fireball.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.fireball_cooldown]);
			}
			else if (input_Config.dash && !dash_on_cooldown[network_Id.val])
            {
                to_trigger_dash.Invoke(network_Id.val,gameplay_Config.float_options[gameplay_float_option.blink_cooldown]);
			}
			else if (input_Config.reflect && !reflect_on_cooldown[network_Id.val])
			{
				to_trigger_reflect_cooldown.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.reflect_cooldown]);
				to_trigger_reflect_time.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.reflect_time]);
			}
		}
	}
}
