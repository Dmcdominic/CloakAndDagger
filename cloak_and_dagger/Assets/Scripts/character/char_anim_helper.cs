using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class char_anim_helper : MonoBehaviour {

	public player_event dagger_thrown;
	public player_event fireball_thrown;

	private Animator animator;
	private pmove Pmove;

    [SerializeField]
    player_int character_chosen;

    int net_id;


	private void Awake() {
		animator = GetComponent<Animator>();
		Pmove = GetComponentInParent<pmove>();

		if (dagger_thrown) {
			dagger_thrown.e.AddListener(on_dagger_thrown);
		}
		if (fireball_thrown) {
			fireball_thrown.e.AddListener(on_fireball_thrown);
		}
	}

    private void Start()
    {
        if (GetComponentInParent<network_id>())
        {
            net_id = GetComponentInParent<network_id>().val;
            anim_parent ap = GetComponent<anim_parent>();
            ap.set_all_palette(character_chosen[net_id]);

        }
    }

    private void Update() {
		if (Pmove) {
			animator.SetBool("running", Pmove.running);
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
