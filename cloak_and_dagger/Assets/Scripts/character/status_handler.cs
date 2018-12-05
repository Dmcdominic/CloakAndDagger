using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


public enum status {stun, dagger_on_cooldown, dash_on_cooldown, reflect_on_cooldown, dead, reflecting, fireball_on_cooldown, torch_on_cooldown, trap_on_cooldown, trapped, revealed}

public class status_handler : MonoBehaviour {

	[SerializeField]
    Status_BoolVar_Dict stats = new Status_BoolVar_Dict();

	[SerializeField]
	Status_FloatEventObject_Dict on_triggers = new Status_FloatEventObject_Dict();

	[SerializeField]
	Status_EventObject_Dict off_triggers = new Status_EventObject_Dict();

	[SerializeField]
	Status_FloatVar_Dict local_cooldowns = new Status_FloatVar_Dict();

	[SerializeField]
	int_var local_id;

	[SerializeField]
	win_con_config win_Con_Config;

	[SerializeField]
	game_stats game_Stats;

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
			times.Add(stat, ScriptableObject.CreateInstance<player_float>());
		}
	}

	public UnityAction<int,float> set_status(status stat) {
		return (id,duration) => { times[stat][id] = Mathf.Max(times[stat][id], duration);};
	}

	public UnityAction<int> reset_status(status stat)
	{
		return id => {times[stat][id] = 0;};
	}



	// Update is called once per frame
	void Update () {
		foreach(status stat in stats.Keys)
		{
			for (int id = 0; id < times[stat].Count; id++) {
				if (times[stat][id] > 0) {
					times[stat][id] -= Time.deltaTime;
					stats[stat][id] = true;
				} else if (stat == status.dead) {
					if (!(win_Con_Config.win_Condition == win_condition.last_survivor) || game_Stats.player_Stats[(byte)id].lives_remaining > 0) {
						stats[stat][id] = false;
					}
				} else {
					stats[stat][id] = false;
				}
            }
			if (local_cooldowns.ContainsKey(stat) && local_cooldowns[stat] != null) {
				local_cooldowns[stat].val = times[stat][local_id.val];
			}
		}
	}
}
