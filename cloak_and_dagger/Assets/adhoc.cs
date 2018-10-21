using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct my_state
{

}

public class adhoc : sync_behaviour<my_state> {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}


    public override void rectify(float t, my_state state)
    {
        
    }


    // Update is called once per frame
    void Update () {
        send_state(new my_state());
	}
}
