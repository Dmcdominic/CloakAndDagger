using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.Linq;

public struct dash_state
{ //use this if you want to put the network players on cooldown on their machine
    public serializable_vec2 pos;
    public float cooldown;
}

[RequireComponent(typeof(network_id))]
public class dash : sync_behaviour<serializable_vec2> {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _target_dest;

	public gameplay_config gameplay_Config;
	private float max_distance;

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	light_spawn_event_object light_spawn_trigger;

	[SerializeField]
	GameObject particles_prefab;

    [SerializeField]
    int_float_event stun_out;

    [SerializeField]
    int_float_event cooldown_out;

    [SerializeField]
    GameObject blink_trail_prefab;

    [SerializeField]
    float mini_stun;

    [SerializeField]
    Sound_manager Sfx;

    Rigidbody2D rb;

	// Use this for initialization
	public override void Start () {
        base.Start();
		if (is_local) {
			trigger.e.AddListener(dash_func);
		}
        rb = GetComponent<Rigidbody2D>();
		max_distance = gameplay_Config.float_options[gameplay_float_option.blink_range];
	}

    public void dash_func(int id, float cooldown)
    {
        if (id != GetComponent<network_id>().val) return;


        // Blink to the target destination
        Vector3 displacement = _target_dest.val - _origin.val;
        if (displacement.magnitude > max_distance)
        {
            displacement = displacement.normalized * max_distance;
        }

        blink(_origin,displacement);
        
        cooldown_out.Invoke(gameObject_id.val, cooldown);
        send_state((Vector2)transform.position);
		GameObject trail = Instantiate(blink_trail_prefab,transform.position,Quaternion.identity);
        trail.GetComponent<move_between>().run(_origin.val, transform.position, 5, Time.deltaTime);
        

    }

    public override void rectify(float f, serializable_vec2 v2)
    {
		// Spawn light at origin
		light_spawn_data light_data = new light_spawn_data(_origin.val, 2f);
		light_spawn_trigger.Invoke(light_data);

		// Spawn particle effects
		GameObject particles = Instantiate(particles_prefab);
		particles.transform.position = _origin.val;

		// Trigger sound effect
		Sfx.sfx_trigger.Invoke("Dash");
		
        transform.position = v2;
        stun_out.Invoke(gameObject_id.val,mini_stun);
    }

    bool blink(Vector2 origin,Vector2 delta)
    {
        // Spawn light at origin
        light_spawn_data light_data = new light_spawn_data(_origin.val, 2f);
        light_spawn_trigger.Invoke(light_data);

		// Spawn particle effects
		GameObject particles = Instantiate(particles_prefab);
		particles.transform.position = _origin.val;

		// Trigger sound effect
		Sfx.sfx_trigger.Invoke("Dash");

		RaycastHit2D[] hits = new RaycastHit2D[1];

        transform.position = origin + delta;
        while (rb.Cast(Vector3.zero, hits, 0) > 0)
        {
            transform.position -= (Vector3)delta.normalized * .1f;
        }
        
        stun_out.Invoke(gameObject_id.val, mini_stun);
		return true;
    }

}