using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class payload_delivery_zone : MonoBehaviour {

	// The ID of the team that can deliver payloads to this zone
	public int teamID;

	public game_stats game_Stats;


	public bool try_deliver_here(byte playerID) {
		if (teamID == game_Stats.player_Stats[playerID].teamID) {
			// todo - some visual effect here when a payload is delivered
			return true;
		}
		return false;
	}

}
