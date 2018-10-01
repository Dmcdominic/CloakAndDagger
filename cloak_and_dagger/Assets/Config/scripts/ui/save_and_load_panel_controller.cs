using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class save_and_load_panel_controller : MonoBehaviour {

	public int_event_object update_fields_trigger;

	public Button load_button;
	public Dropdown loadable_presets_dropdown;
	public string_var preset_name_to_load;
	private List<string> current_loadable_options;


	// Initialization
	private void Awake() {
		if (update_fields_trigger) {
			update_fields_trigger.e.AddListener(update_fields);
		}

		loadable_presets_dropdown.onValueChanged.AddListener(on_dropdown_value_changed);

		bool interactable = true; // TODO - check here whether or not you are the host
		if (!interactable) {
			load_button.interactable = false;
			loadable_presets_dropdown.ClearOptions();
			loadable_presets_dropdown.interactable = false;
		}

		clear_fields();
	}

	private void update_fields(int category_index) {
		clear_fields();
		if (category_index == -1) {
			create_all_fields();
		}
	}

	// To be called when the save/load panel is supposed to close
	private void clear_fields() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
	}

	// To be called when the save/load panel is supposed to open
	private void create_all_fields() {
		repopulate_loadable_dropdown_options();
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
	}

	private void repopulate_loadable_dropdown_options() {
		loadable_presets_dropdown.ClearOptions();
		current_loadable_options = save_and_load_presets.get_available_presets();
		loadable_presets_dropdown.AddOptions(current_loadable_options);
		loadable_presets_dropdown.value = 0;
	}

	private void on_dropdown_value_changed(int option_index) {
		preset_name_to_load.val = current_loadable_options[option_index];
	}

}
