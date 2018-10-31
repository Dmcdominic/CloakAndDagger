using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(network_id))]
public class possum : MonoBehaviour { //this guy just passes the gameobject along to the respawner

    [SerializeField]
    int_float_event in_kill;

    [SerializeField]
    player_event out_kill;


    private void Start()
    {
        in_kill.e.AddListener((id, _) => play_dead(id));
    }

    void play_dead(int id)
    {
        if(id == GetComponent<network_id>().val)
            out_kill.Invoke(id, gameObject);
    }

}
