using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class team_setter : MonoBehaviour {

    enum color : int {blue, red }

    [SerializeField]
    Button B;

    [SerializeField]
    party_var party;

    [SerializeField]
    player_int team;


    [SerializeField]
    int_var local_id;

    public int id;

    [SerializeField]
    sync_event team_swap_in;

    [SerializeField]
    sync_event team_swap_out;

    color my_team = color.blue;

    [SerializeField]
    Image blue_image;

    [SerializeField]
    Image red_image;

    
    [SerializeField]
    win_con_config wcc;

	// Use this for initialization
	void Start () {
        B.onClick.AddListener(() =>
        {
            team_swap_out.Invoke(0, null, local_id);
            toggle();
        });
        team_swap_in.e.AddListener((f, o, i) => { if (i == id) toggle(); });
        team[id] = (int)color.blue;
        StartCoroutine(hide());

    }

    void toggle()
    {
        if (my_team == color.blue)
        {
            my_team = color.red;
            B.image = red_image;
            red_image.gameObject.SetActive(true);
            blue_image.gameObject.SetActive(false);
        }
        else
        {
            my_team = color.blue;
            B.image = blue_image;
            red_image.gameObject.SetActive(false);
            blue_image.gameObject.SetActive(true);
        }
        team[id] = (int)(my_team == color.blue ? color.blue : color.red);
    }
	
	// Update is called once per frame
	void Update () {
        B.interactable = id == local_id;

	}

    IEnumerator hide()
    {
        while (true)
        {
            
            yield return new WaitUntil(() => wcc.bool_options[winCon_bool_option.free_for_all]);
            
            foreach(Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }
            yield return new WaitUntil(() => !wcc.bool_options[winCon_bool_option.free_for_all]);
            foreach(Transform t in transform)
            {
                t.gameObject.SetActive(true);
            }
            if (my_team == color.blue)
                red_image.gameObject.SetActive(false);
            else
                blue_image.gameObject.SetActive(false);
            
            yield return null;
        }

    }

}

