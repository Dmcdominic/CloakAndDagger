using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


public enum status {stun, revealed}

public class status_handler : NetworkBehaviour {

	[System.Serializable]
	public class status_dict : SerializableDictionary<status,bool_var> {}

	[SerializeField]
	status_dict stats = new status_dict();

	[System.Serializable]
	public class float_event_dict : SerializableDictionary<status,float_event_object> {}

	[SerializeField]
	float_event_dict on_triggers = new float_event_dict();


	[System.Serializable]
	public class event_dict : SerializableDictionary<status,event_object> {}

	[SerializeField]
	event_dict off_triggers = new event_dict();

	[System.Serializable]
	public class float_dict : SerializableDictionary<status,float> {}

	[SerializeField]
	float_dict times = new float_dict();


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
