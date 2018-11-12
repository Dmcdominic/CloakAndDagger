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

    public void set_me(connection_struct cs)
    {
        my_name.text = cs.name;
        if(cs.is_in_game)
        {
            if(cs.is_in_game)
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
                    info.text += "| Cloak & Dagger";
                    break;
            }
        }
        else
        {
            info.text = "Offline";
        }

        invite.onClick.AddListener(() => invite_player.Invoke(cs.name));
        invite.onClick.AddListener(() => join_player.Invoke(cs.name));
    }
    
}
