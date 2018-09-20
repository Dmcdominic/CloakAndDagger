using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay_config_fields_controller : config_fields_controller<gameplay_bool_option, gameplay_float_option, gameplay_int_option> {
	public override config_category config_Category {
		get { return config_category.gameplay; }
	}

	public new gameplay_config config;

	// Initialization
	protected new void Awake() {
		base.config = config;
		base.Awake();
	}

};