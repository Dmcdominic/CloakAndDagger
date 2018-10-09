using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class trigger_death : NetworkBehaviour {

	[SerializeField]
	dagger_collision_event_object trigger;
    [SerializeField]
    death_event_object trigger_on_death;

	[SerializeField]
	bool_var spectator_reveal;
	[SerializeField]
	int_var lives;
	[SerializeField]
	event_object respawn_event;
    [SerializeField]
    kill_feed_display kill_Feed;
	
	NetworkIdentity net_id;


	// Use this for initialization
	void Start () {
		net_id = GetComponent<NetworkIdentity>();
		if (isLocalPlayer) {
			lives.val = 10;
			trigger.e.AddListener(on_dagger_collision);
		}

		// THIS vvv IS TEMPORARY. We will need a way to set spectator_reveal to false
		// at the start of every match, so long as the player is alive,
		// but set it/leave it as true if someone loaded in specifically as a spectator.
		spectator_reveal.val = false;
	}
	
	private void on_dagger_collision(GameObject dagger, dagger_data dagger_Data, GameObject player, string tag) {
		if (player.Equals(this.gameObject)) {
			die(dagger_Data.thrower);
		}
	}

	private void die(sbyte killerID) {
		if (isLocalPlayer) {
			lives.val--;
			ClientScene.RemovePlayer(net_id.playerControllerId);

            sbyte playerID = this.gameObject.GetComponent<Player_data_carrier>().player_Data.tempID;
            death_event_data death_Event_Data = new death_event_data(playerID, death_type.dagger, killerID);
            trigger_on_death.e.Invoke(death_Event_Data);
            if (lives.val > 0) {
                respawn_event.Invoke();
                kill_Feed.display_slain(death_Event_Data);
                //TODO: broadcast sfx "You have been slain"
            } else {
                spectator_reveal.val = true;
                kill_Feed.display_terminated(death_Event_Data);
                //TODO: broadcast sfx "You have been terminated"
            }
		}
	}
}
