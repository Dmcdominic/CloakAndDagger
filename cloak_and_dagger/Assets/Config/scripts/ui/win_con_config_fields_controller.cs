using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class win_con_config_fields_controller : config_fields_controller<winCon_bool_option, winCon_float_option, winCon_int_option> {
	public override config_category config_Category {
		get { return config_category.win_con;  }
	}

	public all_win_cons_list all_Win_Cons_List;
	public Dropdown win_con_dropdown_prefab;

	public event_object win_con_changed;
	public new win_con_config config;
	public map_config map_Config;
	public all_maps_list all_Maps_List;


	// Initialization
	protected new void Awake() {
		base.config = config;
		if (win_con_changed) {
			win_con_changed.e.AddListener(refresh_all_fields_if_currently_open);
			win_con_changed.e.AddListener(check_map_compatibility);
		}

		// ========== Populate option UI parameters here ==========
		// NOTE - The order of options here will be reflected in-game in the config menu,
		// except that depedendencies will be placed directly beneath their parent toggle.
		// NOTE - The generic enum type argument in each new ui_(type)_info MUST be the (category)_bool_option.
		// See gameplay_config_fields_controller as an example of this.
		ui_parameters_ordered.Add(winCon_float_option.time_limit, new ui_float_info<winCon_bool_option>(0, 300, 0, 3600, "Time in seconds before the game ends. Set to \"0\" for no limit."));
		ui_parameters_ordered.Add(winCon_int_option.kill_limit, new ui_int_info<winCon_bool_option>(1, 100, 1, 1000, "Number of kills required to win."));
		ui_parameters_ordered.Add(winCon_int_option.lives, new ui_int_info<winCon_bool_option>(1, 100, 1, 1000, "Number of lives that each player has."));

		base.populate_ui_dependents();
		base.Awake();
	}

	public override void create_all_fields() {
		win_con_info current_win_con_info = all_Win_Cons_List.win_con_infos[config.win_Condition];
		limited_options_only = true;
		limited_bool_options = current_win_con_info.compatible_bool_options;
		limited_float_options = current_win_con_info.compatible_float_options;
		limited_int_options = current_win_con_info.compatible_int_options;

		create_win_con_dropdown();
		base.create_all_fields();
	}

	private void create_win_con_dropdown() {
		Dropdown dropdown = Instantiate(win_con_dropdown_prefab.gameObject).GetComponent<Dropdown>();
		dropdown.transform.SetParent(this.transform);

		List<Dropdown.OptionData> dropdown_options = new List<Dropdown.OptionData>();
		foreach (win_condition win_con in Enum.GetValues(typeof(win_condition))) {
			Dropdown.OptionData next_option = new Dropdown.OptionData(option_title(win_con));
			dropdown_options.Add(next_option);
		}
		dropdown.AddOptions(dropdown_options);

		dropdown.interactable = interactable;
		dropdown.value = (int)config.win_Condition;
		dropdown.onValueChanged.AddListener(config.switch_win_con);
	}

	private void check_map_compatibility() {
		bool map_found = all_Maps_List.map_infos.ContainsKey(map_Config.map);
		if (!map_found || !all_Maps_List.map_infos[map_Config.map].compatible_win_cons.Contains(config.win_Condition)) {
			map_Config.map = all_Win_Cons_List.win_con_infos[config.win_Condition].default_map;
		}
	}

}
