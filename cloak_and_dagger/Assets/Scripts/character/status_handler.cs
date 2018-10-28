using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


public enum status {stun, dagger_on_cooldown, dash_on_cooldown}

public class status_handler : MonoBehaviour {

	[SerializeField]
	Status_BoolVar_Dict stats = new Status_BoolVar_Dict();

	[SerializeField]
	Status_FloatEventObject_Dict on_triggers = new Status_FloatEventObject_Dict();

	[SerializeField]
	Status_EventObject_Dict off_triggers = new Status_EventObject_Dict();

	Status_Float_Dict times = new Status_Float_Dict();



	// Use this for initialization
	void Start () {
		init_times();
		foreach(status stat in stats.Keys)
		{
			if (on_triggers.ContainsKey(stat) && on_triggers[stat]) {
				on_triggers[stat].e.AddListener(set_status(stat));
			}
			if (off_triggers.ContainsKey(stat) && off_triggers[stat]) {
				off_triggers[stat].e.AddListener(reset_status(stat));
			}
		}
	}

	private void init_times() {
		foreach (status stat in stats.Keys) {
			times.Add(stat, 0);
		}
	}

	public UnityAction<float> set_status(status stat) {
		return duration => { times[stat] = Mathf.Max(times[stat], duration); };
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
