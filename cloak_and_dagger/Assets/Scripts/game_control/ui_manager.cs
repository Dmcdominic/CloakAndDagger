using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ui_manager : MonoBehaviour {

    [SerializeField]
    thirds_client client;

    [SerializeField]
    string_var my_name;

    [SerializeField]
    string_var pass;

    [SerializeField]
    GameObject login_table;

    [SerializeField]
    GameObject sing_up_table;

    [SerializeField]
    GameObject loading;

    [SerializeField]
    GameObject start_menu;


    [SerializeField]
    obj_event to_splitter;

    [SerializeField]
    party_var my_party;

    [SerializeField]
    friends_list my_friends;

    [SerializeField]
    string_list my_friend_requests;

    [SerializeField]
    GameObject title;

    [SerializeField]
    GameObject sign_up_error;

    [SerializeField]
    GameObject connect_error;

    [SerializeField]
    GameObject login_error;

    [SerializeField]
    string_event_object invite_player;

    [SerializeField]
    string_event_object join_player;

    [SerializeField]
    event_object add_friend;

    [SerializeField]
    string_var friend_to_add;

    [SerializeField]
    event_object failed_to_find_friend;

    [SerializeField]
    event_object found_friend;

    [SerializeField]
    string_event_object add_this_friend;

    [SerializeField]
    int_var local_id;

    [SerializeField]
    GameObject Invited_button;

    [SerializeField]
    GameObject party_menu;

    [SerializeField]
    bool_var host_var;

    bool connected = false;



	// Use this for initialization
	public void Start () {
        client.Connect(0, OnConnect, () => connect_error.SetActive(true));
        invite_player.e.AddListener(str => client.Invite_Player(str));
        join_player.e.AddListener(str => client.Join_Party(str));
        add_friend.e.AddListener(() => client.add_friend(friend_to_add));
        add_this_friend.e.AddListener(str => { client.add_friend(str); my_friend_requests.val.Remove(str); });
        my_friend_requests.val = new List<string>();
        client.Register_friend_requests(str => my_friend_requests.val.Add(str));
        client.Register_Receive_Invite(Invited);
        client.Register_Receive_Request(Requested);
	}


    void Invited(string name, Action accept)
    {
        GameObject but = Instantiate(Invited_button,transform);
        but.GetComponent<invite_setter>().invite(name, () => { party_menu.SetActive(true); start_menu.SetActive(false);  accept(); });
    }

    void Requested(string name, Action accept)
    {
        GameObject but = Instantiate(Invited_button, transform);
        but.GetComponent<invite_setter>().request(name, () => { party_menu.SetActive(true); start_menu.SetActive(false); accept(); });
    }

    void OnConnect()
    {
        connected = true;
        client.Register_Message_Receive(to_splitter.Invoke);
        client.Register_Party_List(pn => 
        {
            my_party.val = pn;
            if (pn.members.Contains(my_name))
            {
                local_id.val = pn.members.IndexOf(my_name) + 1;
                host_var.val = false;
            }
            else
            {
                local_id.val = 0;
                host_var.val = true;
            }

            if (pn.members.Any(_ => true)) { start_menu.SetActive(false); party_menu.SetActive(true); print("yooo"); } 

        });
        client.Register_friends(fl => my_friends.val = fl);
        client.Register_friend_callbacks(found_friend.Invoke, failed_to_find_friend.Invoke);
        
        
    }

    public void sign_up()
    {
        StartCoroutine(delay_sign_up());
    }

    IEnumerator delay_sign_up()
    {
        yield return new WaitUntil(() => connected);
        client.Create_Player(my_name, pass, () => StartCoroutine(delay_sign_in()), 
            () => { sign_up_error.SetActive(true);  loading.SetActive(false); });
    }
    
    public void login_in()
    {
        StartCoroutine(delay_sign_in());
    }

    IEnumerator delay_sign_in()
    {
        yield return new WaitUntil(() => connected);
        client.Login(my_name, pass, () => { loading.SetActive(false); title.SetActive(false); start_menu.SetActive(true); }
            ,() => { login_error.SetActive(true); loading.SetActive(false); });
    }

}
