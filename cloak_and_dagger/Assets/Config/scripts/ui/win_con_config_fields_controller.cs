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
	private Dropdown dropdown;

	public event_object win_con_changed;
	public new win_con_config config;
	public map_config map_Config;
	public all_maps_list all_Maps_List;

	public config_option_event_object update_one_config_value;
	public config_option_event_object update_one_config_field;


	// Initialization
	protected new void Awake() {
		base.config = config;
		if (win_con_changed) {
			win_con_changed.e.AddListener(refresh_all_fields_if_currently_open);
			win_con_changed.e.AddListener(check_map_compatibility);
		}
		if (update_one_config_field) {
			update_one_config_field.e.AddListener(on_update_one_config_field);
		}

		// ========== Populate option UI parameters here ==========
		// NOTE - The order of options here will be reflected in-game in the config menu,
		// except that depedendencies will be placed directly beneath their parent toggle.
		// NOTE - The generic enum type argument in each new ui_(type)_info MUST be the (category)_bool_option.
		// See gameplay_config_fields_controller as an example of this.
		ui_parameters_ordered.Add(winCon_bool_option.free_for_all, new ui_bool_info<winCon_bool_option>("Each player battles for individual victory - No teams allowed."));
		ui_parameters_ordered.Add(winCon_float_option.time_limit, new ui_float_info<winCon_bool_option>(0, 300, 0, 3600, "Time in seconds before the game ends. Set to \"0\" for no limit."));
		ui_parameters_ordered.Add(winCon_bool_option.teammates_revealed, new ui_bool_info<winCon_bool_option>("Your teammates, if any, are always visible to you."));

		ui_parameters_ordered.Add(winCon_bool_option.friendly_fire, new ui_bool_info<winCon_bool_option>("Teammates can be killed by your daggers and fireballs."));
		ui_parameters_ordered.Add(winCon_bool_option.suicide, new ui_bool_info<winCon_bool_option>("You can be killed by your own daggers and fireballs."));

		// Kill Count
		ui_parameters_ordered.Add(winCon_int_option.kill_limit, new ui_int_info<winCon_bool_option>(1, 100, 1, 1000, "Number of kills required to win."));

		// Last Survivor
		ui_parameters_ordered.Add(winCon_int_option.lives, new ui_int_info<winCon_bool_option>(1, 100, 0, 1000, "Number of lives that each player has. Set to \"0\" for no limit."));

		// Regicide/King of the Hill
		ui_parameters_ordered.Add(winCon_float_option.time_to_win, new ui_float_info<winCon_bool_option>(1, 300, 0, 3600, "How much time in seconds you need to spend holding the objective to win"));
		ui_parameters_ordered.Add(winCon_float_option.hill_duration, new ui_float_info<winCon_bool_option>(1, 30, 1, 600, "How many seconds each hill stays active for."));

		// Assault
		ui_parameters_ordered.Add(winCon_int_option.payload_delivery_limit, new ui_int_info<winCon_bool_option>(1, 10, 1, 100, "Number of payload deliveries required to win."));
		ui_parameters_ordered.Add(winCon_float_option.payload_respawn_delay, new ui_float_info<winCon_bool_option>(0, 20, 0, 120, "Delay in seconds before the payload respawns after being delivered."));
		ui_parameters_ordered.Add(winCon_float_option.payload_light_range, new ui_float_info<winCon_bool_option>(8f, 15f, 1f, 50f, "The range of the light emitted by the payload."));
		ui_parameters_ordered.Add(winCon_bool_option.payload_carrier_revealed, new ui_bool_info<winCon_bool_option>("The payload light stays on while being carried."));

		ui_parameters_ordered.Add(winCon_bool_option.carrier_dagger_disabled, new ui_bool_info<winCon_bool_option>("The payload carrier can not use their dagger ability."));
		ui_parameters_ordered.Add(winCon_bool_option.carrier_fireball_disabled, new ui_bool_info<winCon_bool_option>("The payload carrier can not use their fireball ability."));
		ui_parameters_ordered.Add(winCon_bool_option.carrier_blink_disabled, new ui_bool_info<winCon_bool_option>("The payload carrier can not use their blink ability."));
		ui_parameters_ordered.Add(winCon_bool_option.carrier_reflect_disabled, new ui_bool_info<winCon_bool_option>("The payload carrier can not use their reflect ability."));
		ui_parameters_ordered.Add(winCon_bool_option.carrier_torch_disabled, new ui_bool_info<winCon_bool_option>("The payload carrier can not use their torch ability."));
		ui_parameters_ordered.Add(winCon_bool_option.carrier_trap_disabled, new ui_bool_info<winCon_bool_option>("The payload carrier can not use their trap ability."));

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
		current_fields.Add(dropdown.transform);
		dropdown.transform.SetParent(this.transform);
		dropdown.transform.localScale = Vector3.one;

		List<Dropdown.OptionData> dropdown_options = new List<Dropdown.OptionData>();
		foreach (win_condition win_con in Enum.GetValues(typeof(win_condition))) {
			Dropdown.OptionData next_option = new Dropdown.OptionData(option_title(win_con));
			dropdown_options.Add(next_option);
		}
		dropdown.AddOptions(dropdown_options);

		dropdown.interactable = host.val;
		dropdown.value = (int)config.win_Condition;
		dropdown.RefreshShownValue();
		dropdown.onValueChanged.AddListener(switch_win_con_by_index);
	}

	private void switch_win_con_by_index(int index) {
		config.switch_win_con(index);
		update_one_config_value.Invoke(-2, index, (int)config_Category);
		validate_free_for_fall();
	}

	private void validate_free_for_fall() {
		if (!all_Win_Cons_List.win_con_infos[config.win_Condition].compatible_bool_options.Contains(winCon_bool_option.free_for_all)) {
			config.bool_options[winCon_bool_option.free_for_all] = false;
		}
	}

	private void check_map_compatibility() {
		bool map_found = all_Maps_List.map_infos.ContainsKey(map_Config.map);
		if (!map_found || !all_Maps_List.map_infos[map_Config.map].compatible_win_cons.Contains(config.win_Condition)) {
			map_Config.map = all_Win_Cons_List.win_con_infos[config.win_Condition].default_map;
		}
	}

	private void on_update_one_config_field(int inc_encoded_enum, object inc_value, int inc_config_cat) {
		if (inc_encoded_enum == -2 && inc_config_cat == (int)config_Category && dropdown) {
			dropdown.value = (int)inc_value;
			dropdown.RefreshShownValue();
		}
	}

	public override int get_encoded_enum_bool_opt(winCon_bool_option option) {
		return 0 + 3 * ((int)option);
	}
	public override int get_encoded_enum_float_opt(winCon_float_option option) {
		return 1 + 3 * ((int)option);
	}
	public override int get_encoded_enum_int_opt(winCon_int_option option) {
		return 2 + 3 * ((int)option);
	}

}
