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
	float_event_object to_trigger_dagger;
	[SerializeField]
	bool_var dagger_on_cooldown;

	[SerializeField]
	float_event_object to_trigger_dash;
	[SerializeField]
	bool_var dash_on_cooldown;
	[SerializeField]
	gameplay_config gameplay_Config;

	[SerializeField]
	bool_var ingame_state;

    [SerializeField]
    int_var local_id;
	
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<network_id>().val == local_id.val && ingame_state.val && input_Config)
		{
			if (input_Config.dagger && !dagger_on_cooldown.val)
			{
				to_trigger_dagger.Invoke(gameplay_Config.float_options[gameplay_float_option.dagger_cooldown]);
			}
			else if (input_Config.dash && !dash_on_cooldown.val)
			{
				to_trigger_dash.Invoke(gameplay_Config.float_options[gameplay_float_option.dash_cooldown]);
			}
		}
	}
}
