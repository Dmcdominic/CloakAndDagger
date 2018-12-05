using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_on_party : MonoBehaviour {

    [SerializeField]
    string trigger_name;

  

    [SerializeField]
    float wait_time = 3.5f;

	// Use this for initialization
	void OnEnable () {
        Invoke("go", wait_time);
        
	}
	
    void go()
    {
        GetComponent<Animator>().SetTrigger(trigger_name);
    }

    public void off()
    {
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
