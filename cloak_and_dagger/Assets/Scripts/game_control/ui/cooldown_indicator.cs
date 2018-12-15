using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown_indicator : MonoBehaviour {

    public Text timeRemaining;
    public Image timerMask;
	public GameObject disabled_overlay;

	public gameplay_config gameplay_Config;
	public gameplay_float_option cooldown_type;
	
	public float_var current_cooldown;

	public event_object pulse_event;
	public bool_var ability_disabled;

	private Animator animator;


    // Initialize
    void Start() {
		animator = GetComponent<Animator>();
		pulse_event.e.AddListener(pulse);
    }

	private void OnEnable() {
		timerMask.fillAmount = 0;
		timeRemaining.text = "";
		disabled_overlay.SetActive(false);
	}

	// Run the cooldown
	void Update () {
		updateUI();
		completeAction();
	}

	// Update all the elements of this ability indicator
	void updateUI() {
		// Adjust the dark overlay radial fill
		float total_cooldown = gameplay_Config.float_options[cooldown_type];
		if (total_cooldown == 0) {
			timerMask.fillAmount = 0;
		} else {
			timerMask.fillAmount = current_cooldown.val / gameplay_Config.float_options[cooldown_type];
		}

		// Adjust the cooldown text
		if (current_cooldown.val >= 1) {
			timeRemaining.text = "" + Mathf.FloorToInt(current_cooldown.val);
		} else {
			timeRemaining.text = "" + (int)(current_cooldown.val * 10 + 1) / 10.0;
		}

		// Adjust the disabled overlay
		disabled_overlay.SetActive(ability_disabled.val);
	}

    // Stop the cooldown indicator
    public void completeAction() {
		if (current_cooldown.val > 0) {
			return;
		}
        timeRemaining.text = "";
    }

	// Trigger the indicator pulse
	public void pulse() {
		animator.SetTrigger("pulse");
	}
}
