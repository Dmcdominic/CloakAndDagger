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

	public throw_dagger_data(Vector2 _thrower_pos, Vector2 _target_dest) {
		this.thrower_pos = _thrower_pos;
		this.target_dest = _target_dest;
		System.Random rand = new System.Random();
		this.network_id = rand.Next(int.MinValue, int.MaxValue);
	}
}


public class throw_dagger : sync_behaviour<throw_dagger_data> {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _dest;

	[SerializeField]
	float speed = 100;

	[SerializeField]
	float cast_buffer = 1;

	[SerializeField]
	GameObject dagger_prefab;

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	gameplay_config gameplay_Config;

	private Player_data_carrier player_data_Carrier;
	private uint thrown_index_counter = 0;


	private void Awake() {
		player_data_Carrier = GetComponent<Player_data_carrier>();
	}

    public override void Start()
    {
        base.Start();
        if (is_local)
        {
            trigger.e.AddListener(local_throw);
        }
    }

    // This is the local player, and they pressed the throw dagger button, and it was off cooldown
    private void local_throw(int id, float cooldown) {
        if (id != gameObject_id.val) return; 

		throw_dagger_data throw_data = new throw_dagger_data(_origin.val, _dest.val);
		send_state(throw_data);
		throw_func(throw_data);
	}

	// Received a throw_dagger event
	public override void rectify(float t, throw_dagger_data state) {
		// todo - account for lag with t?
		throw_func(state);
	}
	
	// Actually spawn a dagger
	public void throw_func(throw_dagger_data throw_data) { //too many times have I tried to name a func throw.
		Vector3 position = throw_data.thrower_pos;
		Vector3 dir = (Vector3)throw_data.target_dest - position;

		Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
		GameObject my_dagger = Instantiate(dagger_prefab, position, rotation);
		my_dagger.transform.position += my_dagger.transform.right * cast_buffer;
		my_dagger.GetComponent<dagger_data_carrier>().dagger_Data = create_dagger_data();
		my_dagger.GetComponent<network_id>().val = throw_data.network_id;

		Rigidbody2D rb = my_dagger.GetComponent<Rigidbody2D>();
		if (rb) {
			rb.velocity = my_dagger.transform.right * speed;
		}
	}

	// Edit the properties of the dagger here before throwing it
	private dagger_data create_dagger_data() {
		bool collaterals = gameplay_Config.bool_options[gameplay_bool_option.dagger_collaterals];
		byte thrower_ID = player_data_Carrier.player_Data.playerID;
        return new dagger_data(collaterals, thrower_ID, thrown_index_counter++);
	}
	
}
