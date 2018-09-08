using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class On_Action_Button : MonoBehaviour {

	[SerializeField]
	input_config config;

	[SerializeField]
	event_object to_trigger;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(config.dagger)
		{
			to_trigger.Invoke();
		}
		
	}
}
