using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kill_feed_display : MonoBehaviour {

	void Start () {
        this.GetComponent<Text>().text = "";
	}
	
	public void display_slain(death_event_data DED) {
        if (DED.death_Type == death_type.dagger) {
            string tmp = "Player " + DED.killerID + " has slain Player " + DED.playerID + "!";
            this.GetComponent<Text>().text = tmp;
        } else if (DED.death_Type == death_type.suicide) {
            string tmp = "Player " + DED.playerID + " has commited suicide!";
            this.GetComponent<Text>().text = tmp;
        }
        /** The above strings for display are ONLY TEMPORARY and may subject to future
         *  changes. Also, more death types (see death_event_object.cs) can be added
         *  in the future. */
    }

    public void display_terminated(death_event_data DED) {
        if (DED.death_Type == death_type.dagger) {
            string tmp = "Player " + DED.killerID + " has terminated Player " + DED.playerID + "!";
            this.GetComponent<Text>().text = tmp;
        }
        else if (DED.death_Type == death_type.suicide) {
            string tmp = "Player " + DED.playerID + " has self_terminated!";
            this.GetComponent<Text>().text = tmp;
        }
        /** The above strings for display are ONLY TEMPORARY and may subject to future
         *  changes. Also, more death types (see death_event_object.cs) can be added
         *  in the future. */
    }
}