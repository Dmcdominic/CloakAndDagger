using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool connected = false;



	// Use this for initialization
	public void Start () {
        client.Connect(0, OnConnect, () => connect_error.SetActive(true));
        invite_player.e.AddListener(str => client.Invite_Player(str));
        join_player.e.AddListener(str => client.Join_Party(str));
	}

    void OnConnect()
    {
        connected = true;
        client.Register_Message_Receive(to_splitter.Invoke);
        client.Register_Party_List(pn => my_party.val = pn);
        client.Register_friends(fl => my_friends.val = fl);
        
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
