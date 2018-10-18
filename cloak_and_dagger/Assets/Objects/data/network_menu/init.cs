using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour {

    [SerializeField]
    event_object start;

    [SerializeField]
    int_var game_scene;

    [SerializeField]
    vec2_list spawn_points;

    [SerializeField]
    party_var party;

    [SerializeField]
    GameObject player_prefab;

	// Use this for initialization
	void Start () {
        start.e.AddListener(go);
	}
	
    void go()
    {
        SceneManager.LoadScene(game_scene.val);
        GameObject leader_go = Instantiate(player_prefab,spawn_points.next,Quaternion.identity);
    }

}
