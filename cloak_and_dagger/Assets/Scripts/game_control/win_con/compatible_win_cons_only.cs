using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compatible_win_cons_only : MonoBehaviour {

	public win_con_config win_Con_Config;

	public List<win_condition> compatible_win_cons;


	private void Awake() {
		if (!compatible_win_cons.Contains(win_Con_Config.win_Condition)) {
			Destroy(gameObject);
		}
	}

}
