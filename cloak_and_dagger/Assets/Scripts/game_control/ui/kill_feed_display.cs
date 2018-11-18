using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kill_feed_display : MonoBehaviour {

    [SerializeField]
    player_var<int> lives;

	public death_event_object trigger;

    string[] kill_feed = new string[5];

	private void Awake() {
		if (trigger) {
			trigger.e.AddListener(on_death_event);
		}
	}

	void Start () {
        this.GetComponent<Text>().text = "";
        for (int i = 0; i < 5; i++)
            kill_feed[i] = "";
	}

	private void on_death_event(death_event_data DED) {
		if (lives[DED.playerID] <= 0) {
			display_terminated(DED);
		} else {
			display_slain(DED);
		}
	}

    void display_helper(string str) {
        int i = 0;
        while (i < 5 && kill_feed[i] != "")
            i++;
        if (i == 5) {
            for (i = 1; i < 5; i++)
                kill_feed[i - 1] = kill_feed[i];
            kill_feed[4] = str;
        } else {
            kill_feed[i] = str;
            i++;
        }
        change_text(i);
		StartCoroutine(wait_and_clear_last(str));
    }

	public void display_slain(death_event_data DED) {
        string tmp = "";
        if (DED.death_Type == death_type.dagger)
            tmp = "Player " + DED.killerID + " has slain Player " + DED.playerID + "!\n";
        else if (DED.death_Type == death_type.suicide)
            tmp = "Player " + DED.playerID + " has commited suicide!\n";
        /** The above strings for display are ONLY TEMPORARY and may subject to future
         *  changes. Also, more death types (see death_event_object.cs) can be added
         *  in the future. */
        display_helper(tmp);
    }

    public void display_terminated(death_event_data DED) {
        string tmp = "";
        if (DED.death_Type == death_type.dagger)
            tmp = "Player " + DED.killerID + " has terminated Player " + DED.playerID + "!\n";
        else if (DED.death_Type == death_type.suicide)
            tmp = "Player " + DED.playerID + " has self_terminated!\n";
        /** The above strings for display are ONLY TEMPORARY and may subject to future
         *  changes. Also, more death types (see death_event_object.cs) can be added
         *  in the future. */
        display_helper(tmp);
    }

	IEnumerator wait_and_clear_last(string current) {
		yield return new WaitForSeconds(1);
		int i = 0;
		while (i < 5 && kill_feed[i] != "")
			i++;
		i--;
		if (kill_feed[i] == current) {
			kill_feed[i] = "";
		}
		change_text(i);
		yield return null;
	}
    
    public void change_text(int i) {
        string for_print = "";
        for (int j = 0; j < i - 1; j++)
            for_print += (kill_feed[j] + "\n");
        for_print += kill_feed[i - 1];
        this.GetComponent<Text>().text = for_print;
    }
}