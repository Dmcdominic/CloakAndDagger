using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class serializable_vec2
{
    public float x;
    public float y;

    public serializable_vec2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(serializable_vec2 sv2)
    {
        return new Vector2(sv2.x, sv2.y);
    }

    public static implicit operator serializable_vec2(Vector2 v2)
    {
        return new serializable_vec2(v2.x, v2.y);
    }

    public static implicit operator Vector3(serializable_vec2 sv2)
    {
        return new Vector3(sv2.x, sv2.y);
    }


}

[System.Serializable]
public struct player_state
{
    public serializable_vec2 pos;
    public serializable_vec2 vel;

    public player_state(Vector2 pos, Vector2 vel)
    {
        this.pos = pos;
        this.vel = vel;
    }
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IValue<int>))]
public class pmove : sync_behaviour<player_state> {



    [SerializeField]
	Vec2Var input_vec;


	[SerializeField]
	float move_speed = 100;

	[SerializeField]
	bool_var is_stun;


	Rigidbody2D rb;




    // Update is called once per frame
    void Update () {
		if(is_stun.val)
		{
			return;
		}
        if (gameObject_id.val != local_id.val) //you are not the local go
        {
            return;
        }
		rb.AddForce(input_vec.val * move_speed,ForceMode2D.Force);
        send_state(new player_state(transform.position,rb.velocity));
        

	}

    public override void rectify(float t, player_state ps)
    {

        transform.position = ps.pos;
        rb.velocity = ps.vel;
        rb.MovePosition(ps.pos + rb.velocity * (Time.time - t));
        print($"move {gameObject_id}'s butt over to {(Vector2)ps.pos}" +
            $" and push them at vel {(Vector2)ps.vel}" +
            $" and this happenned {Time.time - t} seconds ago.");

    }

	// Use this for initialization
	public override void Start () { 
		rb = transform.GetComponent<Rigidbody2D>();
        //gameObject_id = (IValue<int>)GetComponent(typeof(IValue<int>));
        //in_player_state.e.AddListener(on_player_update);
        base.Start();

    }


}
