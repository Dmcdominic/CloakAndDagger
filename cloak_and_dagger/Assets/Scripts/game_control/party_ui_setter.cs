using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class party_ui_setter : MonoBehaviour {

    [SerializeField]
    party_var pn;

    [SerializeField]
    GameObject icon_prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject cur_member;
        int i;

        if(transform.childCount == 0)
        {
            Instantiate(icon_prefab, transform).GetComponent<character_icon_setter>().setter(0, pn.val.leader);
        } else
        {
            transform.GetChild(0).GetComponent<character_icon_setter>().setter(0, pn.val.leader);
        }

        for(i = 1; i <= pn.val.members.Count; i++)
        {
            if(transform.childCount == i)
            {
                cur_member = Instantiate(icon_prefab, transform);
                cur_member.transform.SetSiblingIndex(i);
            } else
            {
                cur_member = transform.GetChild(i).gameObject;
            }

            cur_member.GetComponent<character_icon_setter>().setter(i, pn.val.members[i - 1]);
        }
        for(int j = i; j < transform.childCount;j++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
	}
}
