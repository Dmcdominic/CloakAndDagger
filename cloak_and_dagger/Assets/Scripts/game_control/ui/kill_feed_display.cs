using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kill_feed_display : MonoBehaviour {

	[SerializeField]
	death_event_object trigger;

	[SerializeField]
	Text feed_line_prefab;

	[SerializeField]
	game_stats game_Stats;

	[SerializeField]
	win_con_config win_Con_Config;

	[SerializeField]
	party_var party;


	// Initialize by setting the on_death_event listener
	private void Awake() {
		if (trigger) {
			trigger.e.AddListener(on_death_event);
		}
	}

	// Whenever this is disabled, e.g. the game ends and we return to the lobby,
	// all text lines should be destroyed
	private void OnDisable() {
		foreach (Transform child in transform) {
			Destroy(child);
		}
	}

	// Display the kill whenever a death event is received
	private void on_death_event(death_event_data DED) {
		display_kill(DED);
	}

	// Display a kill feed line according to a death_event_data object
	private void display_kill(death_event_data DED) {
		bool terminated = (win_Con_Config.win_Condition == win_condition.last_survivor) && (game_Stats.player_Stats[DED.playerID].lives_remaining <= 0);
		bool suicide = (DED.playerID == DED.killerID);
		bool fireball = (DED.death_Type == death_type.fireball);

		string text = "";
		if (suicide) {
			text = party.get_name(DED.playerID);
			if (terminated) {
				text += " has self-terminated";
			} else {
				text += " has committed suicide";
			}
		} else {
			text = party.get_name(DED.killerID);
			if (terminated) {
				text += " has terminated ";
			} else if (fireball) {
				text += " has incinerated ";
			} else {
				text += " has slain ";
			}
			text += party.get_name(DED.playerID);
		}

		create_feed_line(text);
	}

	// Instantiate a kill feed line with given text
	private void create_feed_line(string text) {
		Text new_feed_line = Instantiate(feed_line_prefab.gameObject).GetComponent<Text>();
		new_feed_line.text = text;
		new_feed_line.transform.SetParent(gameObject.transform);
		new_feed_line.transform.SetAsFirstSibling();
	}
}