using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


[System.Serializable]
public struct throw_dagger_data {
	public serializable_vec2 thrower_pos;
	public serializable_vec2 target_dest;
	public int network_id;
	public int palette;
	public bool reflected;

	public throw_dagger_data(Vector2 _thrower_pos, Vector2 _target_dest, int _palette, bool _reflected = false) {
		this.thrower_pos = _thrower_pos;
		this.target_dest = _target_dest;
		System.Random rand = new System.Random();
		this.network_id = rand.Next(int.MinValue, int.MaxValue);
		this.palette = _palette;
		this.reflected = _reflected;
	}
}


[RequireComponent(typeof(network_id))]
public class throw_dagger : sync_behaviour<throw_dagger_data> {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _dest;

	[SerializeField]
	GameObject dagger_prefab;

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	int_vec2_int_event reflect_proc;

	[SerializeField]
	player_event dagger_thrown;

	[SerializeField]
	gameplay_config gameplay_Config;
	[SerializeField]
	readonly_gameplay_config readonly_Gameplay_Config;

    [SerializeField]
    int_float_event inform_pmove;

	[SerializeField]
	anim_parent anim_Parent;

	private network_id networkID;


	private void Awake() {
		networkID = GetComponent<network_id>();
	}

    public override void Start()
    {
        base.Start();
        if (is_local)
        {
            trigger.e.AddListener(local_throw);
			reflect_proc.e.AddListener(local_reflect_proc);
        }
    }

    // This is the local player, and they pressed the throw dagger button, and it was off cooldown
    private void local_throw(int id, float cooldown) {
        if (id != gameObject_id.val) return;

		//int palette = anim_Parent.palette_index;
		int palette = GetComponentInChildren<anim_piece>().palette_index;
        Vector2 dir = autoaim(_origin.val,_dest.val - _origin.val);
		throw_dagger_data throw_data = new throw_dagger_data(_origin.val, _origin.val + dir, palette);
		send_state(throw_data);
		throw_func(throw_data);
	}

	private void local_reflect_proc(int id, Vector2 dest, int palette) {
		if (id != gameObject_id.val) return;

		throw_dagger_data throw_data = new throw_dagger_data(_origin.val, dest, palette, true);
		send_state(throw_data);
		throw_func(throw_data);
	}

	// Received a throw_dagger event
	public override void rectify(float t, throw_dagger_data state) {
		// todo - account for lag with t?
		throw_func(state);
	}

    Vector2 autoaim(Vector2 origin,Vector2 dir)
    {
        float theta = gameplay_Config.float_options[gameplay_float_option.autoaim_theta] * Mathf.Deg2Rad;
        float i = 0;
        RaycastHit2D rh = new RaycastHit2D();
        float autoaim_dist = 25;
        float std = Mathf.Atan2(dir.y, dir.x);
        for (; i < theta / 2 ; i += .01f)
        {
            Vector2 dest = new Vector2(Mathf.Cos(std + i),Mathf.Sin(std + i));
            rh = Physics2D.Raycast(origin + dest, dest,autoaim_dist);
            if(rh && rh.transform.tag == "Player")
            {
                print($"clockwise");
                return dest;
            }
            dest = new Vector2(Mathf.Cos(std - i),Mathf.Sin(std - i));
            rh = Physics2D.Raycast(origin + dest, dest, autoaim_dist);
            if(rh && rh.transform.tag == "Player")
            {
                print("counterclockwise");
                return dest;
            }
        }
        return dir;
    }
	
	// Actually spawn a dagger
	public void throw_func(throw_dagger_data throw_data) { //too many times have I tried to name a func throw.
		Vector3 position = throw_data.thrower_pos;
		Vector3 dir = (Vector3)throw_data.target_dest - position;

		Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
		GameObject my_dagger = Instantiate(dagger_prefab, position, rotation);
		my_dagger.transform.position += my_dagger.transform.right * readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.dagger_buffer];
		my_dagger.GetComponent<dagger_data_carrier>().dagger_Data = create_dagger_data();
		my_dagger.GetComponent<network_id>().val = throw_data.network_id;

		my_dagger.GetComponentInChildren<anim_piece>().palette_index = throw_data.palette;

		Rigidbody2D rb = my_dagger.GetComponent<Rigidbody2D>();
		if (rb) {
			rb.velocity = my_dagger.transform.right * gameplay_Config.float_options[gameplay_float_option.dagger_speed];
		}
		if (!throw_data.reflected) {
			inform_pmove.Invoke(gameObject_id.val, rotation.eulerAngles.z);
			if (dagger_thrown) {
				dagger_thrown.Invoke(gameObject_id.val, gameObject);
			}
		}
	}

	// Edit the properties of the dagger here before throwing it
	private dagger_data create_dagger_data() {
		bool collaterals = gameplay_Config.bool_options[gameplay_bool_option.dagger_collaterals];
		byte thrower_ID = (byte)networkID.val;
        return new dagger_data(collaterals, thrower_ID);
	}
	
}
