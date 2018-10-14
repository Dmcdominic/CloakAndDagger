using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Events;

public class player_manager : MonoBehaviour {

    [SerializeField]
    GameObject party_display;
    [SerializeField]
    GameObject leader_slot;
    [SerializeField]
    GameObject member_slot;
    [SerializeField]
    obj_event message_to_splitter;

    [SerializeField]
    party_var out_party_info;

    [SerializeField]
    string_var out_name;

    

    [SerializeField]
    client_var client;

	// Use this for initialization
	void Start () {
	}



    UnityAction player_join(string arg)
    {
        return () => client.val.Join_Party(arg);
    }

    UnityAction player_invite(string arg)
    {
        return () => client.val.Invite_Player(arg);
    }

    void joinned()
    {

    }

    void invited()
    {

    }

    void populate_list(List<Party_Names> pns)
    {
        out_party_info.val = 
            pns.Where(pn => pn.members.Contains(out_name.val) || pn.leader == out_name.val).FirstOrDefault();
        foreach(Transform child in party_display.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(Party_Names pn in pns)
        {
            GameObject leader = Instantiate(leader_slot,party_display.transform);
            leader.transform.GetChild(0).GetComponent<Text>().text = pn.leader;
            if(!pn.members.Contains(out_name.val))
            {
                leader.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(player_join(pn.leader));
                leader.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(player_invite(pn.leader));
            } //else leave button
           foreach(string member_name in pn.members)
            {
                GameObject member = Instantiate(member_slot, leader.transform);
                member.transform.GetChild(0).GetComponent<Text>().text = member_name;
                if(!pn.members.Contains(out_name.val)) //you could do this faster
                {
                    member.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(player_join(member_name));
                    member.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(player_invite(member_name));
                }

            }
        }
    }

}
