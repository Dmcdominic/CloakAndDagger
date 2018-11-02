using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public enum death_type { dagger, suicide };

[Serializable]
public struct death_event_data
{
    public death_event_data(byte playerID, death_type death_Type, byte killerID)
    {
        this.playerID = playerID;
        this.death_Type = death_Type;
        this.killerID = killerID;
    }

    public byte playerID;
    public death_type death_Type;
    public byte killerID;
}

public class player_dagger_collision_trigger : sync_behaviour<death_event_data> {

	[SerializeField]
	int_float_event kill_out;

    [SerializeField]
    player_float respawn_times;

    [SerializeField]
    int_event_object destroy_dagger;

    public override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
		string dagger_tag = collider.gameObject.tag;
		if (!dagger_tag.Equals("Dagger") || !is_local) {
			return;
		}

		string tag = gameObject.tag; // Should be "Player"
		dagger_data dagger_Data = collider.gameObject.GetComponent<dagger_data_carrier>().dagger_Data;

        destroy_dagger.Invoke(collider.gameObject.GetInstanceID());

        rectify(Time.time, new death_event_data((byte)gameObject_id.val, death_type.dagger, dagger_Data.thrower));
        send_state(new death_event_data((byte)gameObject_id.val,death_type.dagger,dagger_Data.thrower));
	}



    public override void rectify(float f, death_event_data DD)
    {
        kill_out.Invoke(gameObject_id.val, respawn_times[gameObject_id.val]);
    }

}
