using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_blood_splatter_overlay : MonoBehaviour {

	public int_event_object pre_local_death;
	public int_var local_id;

	private Animator animator;


	private void Awake() {
		if (pre_local_death) {
			pre_local_death.e.AddListener(on_local_death);
		}
		animator = GetComponent<Animator>();
	}

	private void on_local_death(int placeholder) {
		animator.SetTrigger("splatter");
	}

}
