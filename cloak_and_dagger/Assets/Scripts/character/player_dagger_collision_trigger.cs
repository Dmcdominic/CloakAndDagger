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
    gameplay_config gameplay_Config;

    [SerializeField]
    int_event_object destroy_dagger;

	[SerializeField]
	GameObject dead_body_prefab;

	[SerializeField]
	player_bool reflecting;

	[SerializeField]
	int_vec2_int_event local_reflect_proc;

	[SerializeField]
	int_event_object end_reflect;

	[SerializeField]
	anim_parent anim_Parent;


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

		if (reflecting[gameObject_id.val]) {
			destroy_dagger.Invoke(collider.gameObject.GetInstanceID());
			Vector2 dest = collider.transform.position;
			int palette = anim_Parent.palette_index;
			local_reflect_proc.e.Invoke(gameObject_id.val, dest, palette);
			if (gameplay_Config.bool_options[gameplay_bool_option.fragile_reflect]) {
				end_reflect.Invoke(gameObject_id.val);
			}
		} else {
			if (!dagger_Data.collaterals) {
				destroy_dagger.Invoke(collider.gameObject.GetInstanceID());
			}

			rectify(Time.time, new death_event_data((byte)gameObject_id.val, death_type.dagger, dagger_Data.thrower));
			send_state(new death_event_data((byte)gameObject_id.val, death_type.dagger, dagger_Data.thrower));
		}
	}

	
    public override void rectify(float f, death_event_data DD) {
		spawn_dead_body(DD);
		kill_out.Invoke(gameObject_id.val, gameplay_Config.float_options[gameplay_float_option.respawn_delay]);
    }

	private void spawn_dead_body(death_event_data DD) {
		GameObject dead_body = Instantiate(dead_body_prefab, transform.position, transform.rotation);

		anim_parent body_anim_parent = dead_body.GetComponentInChildren<anim_parent>();
		body_anim_parent.set_all_palette(anim_Parent.palette_index);
		char_anim_helper body_char_anim_helper = dead_body.GetComponentInChildren<char_anim_helper>();
		body_char_anim_helper.play_death_anim();
	}

}
