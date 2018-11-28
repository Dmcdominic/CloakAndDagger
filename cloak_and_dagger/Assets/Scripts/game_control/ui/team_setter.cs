using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class team_setter : MonoBehaviour {

    [SerializeField]
    Dropdown dd;

    [SerializeField]
    party_var party;

    [SerializeField]
    player_int team;

    [SerializeField]
    player_int also_team;

    [SerializeField]
    int_var local_id;

    public int id;

    [SerializeField]
    sync_event team_swap_in;

    [SerializeField]
    sync_event team_swap_out;

	// Use this for initialization
	void Start () {
        dd.onValueChanged.AddListener(i => 
        {
            team_swap_out.Invoke(0, i, local_id);
            team[id] = i;
            also_team[id] = i;
        });
        team_swap_out.e.AddListener((f, o, i) => { if (i == id) dd.value = (int)o; });
    }
	
	// Update is called once per frame
	void Update () {
        int i = dd.options.Count;
		for(; i < party.val.members.Count + 1; i++)
        {
            dd.options.Add(new Dropdown.OptionData($"Team {i}"));
        }
        while(dd.options.Count > party.val.members.Count + 1)
        {
            dd.options.Remove(dd.options[party.val.members.Count + 1]);
        }

        dd.interactable = id == local_id;

       

	}
}
