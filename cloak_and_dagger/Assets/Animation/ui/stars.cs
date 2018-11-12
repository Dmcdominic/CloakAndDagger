using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class stars : MonoBehaviour {

    [SerializeField]
    Text to_copy;

    [SerializeField]
    Text mine;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		mine.text = "";
        for (int i = 0; i < to_copy.text.Length; i++)
        {
            mine.text += "*";
        }
	}
}
