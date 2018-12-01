using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(network_id))]
public class teammate_reveal : MonoBehaviour {

	[SerializeField]
	win_con_config win_Con_Config;

	[SerializeField]
	int_var local_id;

	[SerializeField]
	player_int teams;

	[SerializeField]
	int_float_event trigger;

	private network_id network_Id;


	private void Start() {
		if (win_Con_Config.bool_options[winCon_bool_option.free_for_all] || !win_Con_Config.bool_options[winCon_bool_option.teammates_revealed]) {
			return;
		}

		network_Id = GetComponent<network_id>();
		if (network_Id.val == local_id.val) {
			return;
		}

		if (teams[local_id.val] == teams[network_Id.val]) {
			trigger.Invoke(gameObject.GetComponent<network_id>().val, float.MaxValue);
		}
	}
}
