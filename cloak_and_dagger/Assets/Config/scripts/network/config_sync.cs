using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class config_sync : MonoBehaviour {

	public ConfigCat_ScriptableObj_Dict editable_configs = new ConfigCat_ScriptableObj_Dict();

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

	private void sync_incoming_config(float t, object state, int placeholder) {
		preset loaded_preset = new preset((string)state);
		try {
			foreach (config_category config_cat in loaded_preset.config_jsons.Keys) {
				JsonUtility.FromJsonOverwrite(loaded_preset.config_jsons[config_cat], editable_configs[config_cat]);
			}
		} catch {
			Debug.LogError("Failed to overwrite a config dictionary from its json.");
			return;
		}
	}

	private void send_config() {
		ConfigCat_String_Dict config_jsons = new ConfigCat_String_Dict();

		try {
			foreach (config_category config_cat in editable_configs.Keys) {
				config_jsons.Add(config_cat, JsonUtility.ToJson(editable_configs[config_cat]));
			}
			preset new_preset = new preset("current settings", config_jsons);
			string data_json = JsonUtility.ToJson(new_preset);
			out_event.Invoke(0, data_json, 0);
			// Todo - reliable = false?
		} catch {
			Debug.LogError("Failed to send config");
			return;
		}
	}

}
