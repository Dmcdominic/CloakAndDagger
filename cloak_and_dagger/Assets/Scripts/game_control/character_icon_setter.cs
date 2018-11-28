using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character_icon_setter : MonoBehaviour {
    [SerializeField]
    sprite_table sprite_lookup;

    [SerializeField]
    Text my_name;

    [SerializeField]
    Image my_picture;

    [SerializeField]
    int_var local_id;


    [SerializeField]
    GameObject local_stuff;

    [SerializeField]
    sync_event network_character_select;

    [SerializeField]
    sync_event local_character_select;

    [SerializeField]
    team_setter team_selecter;
    

    public int my_char = -1;

    int my_color = 0;

    public void Start()
    {
        network_character_select.e.AddListener((d, o, i) => 
        {
            int pallette = (int)o;
            if (i == transform.GetSiblingIndex())
            {
                my_char = pallette / 2;
                my_color = pallette % 2;
            }
        });
        local_character_select.e.AddListener((d, o, i) => {
            if (i == transform.GetSiblingIndex()) { my_char = (int)o / 2; my_color = (int)o % 2; } });
    }

    public void setter(int i, string name)
    {
        my_name.text = name;
        if (my_char == -1) my_char = i;
        my_picture.sprite = sprite_lookup.data[new Vector2(my_char, my_color)];
        local_stuff.SetActive(i == local_id);
        team_selecter.id = transform.GetSiblingIndex();
    }

    public void color_right()
    { 
        local_character_select.Invoke(0, (my_char * 2) + (my_color + 1) % 2, local_id, reliable: true);
    }

    public void color_left()
    {
        local_character_select.Invoke(0, (my_char * 2) + Mathf.Abs((my_color - 1) % 2), local_id, reliable: true);

    }
}
