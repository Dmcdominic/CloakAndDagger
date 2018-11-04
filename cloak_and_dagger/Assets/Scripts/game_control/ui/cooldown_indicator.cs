using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown_indicator : MonoBehaviour {

    public Text timeRemaining;
    public Image timerMask;

    public bool active;
    public float cooldown;
    float timeLeft;

    // initialize
    void Start() {
        timerMask.fillAmount = 0;
        timeRemaining.text = "";
    }

    // run the cooldown
    void Update () {
        if (active) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
                timeLeft = 0;
            updateUI();
            completeAction();
        }
	}

    // activate the cooldown
    public void activateCooldown() {
        active = true;
    }

    void updateUI() {
        timerMask.fillAmount = timeLeft / cooldown;
        if (timeLeft >= 1)
            timeRemaining.text = "" + (int)(timeLeft + 1);
        else
            timeRemaining.text = "" + (int)(timeLeft * 10 + 1) / 10.0;
    }

    // stop the cooldown indicator
    public void completeAction() {
        if (timeLeft > 0)
            return;
        active = false;
        timeLeft = cooldown;
        timeRemaining.text = "";
    }
}
