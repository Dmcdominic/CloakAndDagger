﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class hill_script : MonoBehaviour {

    [SerializeField]
    event_object activate;

    [SerializeField]
    int_float_event score;

    [SerializeField]
    int_var local_id;

    [SerializeField]
    sync_event score_out;

    [SerializeField]
    sync_event score_in;

    [SerializeField]
    sync_event move_out;

    [SerializeField]
    sync_event move_in;


    [SerializeField]
    win_condition_assets_packet WCAP;

    [SerializeField]
    GameObject effects;

    [SerializeField]
    float radius;

    bool active = false;

    List<GameObject> players;

    [SerializeField]
    player_bool dead;



    int id;



    // Use this for initialization
    void Start () {
        id = transform.GetSiblingIndex();
        activate.e.AddListener(() => { if (id == 0 && local_id == 0)
            {
                run();
                activate.e.RemoveAllListeners();
                move_out.Invoke(0, 0, 0);
            } });
        
        score_in.e.AddListener((f, o, i) => { if(active) score.Invoke(i, (float)o); });
        move_in.e.AddListener((f, o, i) => 
        {
            if (i == transform.GetSiblingIndex())
            {
                effects.SetActive(true);
                active = true;
            }
            else
            {
                effects.SetActive(false);
                active = false;
            }

        });
	}
	
    public void run()
    {
        StartCoroutine(go());
    }

    IEnumerator go()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        float wait_time = 
                UnityEngine.Random.value * 5 - 2.5f + WCAP.win_Con_Config.float_options[winCon_float_option.hill_duration];

        move_in.Invoke(0, 0, 0);
        yield return new WaitForSeconds(Mathf.Abs(wait_time));
        System.Random rnd = new System.Random();
        int i = rnd.Next(0, transform.parent.childCount);
        while(i == transform.GetSiblingIndex())
        {
            i = rnd.Next(0, transform.parent.childCount);
        }
        transform.parent.GetChild(i).GetComponent<hill_script>().run();
        move_out.Invoke(0,0,i);
        move_in.Invoke(0,0,i);


    }

	// Update is called once per frame
	void Update () {
        if(players == null || players.Count == 0) players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        if (active)
        {
            print("active");
            foreach(GameObject player in players)
            {
                if(Vector3.Distance(transform.position,player.transform.position) < radius)
                {
                    int player_id = player.GetComponent<network_id>().val;
                    if (player_id == local_id && !dead[player_id])
                    {
                        score_out.Invoke(Time.time,Time.deltaTime,local_id);
                        score.Invoke(player_id, Time.deltaTime);
                    }
                }
            }
        }
	}
}
