using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class friend_ui_setter : MonoBehaviour {

    [SerializeField]
    friends_list my_friends;

    [SerializeField]
    GameObject friend_prefab;

    [SerializeField]
    string_list requests;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        requests.val = requests.val.Where(str => !my_friends.val.Exists(cs => cs.name == str)).ToList();

        GameObject cur_friend;
        int i;
        for(i = 0; i < my_friends.val.Count + requests.val.Count; i++)
        {
            if(transform.childCount == i)
            {
                cur_friend = Instantiate(friend_prefab, transform);
                cur_friend.transform.SetSiblingIndex(i);
            }
            else
            {
                cur_friend = transform.GetChild(i).gameObject;
            }
            if(i < requests.val.Count)
            {
                cur_friend.GetComponent<set_friend>().set_request(requests.val[i]);
            } else
            {
                cur_friend.GetComponent<set_friend>().set_me(my_friends.val[i - requests.val.Count]);
            }
        }
        for(int j = i; j < transform.childCount; j++)
        {
            transform.GetChild(j).gameObject.SetActive(false);
        }
	}
}
