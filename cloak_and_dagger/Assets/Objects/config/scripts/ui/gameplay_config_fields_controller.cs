using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay_config_fields_controller : config_fields_controller<gameplay_bool_option, gameplay_float_option, gameplay_int_option> {
	public new gameplay_config config;

	private new void Start() {
		base.config = config;
		base.Start();
	}

};