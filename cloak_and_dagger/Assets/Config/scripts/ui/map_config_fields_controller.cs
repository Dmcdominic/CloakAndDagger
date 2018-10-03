using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class map_config_fields_controller : config_fields_controller<map_bool_option, map_float_option, map_int_option> {
	public override config_category config_Category {
		get { return config_category.map; }
	}

	public List<map_info> all_maps;
	//public Dropdown map_dropdown_prefab;

	public event_object map_changed;
	public new map_config config;

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
		//create_map_dropdown();
		base.create_all_fields();
	}

	//private void create_map_dropdown() {
	//	Dropdown dropdown = Instantiate(win_con_dropdown_prefab.gameObject).GetComponent<Dropdown>();
	//	dropdown.transform.SetParent(this.transform);

	//	List<Dropdown.OptionData> dropdown_options = new List<Dropdown.OptionData>();
	//	foreach (win_condition win_con in Enum.GetValues(typeof(win_condition))) {
	//		Dropdown.OptionData next_option = new Dropdown.OptionData(option_title(win_con));
	//		dropdown_options.Add(next_option);
	//	}
	//	dropdown.AddOptions(dropdown_options);

	//	dropdown.interactable = interactable;
	//	dropdown.value = (int)config.win_Condition;
	//	dropdown.onValueChanged.AddListener(config.switch_win_con);
	//}

}
