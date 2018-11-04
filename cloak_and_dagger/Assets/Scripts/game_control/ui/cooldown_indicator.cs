using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown_indicator : MonoBehaviour {

    public Image timerMask;

    public bool active;
    public float cooldown;
    public float timeLeft;

    // initialize
    void Start() {
        timerMask.fillAmount = 0;
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
    }

    // stop the cooldown indicator
    public void completeAction() {
        if (timeLeft > 0)
            return;
        active = false;
        timeLeft = cooldown;
    }
}
