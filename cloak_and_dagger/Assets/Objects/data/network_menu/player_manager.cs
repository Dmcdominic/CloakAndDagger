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
    string_var password;


    [SerializeField]
    GameObject req_join_button_prefab;


    [SerializeField]
    GameObject invite_button_prefab;

    [SerializeField]
    GameObject client_go;
    IProtagoras_Client<object> client;


	// Use this for initialization
	void Start () {
        client = client_go.GetComponent<IProtagoras_Client<object>>();
	}

    public void refresh()
    {
        if(client != null) populate_list(client.Get_Party_list());
    }


    IEnumerator refresher()
    {
        while(true)
        {
            refresh();
            yield return new WaitForSeconds(.5f);
            
        }
    }

    public void setup()
    {
        client.Setup_for_player(out_name.val, password.val, invited, joinned, message_to_splitter.Invoke, 0);
        StartCoroutine(refresher());
    }

    UnityAction player_join(string arg)
    {
        return () => { print($"you tried to join {arg}'s party"); client.Join_Party(arg); };
    }

    UnityAction player_invite(string arg)
    {
        return () => client.Invite_Player(arg);
    }

    UnityAction identity(Action act) { return () => act (); }

    void joinned(string joinner,Action accept)
    {
        GameObject j_button = Instantiate(req_join_button_prefab,transform);
        j_button.transform.GetChild(0).GetComponentInChildren<Text>().text = $"{joinner} wants to join your party!";
        j_button.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(identity (accept));
    }

    void invited(string inviter,Action accept)
    {
        GameObject j_button = Instantiate(req_join_button_prefab, transform);
        j_button.transform.GetChild(0).GetComponentInChildren<Text>().text = $"{inviter} invited you to their party!";
        j_button.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(identity(accept));
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
            leader.transform.GetChild(0).GetComponentInChildren<Text>().text = pn.leader;
            if(pn.leader != out_name.val)
            {
                leader.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(player_join(pn.leader));
                leader.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(player_invite(pn.leader));
            } //else leave button
           foreach(string member_name in pn.members)
            {
                GameObject member = Instantiate(member_slot, leader.transform);
                member.transform.GetChild(0).GetComponentInChildren<Text>().text = member_name;
                if(!pn.members.Contains(out_name.val)) //you could do this faster
                {
                    member.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(player_join(member_name));
                    member.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(player_invite(member_name));
                }

            }
        }
    }

}
