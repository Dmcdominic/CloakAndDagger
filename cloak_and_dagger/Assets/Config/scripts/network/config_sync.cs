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
		int type = ((int)t) % 3;
		int enum_index = ((int)t) / 3;
		switch ((config_category)config_cat_int) {
			case config_category.map:
				Debug.Log("Received a map config value");
				if (type == 0) {
					((map_config)editable_configs[config_category.map]).bool_options[(map_bool_option)enum_index] = (bool)state;
				} else if (type == 1) {
					((map_config)editable_configs[config_category.map]).float_options[(map_float_option)enum_index] = (float)state;
				} else if (type == 2) {
					((map_config)editable_configs[config_category.map]).int_options[(map_int_option)enum_index] = (int)state;
				}
				break;
			case config_category.win_con:
				Debug.Log("Received a win_con config value");
				if (type == 0) {
					((win_con_config)editable_configs[config_category.win_con]).bool_options[(winCon_bool_option)enum_index] = (bool)state;
				} else if (type == 1) {
					((win_con_config)editable_configs[config_category.win_con]).float_options[(winCon_float_option)enum_index] = (float)state;
				} else if (type == 2) {
					((win_con_config)editable_configs[config_category.win_con]).int_options[(winCon_int_option)enum_index] = (int)state;
				}
				break;
			case config_category.gameplay:
				Debug.Log("Received a gameplay config value");
				if (type == 0) {
					((gameplay_config)editable_configs[config_category.gameplay]).bool_options[(gameplay_bool_option)enum_index] = (bool)state;
				} else if (type == 1) {
					((gameplay_config)editable_configs[config_category.gameplay]).float_options[(gameplay_float_option)enum_index] = (float)state;
				} else if (type == 2) {
					((gameplay_config)editable_configs[config_category.gameplay]).int_options[(gameplay_int_option)enum_index] = (int)state;
				}
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
		map_config map_Config = (map_config)(editable_configs[config_category.map]);
		foreach (map_bool_option bool_Option in map_Config.bool_options.Keys) {
			out_event.Invoke(((int)bool_Option) * 3, map_Config.bool_options[bool_Option], (int)config_category.map);
		}
		foreach (map_float_option float_Option in map_Config.float_options.Keys) {
			out_event.Invoke(1 + ((int)float_Option) * 3, map_Config.float_options[float_Option], (int)config_category.map);
		}
		foreach (map_int_option int_Option in map_Config.int_options.Keys) {
			out_event.Invoke(2 + ((int)int_Option) * 3, map_Config.int_options[int_Option], (int)config_category.map);
		}

		// Send win_con config
		Debug.Log("Sending win_con config");
		win_con_config win_con_Config = (win_con_config)(editable_configs[config_category.win_con]);
		foreach (winCon_bool_option bool_Option in win_con_Config.bool_options.Keys) {
			out_event.Invoke(((int)bool_Option) * 3, win_con_Config.bool_options[bool_Option], (int)config_category.win_con);
		}
		foreach (winCon_float_option float_Option in win_con_Config.float_options.Keys) {
			out_event.Invoke(1 + ((int)float_Option) * 3, win_con_Config.float_options[float_Option], (int)config_category.win_con);
		}
		foreach (winCon_int_option int_Option in win_con_Config.int_options.Keys) {
			out_event.Invoke(2 + ((int)int_Option) * 3, win_con_Config.int_options[int_Option], (int)config_category.win_con);
		}

		// Send gameplay config
		Debug.Log("Sending gameplay config");
		gameplay_config gameplay_Config = (gameplay_config)(editable_configs[config_category.gameplay]);
		foreach (gameplay_bool_option bool_Option in gameplay_Config.bool_options.Keys) {
			out_event.Invoke(((int)bool_Option) * 3, gameplay_Config.bool_options[bool_Option], (int)config_category.gameplay);
		}
		foreach (gameplay_float_option float_Option in gameplay_Config.float_options.Keys) {
			out_event.Invoke(1 + ((int)float_Option) * 3, gameplay_Config.float_options[float_Option], (int)config_category.gameplay);
		}
		foreach (gameplay_int_option int_Option in gameplay_Config.int_options.Keys) {
			out_event.Invoke(2 + ((int)int_Option) * 3, gameplay_Config.int_options[int_Option], (int)config_category.gameplay);
		}

		Debug.Log("Completed all 3 config sends");
	}

}
