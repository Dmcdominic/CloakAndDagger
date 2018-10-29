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

	private void sync_incoming_config(float t, object state, int placeholder) {
		Debug.Log("Reached sync_incoming_config()");
		preset loaded_preset = new preset((string)state);
		if (!save_and_load_presets.copy_all_editable_configs(loaded_preset, editable_configs)) {
			Debug.LogError("Failed to overwrite a config dictionary from its json.");
		}
	}

	private void send_config() {
		Debug.Log("Reached send_config()");
		if (!host.val) {
			Debug.Log("Returning, though, because you're not the host");
			return;
		}

		ConfigCat_String_Dict config_jsons = new ConfigCat_String_Dict();

		try {
			foreach (config_category config_cat in editable_configs.Keys) {
				Debug.Log("Adding category: " + config_cat + "to the json which will be sent");
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
