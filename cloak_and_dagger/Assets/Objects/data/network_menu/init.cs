using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour {

    [SerializeField]
    event_object start;

    [SerializeField]
    event_object done_initing;

    [SerializeField]
    int_var game_scene;

    [SerializeField]
    vec2_list spawn_points;

    [SerializeField]
    party_var party;

    [SerializeField]
    GameObject player_prefab;

    [SerializeField]
    string_var local_name;

    [SerializeField]
    int_var local_network_id;

	// Use this for initialization
	void Start () {
        start.e.AddListener(() => StartCoroutine(go()));
	}
	
    IEnumerator go()
    {
        yield return new WaitUntil(() => party.val.leader != "");
        SceneManager.LoadScene(game_scene.val);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == game_scene.val);

        GameObject leader_go = Instantiate(player_prefab,spawn_points.next,Quaternion.identity);

        network_id leader_id = leader_go.GetComponent<network_id>();
        leader_id.val = 0;
        if(party.val.leader == local_name.val)
        {
            local_network_id.val = leader_id.val;
        }

        GameObject member_go;
        network_id net_id;
        foreach(string member in party.val.members)
        {
            member_go = Instantiate(player_prefab, spawn_points.next, Quaternion.identity);
            net_id = member_go.GetComponent<network_id>();
            if(member == local_name.val)
            {
                local_network_id.val = net_id.val;
            }
        }

        done_initing.Invoke();
    }

}
