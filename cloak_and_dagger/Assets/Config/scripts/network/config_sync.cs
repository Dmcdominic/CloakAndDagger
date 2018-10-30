using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class config_sync : MonoBehaviour {

	public ConfigCat_ScriptableObj_Dict editable_configs = new ConfigCat_ScriptableObj_Dict();

	public bool_var host;

	public event_object trigger_out;
	public sync_event out_event;
	public sync_event in_event;

	private void Awake() {
		if (in_event) {
			in_event.e.AddListener(sync_incoming_config);
		}
		if (trigger_out) {
			trigger_out.e.AddListener(send_config);
		}
	}

	private void sync_incoming_config(float t, object state, int config_cat_int) {
		Debug.Log("Reached sync_incoming_config()");
		switch ((config_category)config_cat_int) {
			case config_category.map:
				((map_config)editable_configs[config_category.map]).copy_from_syncable((map_syncable_config)state);
				break;
			case config_category.win_con:
				((win_con_config)editable_configs[config_category.win_con]).copy_from_syncable((win_con_syncable_config)state);
				break;
			case config_category.gameplay:
				((gameplay_config)editable_configs[config_category.gameplay]).copy_from_syncable((gameplay_syncable_config)state);
				break;
			default:
				Debug.LogError("config_cat_int received (" + config_cat_int + ") does not apply to a compatible config category");
				break;
		}
	}

	private void send_config() {
		Debug.Log("Reached send_config()");
		if (!host.val) {
			Debug.Log("Returning, though, because you're not the host");
			return;
		}

		// Send map config
		Debug.Log("Sending map config");
		map_syncable_config map_syncable = new map_syncable_config((map_config)editable_configs[config_category.map]);
		out_event.Invoke(0, map_syncable, (int)config_category.map, large: true);

		// Send win_con config
		Debug.Log("Sending win_con config");
		win_con_syncable_config win_con_syncable = new win_con_syncable_config((win_con_config)editable_configs[config_category.win_con]);
		out_event.Invoke(0, win_con_syncable, (int)config_category.win_con, large: true);

		// Send gameplay config
		Debug.Log("Sending gameplay config");
		gameplay_syncable_config gameplay_syncable = new gameplay_syncable_config((gameplay_config)editable_configs[config_category.gameplay]);
		out_event.Invoke(0, gameplay_syncable, (int)config_category.gameplay, large: true);

		Debug.Log("Completed all 3 config sends");
	}

}
