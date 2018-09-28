using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum config_category { gameplay, readonly_gameplay }

public class save_and_load_presets : MonoBehaviour {

	// All editable configs, to be saved and loaded, should be serialized in this dictionary
	public ConfigCat_ScriptableObj_Dict editable_configs = new ConfigCat_ScriptableObj_Dict();


	public void save_preset(string preset_name) {
		preset config_data = new preset(preset_name, editable_configs);
		save_util.save_to<preset>("gamemode_presets", preset_name, config_data);
	}

	public void load_preset(string preset_name) {
		preset config_data;
		if (!save_util.try_load_from<preset>("gamemode_presets", preset_name, out config_data)) {
			Debug.Log("Failed to load preset: " + preset_name);
			return;
		}

		// TODO - copy the loaded config scriptable objects into the serialized scriptable objects
		//editable_configs[config_category.gameplay]
		//gameplay_config.copy_values()
	}

	public List<string> get_available_presets() {
		// TODO
		return null;
	}

}

[System.Serializable]
public class ConfigCat_ScriptableObj_Dict : SerializableDictionary<config_category, ScriptableObject> { }

[System.Serializable]
public class preset {
	string name;
	public Dictionary<config_category, ScriptableObject> config_data;
	public preset(string _name, Dictionary<config_category, ScriptableObject> _config_data) {
		this.name = _name;
		this.config_data = _config_data;
	}
}