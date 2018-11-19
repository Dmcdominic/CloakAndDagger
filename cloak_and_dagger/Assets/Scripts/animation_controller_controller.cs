using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class animation_controller_controller : MonoBehaviour {

    [SerializeField]
    Animation_Trigger_Dict triggers;

    [SerializeField]
    Int_var_to_string ints;

    [SerializeField]
    Bool_var_to_string bools;

    [SerializeField]
    Float_var_to_string floats;

    Animator am;

	// Use this for initialization
	void Start () {
        am = GetComponent<Animator>();
		foreach(KeyValuePair<event_object,string> p in triggers)
        {
            p.Key.e.AddListener(() => am.SetTrigger(p.Value));
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (KeyValuePair<int_var, string> p in ints)
        {
            am.SetInteger(p.Value, p.Key);
        }
        foreach (KeyValuePair<bool_var, string> p in bools)
        {
            am.SetBool(p.Value, p.Key);
        }
        foreach (KeyValuePair<float_var, string> p in floats)
        {
            am.SetFloat(p.Value, p.Key);
        }
    }
}
