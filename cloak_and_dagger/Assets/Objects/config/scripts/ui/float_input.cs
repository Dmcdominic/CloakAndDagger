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
		set { _value = value; on_value_changed.Invoke(_value); }
	}

	public Text title;
	public Slider slider;
	public InputField input_field;

	[HideInInspector]
	public float min, max, slider_min, slider_max;

	public class float_event : UnityEvent<float> { };
	public float_event on_value_changed = new float_event();


	public void set_up_listeners() {
		// UI input listeners
		input_field.onEndEdit.AddListener(on_end_edit_action());
		slider.onValueChanged.AddListener(on_slider_value_change());

		// on_value_change listeners
		on_value_changed.AddListener(update_text_field());
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
			print("new value: " + value);
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
