﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn_manager : MonoBehaviour {

    [SerializeField]
    player_event death_trigger;

    [SerializeField]
    player_bool dead;

    [SerializeField]
    map_config map;

	// Use this for initialization
	void Start () {
        death_trigger.e.AddListener(kill);
	}
	
    void kill(int id,GameObject go)
    {
        go.SetActive(false);
        StartCoroutine(revive(id,go));
    }

	IEnumerator revive(int id,GameObject go)
    {
        yield return new WaitUntil(() => !dead[id]);
        Vector2 target = map.current_map_info._spawn_points.next;
        while(!good_to_move(target))
        {
            target = map.current_map_info._spawn_points.next;
            yield return null;
        }
        go.transform.position = target;
        go.SetActive(true);
    }

    bool good_to_move(Vector2 pos)
    {
        return true;
    }
}
