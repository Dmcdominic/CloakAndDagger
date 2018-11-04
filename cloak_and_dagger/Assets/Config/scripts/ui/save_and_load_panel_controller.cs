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

	public Text result_text;
	public int_event_object completed_save_event;
	public int_event_object completed_load_event;

	public bool_var host;

	//protected List<Transform> current_fields;


	// Initialization
	private void Awake() {
		if (update_fields_trigger) {
			update_fields_trigger.e.AddListener(update_fields);
		}
		if (completed_save_event) {
			completed_save_event.e.AddListener(update_result_text_save);
		}
		if (completed_load_event) {
			completed_load_event.e.AddListener(update_result_text_load);
		}

		loadable_presets_dropdown.onValueChanged.AddListener(on_dropdown_value_changed);
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
		result_text.text = "";
		repopulate_loadable_dropdown_options();
		if (!host.val) {
			load_button.interactable = false;
			loadable_presets_dropdown.ClearOptions();
			loadable_presets_dropdown.interactable = false;
		} else {
			load_button.interactable = true;
			loadable_presets_dropdown.interactable = true;
		}
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
	}

	private void repopulate_loadable_dropdown_options() {
		loadable_presets_dropdown.ClearOptions();
		current_loadable_options = save_and_load_presets.get_available_presets();
		loadable_presets_dropdown.AddOptions(current_loadable_options);
		if (current_loadable_options.Count > 0) {
			loadable_presets_dropdown.value = 0;
			on_dropdown_value_changed(0);
		} else {
			preset_name_to_load.val = "";
		}
	}

	private void on_dropdown_value_changed(int option_index) {
		preset_name_to_load.val = current_loadable_options[option_index];
	}

	private void update_result_text_save(int success) {
		if (success > 0) {
			repopulate_loadable_dropdown_options();
			result_text.text = "Save success!";
			result_text.color = Color.green;
		} else {
			result_text.text = "Save failed";
			result_text.color = Color.red;
		}
	}
	private void update_result_text_load(int success) {
		if (success > 0) {
			result_text.text = "Load success!";
			result_text.color = Color.green;
		} else {
			result_text.text = "Load failed";
			result_text.color = Color.red;
		}
	}

}
