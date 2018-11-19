using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class friend_add_button : MonoBehaviour {

    Button add;

    [SerializeField]
    Text friend_name;

	// Use this for initialization
	void Start () {
        add = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {

        add.interactable = friend_name.text.Length != 0;

    }
}
