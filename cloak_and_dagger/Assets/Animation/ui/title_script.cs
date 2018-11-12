using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class title_script : MonoBehaviour {

    Animator am;


    [SerializeField]
    GameObject login_fields;

	// Use this for initialization
	void Start () {
        am = GetComponent<Animator>();
        StartCoroutine(delay_pop_up());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator delay_pop_up()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        am.SetTrigger("any_button");
        yield return new WaitForSeconds(.1f);
        login_fields.GetComponent<Animator>().SetTrigger("fade_in");
    }
}
