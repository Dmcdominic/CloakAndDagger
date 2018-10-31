using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour {

    [SerializeField]
    float_event_object start;

    [SerializeField]
    event_object done_initing;

    [SerializeField]
    int_var game_scene;

	[SerializeField]
	map_config map_Config;

    [SerializeField]
    bool_var ingame;

    [SerializeField]
    party_var party;

    [SerializeField]
    GameObject player_prefab;

    [SerializeField]
    string_var local_name;

    [SerializeField]
    int_var local_network_id;

    [SerializeField]
    float_var t0;

	// Use this for initialization
	void Start () {
        start.e.AddListener((t) => StartCoroutine(go(t)));
	}
	
    IEnumerator go(float t)
    {
        yield return new WaitUntil(() => party.val.leader != "");
        SceneManager.LoadScene(game_scene.val);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == game_scene.val);

		Vector2 spawn_point = map_Config.current_map_info.next_spawn_point;
		GameObject leader_go = Instantiate(player_prefab,spawn_point,Quaternion.identity);

        network_id leader_id = leader_go.GetComponent<network_id>();
        leader_id.val = 0;
        if(party.val.leader == local_name.val)
        {
            local_network_id.val = leader_id.val;
        }

        GameObject member_go;
        network_id net_id;
        int i = 1;
        foreach(string member in party.val.members)
        {
			spawn_point = map_Config.current_map_info.next_spawn_point;
			member_go = Instantiate(player_prefab, spawn_point, Quaternion.identity);
            net_id = member_go.GetComponent<network_id>();
            net_id.val = i;
            i++;
            if (member == local_name.val)
            {
                local_network_id.val = net_id.val;
            }
        }
        data.local_id = local_network_id;
        t0.val = t;
        ingame.val = true;
        done_initing.Invoke();
    }

}
