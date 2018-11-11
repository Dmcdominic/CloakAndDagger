using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class range_cap : MonoBehaviour {
	
	[SerializeField]
	readonly_gameplay_config readonly_Gameplay_Config;


	private void Update() {
		if (transform.position.magnitude > readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.absolute_dagger_range]) {
			Destroy(gameObject);
		}
	}

}
