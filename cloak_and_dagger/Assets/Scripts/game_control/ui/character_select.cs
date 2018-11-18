using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_select : MonoBehaviour {

    [SerializeField]
    sync_event out_select;

    [SerializeField]
    int_var local_player;

    public void select(int i)
    {
        out_select.Invoke(0,i,local_player,reliable: true);
    }
}
