using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun_On_Throw : MonoBehaviour {

	[SerializeField]
	event_object throw_trigger;

	[SerializeField]
	float_event_object stun_trigger;

	[SerializeField]
	float time = 1;

	
	void stun()
	{
		stun_trigger.Invoke(time);
	}


	// Use this for initialization
	void Start () {
		throw_trigger.e.AddListener(stun);
	}

}
