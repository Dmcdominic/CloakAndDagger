using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown_indicator : MonoBehaviour {

    public Text timeRemaining;
    public Image timerMask;

	public gameplay_config gameplay_Config;
	public gameplay_float_option cooldown_type;
	
	public float_var current_cooldown;

	public event_object pulse_event;

	private Animator animator;


    // initialize
    void Start() {
		animator = GetComponent<Animator>();
		pulse_event.e.AddListener(pulse);
        timerMask.fillAmount = 0;
        timeRemaining.text = "";
    }

    // run the cooldown
    void Update () {
		updateUI();
		completeAction();
	}

	void updateUI() {
		float total_cooldown = gameplay_Config.float_options[cooldown_type];
		if (total_cooldown == 0) {
			timerMask.fillAmount = 0;
		} else {
			timerMask.fillAmount = current_cooldown.val / gameplay_Config.float_options[cooldown_type];
		}
        if (current_cooldown.val >= 1)
            timeRemaining.text = "" + Mathf.FloorToInt(current_cooldown.val);
        else
            timeRemaining.text = "" + (int)(current_cooldown.val * 10 + 1) / 10.0;
    }

    // stop the cooldown indicator
    public void completeAction() {
        if (current_cooldown.val > 0)
            return;
        timeRemaining.text = "";
    }

	// trigger the indicator pulse
	public void pulse() {
		animator.SetTrigger("pulse");
	}
}
