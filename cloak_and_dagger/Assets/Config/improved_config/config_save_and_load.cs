using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	
 *	config_save_and_load:
 *	A tool for saving and loading config presets to the local disk.
 *	Created by Dominic Calkosz.
 */
public class config_save_and_load : MonoBehaviour {

	// The directory within the standard persistent data path where config presets will be saved.
	private static string presets_subpath = "config/";

	// Toggle for debug log statements.
	public bool debug = true;

	// The config object that should be saved from and loaded to.
	public config_obj config;

	// Events to trigger the saving or loading of a preset.
	public event_object save_trigger;
	public string_var preset_name_to_save;
	public event_object load_trigger;
	public string_var preset_name_to_load;
	
	// This event, if added, will be invoked whenever a config preset is loaded,
	// indicating that the updated config should be synced over the network.
	public event_object sync_config_event;

	// json TextAssets can be included and serialized here,
	// in order to load them onto the disk during initialization.
	public json_list jsons_to_preload;


	// Initialization
	private void Awake() {
		DontDestroyOnLoad(gameObject);
		if (save_trigger) {
			save_trigger.e.AddListener(save_preset);
		}
		if (load_trigger) {
			load_trigger.e.AddListener(load_preset);
		}
	}

	private void Start () {
		// Preload all built-in presets.
		if (jsons_to_preload) {
			foreach (TextAsset preset in jsons_to_preload.jsons) {
				save_util.save_to_JSON(presets_subpath, preset.name, preset.text);
			}
		}
		// In build, the first (alphabetically) avaliable preset will be loaded by default.
		#if !UNITY_EDITOR
			load_preset(get_available_presets()[0], false);
		#endif
	}

	// Saves a config preset to the local disk.
	public void save_preset(string preset_name) {
		try {
			save_util.save_to_JSON(presets_subpath, preset_name, config);
		} catch {
			output_result(true, false, preset_name);
			return;
		}

		output_result(true, true, preset_name);
	}
	public void save_preset() {
		save_preset(preset_name_to_save.val);
	}

	// Loads a config preset from the local disk.
	// If sync is true, then the sync_config_event will be invoked after a successful load.
	public void load_preset(string preset_name, bool sync = true) {
		if (!save_util.try_load_from_JSON<config_obj>(presets_subpath, preset_name, out config)) {
			output_result(false, false, preset_name);
			return;
		}

		if (sync) {
			sync_config_event.Invoke();
		}
		output_result(false, true, preset_name);
	}
	public void load_preset() {
		load_preset(preset_name_to_load.val);
	}

	// Returns the full list of all presets saved in the gamemode_presets folder.
	public static List<string> get_available_presets() {
		return save_util.get_files_in_dir(presets_subpath);
	}

	// Helper function for degug statements.
	private void output_result(bool saving, bool succeeded, string preset_name) {
		if (!debug) {
			return;
		}

		string saved_or_loaded = saving ? "Saved" : "Loaded";
		string saving_or_loading = saving ? "saving" : "loading";
		if (succeeded) {
			Debug.Log("Success! " + saved_or_loaded + " preset: " + preset_name);
		} else {
			Debug.LogError("Failed! Error occured while " + saving_or_loading + " preset: " + preset_name);
		}
	}
}


