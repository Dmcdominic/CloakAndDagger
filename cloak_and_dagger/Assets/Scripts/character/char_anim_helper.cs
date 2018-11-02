using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class char_anim_helper : MonoBehaviour {

	public player_event dagger_thrown;

	private Animator animator;


	private void Awake() {
		animator = GetComponent<Animator>();
		if (dagger_thrown) {
			dagger_thrown.e.AddListener(on_dagger_thrown);
		}
	}

	public void on_dagger_thrown(int placeholder, GameObject obj) {
		if (obj == transform.parent.gameObject) {
			animator.SetTrigger("throw");
		}
	}

	public void on_die() {
		animator.SetTrigger("die");
	}
}
