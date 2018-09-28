using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun_On_Throw : MonoBehaviour {

	[SerializeField]
	float_event_object throw_trigger;

	[SerializeField]
	float_event_object stun_trigger;

	[SerializeField]
	readonly_gameplay_config readonly_gameplay_Config;

	
	void stun(float cooldown)
	{
		stun_trigger.Invoke(readonly_gameplay_Config.float_options[readonly_gameplay_float_option.dagger_stun_time]);
	}


	// Use this for initialization
	void Start () {
		throw_trigger.e.AddListener(stun);
	}

}
