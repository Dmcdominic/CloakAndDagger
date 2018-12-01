using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class death_fov : MonoBehaviour {


    

    [SerializeField]
    player_bool is_dead;

    PostProcessingProfile ppp;

	// Use this for initialization
	void Start () {
        ppp = GetComponent<PostProcessingBehaviour>().profile;
	}
	
	// Update is called once per frame
	void Update () {
        ppp.depthOfField.enabled = is_dead.loc;
	}
}
