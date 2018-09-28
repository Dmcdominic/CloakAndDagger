using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum config_category { gameplay, readonly_gameplay }

public class save_and_load_configs : MonoBehaviour {

	// All editable configs, to be saved and loaded, should be serialized in this dictionary
	public ConfigCat_ScriptableObj_Dict editable_configs = new ConfigCat_ScriptableObj_Dict();


	public void save_all_config_data(string preset_name) {
		complete_config_data config_data = new complete_config_data();
		config_data.configs_dict = editable_configs;
	}

	public void load_all_config_data(string preset_name) {

	}

}

[System.Serializable]
public class ConfigCat_ScriptableObj_Dict : SerializableDictionary<config_category, ScriptableObject> { }

[System.Serializable]
public class complete_config_data {
	public Dictionary<config_category, ScriptableObject> configs_dict;
}