using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun_On_Throw : MonoBehaviour {

	[SerializeField]
	float_event_object throw_trigger;

	[SerializeField]
	float_event_object stun_trigger;

	[SerializeField]
	dagger_config dagger_Config;

	
	void stun(float cooldown)
	{
		stun_trigger.Invoke(dagger_Config.stun_time);
	}


	// Use this for initialization
	void Start () {
		throw_trigger.e.AddListener(stun);
	}

}
