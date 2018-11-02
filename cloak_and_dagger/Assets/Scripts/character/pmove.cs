﻿using System.Collections;
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
    public float theta;

    public player_state(Vector2 pos, Vector2 vel, float theta)
    {
        this.pos = pos;
        this.vel = vel;
        this.theta = theta;
    }
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IValue<int>))]
public class pmove : sync_behaviour<player_state>
{

    [SerializeField]
    Vec2Var input_vec;


    [SerializeField]
    float move_speed = 100;

    [SerializeField]
    bool_var is_stun;

    [SerializeField]
    int_float_event dagger_in;

    Rigidbody2D rb;

    Vector2 target_pos;

    float target_theta;

    float smooth_rot_vel = 0;

    bool ignore_input = false;

    Vector3 smooth_vel = Vector3.zero;




    // Update is called once per frame
    void Update()
    {

        if (!is_local) //you are not the local go
        {
            transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref smooth_vel, .005f);
            return;
        }
        if (is_stun.val)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (rb.velocity.x * input_vec.val.x < 0) rb.velocity = Vector2.up * rb.velocity;
        if (rb.velocity.y * input_vec.val.y < 0) rb.velocity = Vector2.right * rb.velocity;
        if(rb.velocity.sqrMagnitude < .25f)
            rb.AddForce(input_vec.val * move_speed * .1f, ForceMode2D.Impulse);
        rb.AddForce(input_vec.val * move_speed, ForceMode2D.Force);

        if(((Vector2)input_vec).sqrMagnitude > .1f && !ignore_input)
            target_theta = Mathf.Rad2Deg * Mathf.Atan2(input_vec.val.y, input_vec.val.x);

        transform.eulerAngles = Vector3.forward * Mathf.SmoothDampAngle(
                                transform.eulerAngles.z, target_theta, 
                                ref smooth_rot_vel, .025f);

        state = new player_state(transform.position, rb.velocity,transform.eulerAngles.z);

    }



    public override void rectify(float t, player_state ps)
    {

        target_pos = ps.pos;
        rb.velocity = ps.vel;
        transform.eulerAngles = Vector3.forward * ps.theta;
        
    }
    // Use this for initialization
    public override void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        base.Start();
        state = new player_state(transform.position, rb.velocity,transform.eulerAngles.z);

        dagger_in.e.AddListener(throw_dagger);
    }

    void throw_dagger(int id,float rot)
    {
        if(id == gameObject_id.val)
        {
            transform.eulerAngles = Vector3.forward * rot;
            smooth_rot_vel = 0;
            target_theta = transform.eulerAngles.z;
            ignore_input = true;
            Invoke("reset_input", .15f);
        }
    }

    void reset_input() { ignore_input = false; }

    private void OnEnable()
    {
        sync_continously();
    }
}
