using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class On_Action_Button : NetworkBehaviour {

	[SerializeField]
	input_config input_Config;

	[SerializeField]
	float_event_object to_trigger_dagger;
	[SerializeField]
	bool_var dagger_on_cooldown;
	[SerializeField]
	dagger_config dagger_Config;

	[SerializeField]
	float_event_object to_trigger_dash;
	[SerializeField]
	bool_var dash_on_cooldown;
	[SerializeField]
	dash_config dash_Config;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer && input_Config)
		{
			if (input_Config.dagger && !dagger_on_cooldown.val)
			{
				to_trigger_dagger.Invoke(dagger_Config.cooldown);
			}
			else if (input_Config.dash && !dash_on_cooldown.val)
			{
				to_trigger_dash.Invoke(dash_Config.cooldown);
			}
		}
	}
}
