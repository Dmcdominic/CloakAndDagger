using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct vec2xvec2
{
    public Vector2 pi_l;
    public Vector2 pi_r;

    public vec2xvec2(Vector2 pi_l, Vector2 pi_r)
    {
        this.pi_l = pi_l;
        this.pi_r = pi_r;
    }
}
[CreateAssetMenu(menuName = "variables/player_state")]
public class player_state_obj : ScriptableObject, IValue<vec2xvec2>
{

    [SerializeField]
    bool use_constant = false;

    [SerializeField]
    vec2xvec2 constant = new vec2xvec2();

    [SerializeField]
    private vec2xvec2 value;

    public vec2xvec2 val
    {
        get { return use_constant ? constant : value; }
        set { this.value = value; }
    }
}
