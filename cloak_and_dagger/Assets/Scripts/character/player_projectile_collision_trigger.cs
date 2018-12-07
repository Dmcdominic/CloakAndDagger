using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public enum death_type { dagger, fireball };

[Serializable]
public struct death_event_data
{
    public death_event_data(byte playerID, death_type death_Type, byte killerID, float dagger_z_rotation)
    {
        this.playerID = playerID;
        this.death_Type = death_Type;
        this.killerID = killerID;
		this.dagger_z_rotation = dagger_z_rotation;
    }

    public byte playerID;
    public death_type death_Type;
    public byte killerID;
	public float dagger_z_rotation;
}

[RequireComponent(typeof(Collider2D))]
public class player_projectile_collision_trigger : sync_behaviour<death_event_data> {

	[SerializeField]
	int_float_event kill_out;

	[SerializeField]
	int_event_object pre_local_death;

    [SerializeField]
    gameplay_config gameplay_Config;
	[SerializeField]
	win_con_config win_Con_Config;
	[SerializeField]
	readonly_camera_config camera_Config;

	[SerializeField]
	float_event_object camera_shake_event;

	[SerializeField]
	player_int teams;

	[SerializeField]
    int_event_object destroy_dagger;

	[SerializeField]
	int_event_object destroy_fireball;

	[SerializeField]
	GameObject dead_body_prefab;

	[SerializeField]
	GameObject blood_spray_prefab;

	[SerializeField]
	GameObject incineration_prefab;

	[SerializeField]
	player_bool reflecting;

	[SerializeField]
	int_vec2_int_event local_reflect_proc;

	[SerializeField]
	int_event_object end_reflect;

	[SerializeField]
	anim_parent anim_Parent;

    [SerializeField]
    Sound_manager Sfx;

    [SerializeField]
    death_event_object trigger;

	public override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
		string collider_tag = collider.gameObject.tag;
		if (!is_local) {
			return;
		}
		
		if (collider_tag.Equals("Dagger")) {
			dagger_data dagger_Data = collider.gameObject.GetComponent<dagger_data_carrier>().dagger_Data;
			network_id daggerID = collider.gameObject.GetComponent<network_id>();

			if (!can_be_killed_by(dagger_Data.thrower)) {
				return;
			}

			if (reflecting[gameObject_id.val]) {
				int palette = collider.gameObject.GetComponent<anim_piece>().palette_index;
				destroy_dagger.Invoke(daggerID.val);

				// For natural bounce away from player:
				//Vector2 dest = collider.transform.position;

				// For dagger to reflect back in the direction it came:
				Vector2 dest = transform.position - collider.transform.right.normalized;

				local_reflect_proc.e.Invoke(gameObject_id.val, dest, palette);
				if (gameplay_Config.bool_options[gameplay_bool_option.fragile_reflect]) {
					end_reflect.Invoke(gameObject_id.val);
				}
			} else {
				if (!dagger_Data.collaterals) {
					destroy_dagger.Invoke(daggerID.val);
				}

				float dagger_z_rotation = collider.transform.rotation.eulerAngles.z;
				rectify(Time.time, new death_event_data((byte)gameObject_id.val, death_type.dagger, dagger_Data.thrower, dagger_z_rotation));
				send_state(new death_event_data((byte)gameObject_id.val, death_type.dagger, dagger_Data.thrower, dagger_z_rotation));
			}
		} else if (collider_tag.Equals("Fireball")) {
			fireball_data fireball_Data = collider.gameObject.GetComponent<fireball_data_carrier>().fireball_Data;
			network_id fireballID = collider.gameObject.GetComponent<network_id>();

			if (!can_be_killed_by(fireball_Data.thrower)) {
				return;
			}

			if (!fireball_Data.collaterals) {
				destroy_fireball.Invoke(fireballID.val);
			}

			rectify(Time.time, new death_event_data((byte)gameObject_id.val, death_type.fireball, fireball_Data.thrower, 0));
			send_state(new death_event_data((byte)gameObject_id.val, death_type.fireball, fireball_Data.thrower, 0));
		}
	}


	public override void rectify(float f, death_event_data DD) {
		if (DD.death_Type == death_type.dagger) {
			spawn_dead_body(DD);
			Sfx.sfx_trigger.Invoke("Dagger_hit_player");

			GameObject spray = Instantiate(blood_spray_prefab);
			spray.transform.position = transform.position;
			print("Dagger rotation:" + DD.dagger_z_rotation);
			ParticleSystem.ShapeModule shape = spray.GetComponent<ParticleSystem>().shape;
			shape.rotation = new Vector3(0, 90f + -DD.dagger_z_rotation, 0);
			print("Shape rotation:" + shape.rotation);

			// Camera shake
			if (is_local) {
				camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.dagger_you_die_shake]);
			} else if (DD.killerID == gameObject_id.val) {
				camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.dagger_you_kill_shake]);
			} else {
				camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.dagger_other_die_shake]);
			}
		} else if (DD.death_Type == death_type.fireball) {
			spawn_incineration(DD);
			Sfx.sfx_trigger.Invoke("Fireball_hit_player");

			// Camera shake
			if (is_local) {
				camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.fireball_you_die_shake]);
			} else if (DD.killerID == gameObject_id.val) {
				camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.fireball_you_kill_shake]);
			} else {
				camera_shake_event.Invoke(camera_Config.float_options[readonly_camera_float_option.fireball_other_die_shake]);
			}
		} else { // Default
			spawn_dead_body(DD);
			Sfx.sfx_trigger.Invoke("Dagger_hit_player");
		}

		pre_local_death.Invoke(gameObject_id.val);
		kill_out.Invoke(gameObject_id.val, gameplay_Config.float_options[gameplay_float_option.respawn_delay]);
        trigger.Invoke(DD);
    }

	private void spawn_dead_body(death_event_data DD) {
		GameObject dead_body = Instantiate(dead_body_prefab, transform.position, transform.rotation);

		anim_parent body_anim_parent = dead_body.GetComponentInChildren<anim_parent>();
		body_anim_parent.set_all_palette(anim_Parent.palette_index);
		char_anim_helper body_char_anim_helper = dead_body.GetComponentInChildren<char_anim_helper>();
		body_char_anim_helper.play_death_anim();
	}

	private void spawn_incineration(death_event_data DD) {
		GameObject incineration = Instantiate(incineration_prefab, transform.position, transform.rotation);
	}

	private bool can_be_killed_by(int id) {
		if (gameObject_id.val == id) {
			return (win_Con_Config.bool_options[winCon_bool_option.suicide]);
		}

		if (teams[gameObject_id.val] == teams[id]) {
			return (win_Con_Config.bool_options[winCon_bool_option.friendly_fire]);
		}

		return true;
	}

}