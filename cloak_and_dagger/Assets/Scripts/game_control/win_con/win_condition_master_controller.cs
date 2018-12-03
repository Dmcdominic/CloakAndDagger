using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win_condition_master_controller : MonoBehaviour {

	public float_event_object start_in;
	public win_condition_assets_packet WCAP;
	public WinCon_Controller_Dict WC_controller_prefabs;


	private void Awake() {
		DontDestroyOnLoad(gameObject);

		if (start_in) {
			start_in.e.AddListener(on_start_in);
		}
	}

	private void on_start_in(float placeholder) {
		win_condition_controller new_WC_controller = Instantiate(WC_controller_prefabs[WCAP.win_Con_Config.win_Condition]);
		new_WC_controller.transform.SetParent(transform);

		new_WC_controller.WCAP = WCAP;
		new_WC_controller.in_event = WCAP.in_event;
		new_WC_controller.out_event = WCAP.out_event;
		new_WC_controller.local_id = WCAP.local_id;
		new_WC_controller.t0 = WCAP.t0;

		new_WC_controller.gameObject.SetActive(true);
	}

}

[System.Serializable]
public class WinCon_Controller_Dict : SerializableDictionary<win_condition, win_condition_controller> { }
