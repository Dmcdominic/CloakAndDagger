using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class kill_feed_line : MonoBehaviour {

	public float fully_visible_time;
	public float fade_time;

	private Text text;

	private float visible_timer;
	private float fading_timer;
	private bool fading = false;


	private void Awake() {
		text = GetComponent<Text>();
		visible_timer = fully_visible_time;
	}

	// Keep the text visible until fully_visible_time is up,
	// and then fade it out according to fade_time.
	// Once it has faded to complete transparency, destroy the gameObject.
	void Update () {
		if (visible_timer > 0) {
			visible_timer -= Time.deltaTime;
		} else if (!fading) {
			text.CrossFadeAlpha(0, fade_time, false);
			fading_timer = fade_time + 0.5f; // Include some buffer time
			fading = true;
		} else if (fading_timer > 0) {
			fading_timer -= Time.deltaTime;
		} else {
			Destroy(gameObject);
		}
	}
}
