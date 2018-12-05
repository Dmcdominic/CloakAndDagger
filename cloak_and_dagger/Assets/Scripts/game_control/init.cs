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
	map_config map_Config;

    [SerializeField]
    gameplay_config gc;

	[SerializeField]
	string_event_object load_map;

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

    [SerializeField]
    player_float respawn_times;

    [SerializeField]
    GameObject status_handler;
    

	// Use this for initialization
	void Start () {
        start.e.AddListener((t) => StartCoroutine(go(t)));
	}

	IEnumerator go(float t)
    {
        print("**");
		yield return new WaitUntil(() => party.val.leader != "");
        print("#@");
		// Original version
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(map_Config.map);

		// Full pre-load version (must uncomment lines in map_loader
		//load_map.Invoke(map_Config.map);

		// Single async load version
		//AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(map_Config.map);
		//asyncOperation.allowSceneActivation = false;
		//while (asyncOperation.progress < 0.9f) {
		//	yield return null;
		//}
		//asyncOperation.allowSceneActivation = true;

		status_handler.SetActive(true);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == map_Config.map);

		map_Config.current_map_info.init_spawnpoints();

		Vector2 spawn_point = map_Config.next_spawn_point(0);
		GameObject leader_go = Instantiate(player_prefab, spawn_point, Quaternion.identity);

		network_id leader_id = leader_go.GetComponent<network_id>();
		leader_id.val = 0;
		if (party.val.leader == local_name.val) {
			local_network_id.val = leader_id.val;
			respawn_times[leader_id.val] = gc.float_options[gameplay_float_option.respawn_delay];
		}

		GameObject member_go;
		network_id net_id;
		int i = 1;
		if (party && party.val.members != null) {
			//foreach (string member in party.val.members) {
			for (int id = 1; id < party.val.members.Count + 1; id++) {
				string member = party.val.members[id - 1];
				spawn_point = map_Config.next_spawn_point(id);
				member_go = Instantiate(player_prefab, spawn_point, Quaternion.identity);
				net_id = member_go.GetComponent<network_id>();
				net_id.val = i;
				i++;
				if (member == local_name.val) {
					local_network_id.val = net_id.val;
					respawn_times[net_id.val] = gc.float_options[gameplay_float_option.respawn_delay];
				}
			}
		}
        data.local_id = local_network_id;
        t0.val = t;
        done_initing.Invoke();
    }

}
