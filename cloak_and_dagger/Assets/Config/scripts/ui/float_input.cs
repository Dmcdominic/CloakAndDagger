using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class float_input : config_input_field {

	[HideInInspector]
	private float _value;
	public float value {
		get { return _value; }
		set { _value = value; on_value_changed.Invoke(_value); after_val_changed(_value); }
	}
	
	public Slider slider;
	public InputField input_field;

	[HideInInspector]
	public float min, max, slider_min, slider_max;

	public class float_event : UnityEvent<float> { };
	public float_event on_value_changed = new float_event();


	public override void set_up_listeners() {
		// UI input listeners
		input_field.onEndEdit.AddListener(on_end_edit_action());
		slider.onValueChanged.AddListener(on_slider_value_change());

		// on_value_change listeners
		on_value_changed.AddListener(update_text_field());

		// Enable values_prepopulated so that a config sync event will now get sent after edits
		values_prepopulated = true;
	}

	public override void update_this_field_to(object new_val_obj) {
		float new_val = (float)new_val_obj;
		if (new_val != value) {
			value = new_val;
		}
	}

	private float parse_and_clamp_string(string val) {
		float new_val = min;
		float.TryParse(val, out new_val);
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
			input_field.text = ((float)input).ToString();
			input_field.onEndEdit.Invoke(input_field.text);
		};
	}

	private UnityAction<float> update_text_field() {
		return input => {
			if (input_field.text != input.ToString()) {
				input_field.text = input.ToString();
			}
		};
	}

}
