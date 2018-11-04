using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class map_config_fields_controller : config_fields_controller<map_bool_option, map_float_option, map_int_option> {
	public override config_category config_Category {
		get { return config_category.map; }
	}

	public all_maps_list all_Maps_List;
	public Dropdown map_dropdown_prefab;
	private List<string> current_map_options;

	public event_object map_changed;
	public new map_config config;
	public win_con_config win_Con_Config;

	// Initialization
	protected new void Awake() {
		base.config = config;
		if (map_changed) {
			map_changed.e.AddListener(refresh_all_fields_if_currently_open);
		}

		// ========== Populate option UI parameters here ==========
		// NOTE - The order of options here will be reflected in-game in the config menu,
		// except that depedendencies will be placed directly beneath their parent toggle.
		// NOTE - The generic enum type argument in each new ui_(type)_info MUST be the (category)_bool_option.
		// See gameplay_config_fields_controller as an example of this.
		ui_parameters_ordered.Add(map_bool_option.dynamic_lights, new ui_bool_info<map_bool_option>("Moving lights which roam across the map."));
		ui_parameters_ordered.Add(map_bool_option.hazards, new ui_bool_info<map_bool_option>("Potentially dangerous map-specific hazards."));

		base.populate_ui_dependents();
		base.Awake();
	}

	public override void create_all_fields() {
		map_info current_map_info;
		if (!all_Maps_List.map_infos.TryGetValue(config.map, out current_map_info)) {
			current_map_info = Enumerable.ToList(all_Maps_List.map_infos.Values)[0];
			config.map = current_map_info.map_name;
		}
		
		limited_options_only = true;
		limited_bool_options = current_map_info.compatible_bool_options;
		limited_float_options = current_map_info.compatible_float_options;
		limited_int_options = current_map_info.compatible_int_options;

		create_map_selection_menu(current_map_info);
		base.create_all_fields();
	}

	private void create_map_selection_menu(map_info current_map_info) {
		// TODO - make this a visual selection menu using the thumbnails and a Grid Layout Group

		Dropdown dropdown = Instantiate(map_dropdown_prefab.gameObject).GetComponent<Dropdown>();
		current_fields.Add(dropdown.transform);
		dropdown.transform.SetParent(this.transform);

		List<Dropdown.OptionData> dropdown_options = new List<Dropdown.OptionData>();
		current_map_options = new List<string>();
		foreach (map_info map_Info in all_Maps_List.map_infos.Values) {
			if (map_Info.compatible_win_cons.Contains(win_Con_Config.win_Condition)) {
				current_map_options.Add(map_Info.map_name);
				Dropdown.OptionData next_option = new Dropdown.OptionData(map_Info.map_name);
				dropdown_options.Add(next_option);
			}
		}
		dropdown.AddOptions(dropdown_options);

		dropdown.interactable = host.val;
		dropdown.value = current_map_options.IndexOf(current_map_info.map_name);
		dropdown.onValueChanged.AddListener(switch_map_by_index);
	}

	private void switch_map_by_index(int index) {
		config.switch_map(current_map_options[index]);
	}

}
