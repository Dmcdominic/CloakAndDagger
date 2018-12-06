using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_hoc_animator_shit : MonoBehaviour {

    Animator am;

    [SerializeField]
    event_object char_out;

	// Use this for initialization
	void Start () {
        am = GetComponent<Animator>();
        char_out.e.AddListener(() => { if (am.GetCurrentAnimatorStateInfo(0).IsName("characters_in") ) am.SetTrigger("toggle"); });
	}
	
	
}
