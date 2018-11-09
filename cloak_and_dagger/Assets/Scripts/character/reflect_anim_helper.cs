using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class reflect_anim_helper : MonoBehaviour {

	[SerializeField]
	int_float_event reflect_time;

	[SerializeField]
	int_event_object end_reflect;

	private SpriteRenderer sr;
	private network_id network_Id;


	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		network_Id = GetComponentInParent<network_id>();
		if (reflect_time) {
			reflect_time.e.AddListener(on_reflect_time);
		}
		if (end_reflect) {
			end_reflect.e.AddListener(on_end_reflect);
		}
	}
	
	private void on_reflect_time(int id, float time) {
		if (network_Id.val != id) return;

		StartCoroutine(reflect_anim_for_time(time));
	}

	private void on_end_reflect(int id) {
		if (network_Id.val != id) return;

		StopAllCoroutines();
		sr.enabled = false;
	}

	IEnumerator reflect_anim_for_time(float time) {
		sr.enabled = true;
		yield return new WaitForSeconds(time);
		sr.enabled = false;
	}

}
