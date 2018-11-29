using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_blood_splatter_overlay : MonoBehaviour {

	public sync_event local_death;
	public int_var local_id;

	private Animator animator;


	private void Awake() {
		if (local_death) {
			local_death.e.AddListener(on_local_death);
		}
		animator = GetComponent<Animator>();
	}

	private void on_local_death(float t, object o, int i) {
		animator.SetTrigger("splatter");
	}

}
