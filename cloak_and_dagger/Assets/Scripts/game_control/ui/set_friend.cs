using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class set_friend : MonoBehaviour {

    [SerializeField]
    Text my_name;

    [SerializeField]
    Text info;

    [SerializeField]
    Button invite;

    [SerializeField]
    Button join;

    [SerializeField]
    string_event_object invite_player;

    [SerializeField]
    string_event_object join_player;

    [SerializeField]
    string_event_object add_Friend;

    [SerializeField]
    GameObject options;

    public void set_me(connection_struct cs)
    {
        
        if(cs.online)
        {
            if (info.text == "Offline") options.SetActive(true);
            if (cs.is_in_game)
            {

                info.text = "In Game ";
            } else if(cs.is_in_party)
            {
                info.text = "In Party ";
            } else
            {
                info.text = "In Menu ";
            }
            switch (cs.current_game)
            {
                case GAME.Cloak_and_Dagger:
                    if(my_name.text != cs.name) options.SetActive(true);
                    info.text += "| Cloak & Dagger";
                    break;
            }
            invite.onClick.RemoveAllListeners();
            join.onClick.RemoveAllListeners();
            invite.onClick.AddListener(() => invite_player.Invoke(cs.name));
            join.onClick.AddListener(() => join_player.Invoke(cs.name));
        }
        else
        {
            info.text = "Offline";
            foreach(Button b in GetComponentsInChildren<Button>()) b.gameObject.SetActive(false);
        }

        my_name.text = cs.name;


    }

    public void set_request(string name)
    {
        
        invite.gameObject.SetActive(true);
        join.gameObject.SetActive(true);
        options.SetActive(false);
        my_name.text = name;
        info.text = "Wants to join your party!";
        invite.GetComponentInChildren<Text>().text = "Accept";
        invite.onClick.RemoveAllListeners();
        invite.onClick.AddListener(() => { add_Friend.Invoke(name); Destroy(gameObject); });
        join.GetComponentInChildren<Text>().text = "Ignore"; //lol this does nothing
        join.onClick.RemoveAllListeners();
        join.onClick.AddListener(() =>
        {
            foreach (Button b in GetComponentsInChildren<Button>()) b.gameObject.SetActive(false);
            my_name.text = "";
            join.GetComponentInChildren<Text>().text = "No.";
            join.onClick.AddListener(() =>
            {
                join.GetComponentInChildren<Text>().text = "Why?";
                join.onClick.AddListener(() =>
                {
                    join.GetComponentInChildren<Text>().text = "Sorry.";
                    join.onClick.AddListener(() => join.gameObject.SetActive(false));
                });
            });
        });
        
    }
    
}
