using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "events/config_option_event")]
public class config_option_event_object : ScriptableObject {

	public class config_option_event : UnityEvent<int, object, int> { }

	public UnityEvent<int, object, int> e = new config_option_event();

	public void Invoke(int encoded_enum, object val, int config_cat) { e.Invoke(encoded_enum, val, config_cat); }

}
