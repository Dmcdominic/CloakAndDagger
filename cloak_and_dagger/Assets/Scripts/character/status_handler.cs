using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


public enum status {stun, revealed}

public class status_handler : NetworkBehaviour {

	[SerializeField]
	Status_BoolVar_Dict stats = new Status_BoolVar_Dict();

	[SerializeField]
	Status_FloatEventObject_Dict on_triggers = new Status_FloatEventObject_Dict();

	[SerializeField]
	Status_EventObject_Dict off_triggers = new Status_EventObject_Dict();

	[SerializeField]
	Status_Float_Dict times = new Status_Float_Dict();


	public UnityAction<float> set_status(status stat)
	{
		return duration => {times[stat] = Mathf.Max(times[stat],duration);};
	}

	// Use this for initialization
	void Start () {
		foreach(status stat in stats.Keys)
		{
			on_triggers[stat].e.AddListener(set_status(stat));
			off_triggers[stat].e.AddListener(reset_status(stat));
		}
	}
	

	public UnityAction reset_status(status stat)
	{
		return () => {times[stat] = 0;};
	}



	// Update is called once per frame
	void Update () {
		foreach(status stat in stats.Keys)
		{
			if(times[stat] > 0) 
			{
				times[stat] -= Time.deltaTime;
				stats[stat].val = true;
			}
			else 
			{
				stats[stat].val = false;
			}
		}
	}
}
