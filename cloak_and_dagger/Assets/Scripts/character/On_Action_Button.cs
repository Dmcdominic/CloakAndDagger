using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class On_Action_Button : NetworkBehaviour {

	[SerializeField]
	input_config config;

	[SerializeField]
	event_object to_trigger_dagger;

	[SerializeField]
	event_object to_trigger_dash;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer && config)
		{
			if (config.dagger)
			{
				to_trigger_dagger.Invoke();
			}
			else if (config.dash)
			{
				to_trigger_dash.Invoke();
			}
		}
	}
}
