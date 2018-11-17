using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class char_anim_helper : MonoBehaviour {

	public player_event dagger_thrown;
	public player_event fireball_thrown;

	public float running_velo_threshhold;

	private Animator animator;
	private Rigidbody2D rb;


	private void Awake() {
		animator = GetComponent<Animator>();
		rb = GetComponentInParent<Rigidbody2D>();
		if (dagger_thrown) {
			dagger_thrown.e.AddListener(on_dagger_thrown);
		}
		if (fireball_thrown) {
			fireball_thrown.e.AddListener(on_fireball_thrown);
		}
	}

	private void Update() {
		if (rb) {
			bool running = rb.velocity.magnitude > running_velo_threshhold;
			animator.SetBool("running", running);
		}
	}

	public void on_dagger_thrown(int placeholder, GameObject obj) {
		if (obj == transform.parent.gameObject) {
			animator.SetTrigger("throw");
		}
	}
	public void on_fireball_thrown(int placeholder, GameObject obj) {
		if (obj == transform.parent.gameObject) {
			animator.SetTrigger("throw");
		}
	}

	public void play_death_anim() {
		animator.SetTrigger("die");
	}

	public void destroy_this() {
		Destroy(transform.root.gameObject);
	}
}
