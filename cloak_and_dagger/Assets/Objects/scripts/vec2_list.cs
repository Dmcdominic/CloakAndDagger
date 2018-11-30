using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "variables/vec2_list")]
public class vec2_list : ScriptableObject
{


    [SerializeField]
    private List<Vector2> value;

    private int pos = -1;

    public List<Vector2> val
    {
        get { return value; }
        set { this.value = value; }
    }

    public void set_counter(int i)
    {
        pos = i;
    }
    public Vector2 next
    {
        get { pos++; pos %= value.Count; return value[pos]; }
    }
}