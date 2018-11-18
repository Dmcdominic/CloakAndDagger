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
	

    // initialize
    void Start() {
        timerMask.fillAmount = 0;
        timeRemaining.text = "";
    }

    // run the cooldown
    void Update () {
		updateUI();
		completeAction();
	}

    void updateUI() {
        timerMask.fillAmount = current_cooldown.val / gameplay_Config.float_options[cooldown_type];
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
}
