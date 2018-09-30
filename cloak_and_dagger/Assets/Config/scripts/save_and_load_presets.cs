using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_and_load_presets : MonoBehaviour {

	private static string presets_subpath = "gamemode_presets/";

	// Events to trigger the saving or loading of a preset
	public string_event_object save_trigger;
	public string_event_object load_trigger;

	// All editable configs, to be saved and loaded, should be serialized in this dictionary
	public ConfigCat_ScriptableObj_Dict editable_configs = new ConfigCat_ScriptableObj_Dict();


	private void Awake() {
		if (save_trigger) {
			save_trigger.e.AddListener(save_preset);
		}
		if (load_trigger) {
			load_trigger.e.AddListener(load_preset);
		}
	}

	private void Start() {
		// FOR TESTING
		// TODO - REMOVE
		//save_preset("test_preset");
		//get_available_presets();
		//load_preset("test_preset");
	}

	public void save_preset(string preset_name) {
		ConfigCat_String_Dict config_jsons = new ConfigCat_String_Dict();

		foreach (config_category config_cat in editable_configs.Keys) {
			config_jsons.Add(config_cat, JsonUtility.ToJson(editable_configs[config_cat]));
		}
		preset new_preset = new preset(preset_name, config_jsons);
		save_util.save_to_JSON(presets_subpath, preset_name, new_preset);
	}

	public void load_preset(string preset_name) {
		preset loaded_preset;
		if (!save_util.try_load_from_JSON<preset>(presets_subpath, preset_name, out loaded_preset)) {
			Debug.Log("Failed to load preset: " + preset_name);
			// TODO - ingame feedback on failed load
			return;
		}

		print("Preset load success! You loaded: " + loaded_preset.name);

		foreach (config_category config_cat in editable_configs.Keys) {
			JsonUtility.FromJsonOverwrite(loaded_preset.config_jsons[config_cat], editable_configs[config_cat]);
		}
	}

	// Returns the full list of all presets saved in the gamemode_presets folder
	public static List<string> get_available_presets() {
		return save_util.get_files_in_dir(presets_subpath);
	}

}


[System.Serializable]
public class preset {
	public string name;
	public ConfigCat_String_Dict config_jsons;
	public preset(string _name, ConfigCat_String_Dict _config_jsons) {
		this.name = _name;
		this.config_jsons = _config_jsons;
	}
}


[System.Serializable]
public class ConfigCat_ScriptableObj_Dict : SerializableDictionary<config_category, ScriptableObject> { }
[System.Serializable]
public class ConfigCat_String_Dict : SerializableDictionary<config_category, string> { }
