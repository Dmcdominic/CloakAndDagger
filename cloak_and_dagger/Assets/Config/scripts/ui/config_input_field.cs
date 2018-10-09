using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class config_input_field : MonoBehaviour {

	[HideInInspector]
	public List<bool_input> toggle_dependencies = new List<bool_input>();

	//private HorizontalLayoutGroup _HLG;
	//private HorizontalLayoutGroup HLG { get {
	//		if (!_HLG) {
	//			_HLG = GetComponent<HorizontalLayoutGroup>();
	//		}
	//		return _HLG;
	//} }

	public Text title;


	private void Start() {
		update_dependency_styling();
		update_visibility();
	}

	// This will check all toggle_depedencies and hide or show if necessary
	public void update_visibility() {
		foreach (bool_input bool_Input in toggle_dependencies) {
			if (bool_Input.collapsed) {
				this.gameObject.SetActive(false);
				return;
			}
		}
		this.gameObject.SetActive(true);
	}
	
	// This is how we can adjust the styling of each dependent child option
	public void update_dependency_styling() {
		//HLG.padding.left += 20 * toggle_dependencies.Count;
	}

	public abstract void set_up_listeners();
	
}
