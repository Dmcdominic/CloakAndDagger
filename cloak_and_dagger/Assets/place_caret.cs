using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class place_caret : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(run());
	}
	
	IEnumerator run()
    {
        yield return null;
        transform.GetChild(0).SetSiblingIndex(transform.childCount - 1);
    }
}
