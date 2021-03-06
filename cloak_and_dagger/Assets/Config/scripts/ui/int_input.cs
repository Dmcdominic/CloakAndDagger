﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class int_input : config_input_field {

	[HideInInspector]
	private int _value;
	public int value {
		get { return _value; }
		set { _value = value; on_value_changed.Invoke(_value); after_val_changed(_value); }
	}
	
	public Slider slider;
	public InputField input_field;

	[HideInInspector]
	public int min, max, slider_min, slider_max;

	public class int_event : UnityEvent<int> { };
	public int_event on_value_changed = new int_event();


	public override void set_up_listeners() {
		// UI input listeners
		input_field.onEndEdit.AddListener(on_end_edit_action());
		slider.onValueChanged.AddListener(on_slider_value_change());

		// on_value_change listeners
		on_value_changed.AddListener(update_text_field());
		on_value_changed.AddListener(update_slider());

		// Start the coroutine which will listen for value changes to send
		StartCoroutine(waiting_to_send_val());
	}

	public override void update_this_field_to(object new_val_obj) {
		int new_val = (int)new_val_obj;
		if (new_val != value) {
			value = new_val;
			slider.value = new_val;
		}
	}

	private int parse_and_clamp_string(string val) {
		int new_val = min;
		int.TryParse(val, out new_val);
		new_val = Mathf.Clamp(new_val, min, max);
		return new_val;
	}

	private UnityAction<string> on_end_edit_action() {
		return input => {
			value = parse_and_clamp_string(input);
		};
	}
	private UnityAction<float> on_slider_value_change() {
		return input => {
			input_field.text = ((int)input).ToString();
			input_field.onEndEdit.Invoke(input_field.text);
		};
	}

	private UnityAction<int> update_text_field() {
		return input => {
			if (input_field.text != input.ToString()) {
				input_field.text = input.ToString();
			}
		};
	}

	private UnityAction<int> update_slider() {
		return input => {
			if (slider.value != input && slider.minValue <= input && input <= slider.maxValue) {
				slider.value = input;
			}
		};
	}

}
