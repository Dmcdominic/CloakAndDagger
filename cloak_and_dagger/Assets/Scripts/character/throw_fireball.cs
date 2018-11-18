using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


[System.Serializable]
public struct throw_fireball_data {
	public serializable_vec2 thrower_pos;
	public serializable_vec2 target_dest;
	public int network_id;
	public int palette;

	public throw_fireball_data(Vector2 _thrower_pos, Vector2 _target_dest, int _palette) {
		this.thrower_pos = _thrower_pos;
		this.target_dest = _target_dest;
		System.Random rand = new System.Random();
		this.network_id = rand.Next(int.MinValue, int.MaxValue);
		this.palette = _palette;
	}
}


[RequireComponent(typeof(network_id))]
public class throw_fireball : sync_behaviour<throw_fireball_data> {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _dest;

	[SerializeField]
	GameObject fireball_prefab;

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	player_event fireball_thrown;

	[SerializeField]
	gameplay_config gameplay_Config;
	[SerializeField]
	readonly_gameplay_config readonly_Gameplay_Config;

    [SerializeField]
    int_float_event inform_pmove;

	[SerializeField]
	anim_parent anim_Parent;

    [SerializeField]
    Sound_manager Sfx;

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
        }
    }

    // This is the local player, and they pressed the throw fireball button, and it was off cooldown
    private void local_throw(int id, float cooldown) {
        if (id != gameObject_id.val) return; 

		throw_fireball_data throw_data = new throw_fireball_data(_origin.val, _dest.val, anim_Parent.palette_index);
		send_state(throw_data);
		throw_func(throw_data);
	}

	// Received a throw_fireball event
	public override void rectify(float t, throw_fireball_data state) {
        // todo - account for lag with t?
        Sfx.sfx_trigger.Invoke("Throw_fireball");
		throw_func(state);
	}
	
	// Actually spawn a fireball
	public void throw_func(throw_fireball_data throw_data) { //too many times have I tried to name a func throw.
		Vector3 position = throw_data.thrower_pos;
		Vector3 dir = (Vector3)throw_data.target_dest - position;

		Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
		GameObject my_fireball = Instantiate(fireball_prefab, position, rotation);
		my_fireball.transform.position += my_fireball.transform.right * readonly_Gameplay_Config.float_options[readonly_gameplay_float_option.fireball_buffer];
		my_fireball.GetComponent<fireball_data_carrier>().fireball_Data = create_fireball_data();
		my_fireball.GetComponent<network_id>().val = throw_data.network_id;

		my_fireball.GetComponentInChildren<anim_piece>().palette_index = throw_data.palette;

		Rigidbody2D rb = my_fireball.GetComponent<Rigidbody2D>();
		if (rb) {
			rb.velocity = my_fireball.transform.right * gameplay_Config.float_options[gameplay_float_option.fireball_speed];
			rb.angularVelocity = 720f * (Random.value > 0.5 ? 1 : -1); // Make it spin
		}
		inform_pmove.Invoke(gameObject_id.val, rotation.eulerAngles.z);
		if (fireball_thrown) {
			fireball_thrown.Invoke(0, gameObject);
		}
	}

	// Edit the properties of the fireball here before throwing it
	private fireball_data create_fireball_data() {
		byte thrower_ID = (byte)networkID.val;
        return new fireball_data(thrower_ID);
	}
	
}
