using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class invite_setter : MonoBehaviour {

    [SerializeField]
    Button accept_but;

    [SerializeField]
    Text message;

    public void invite(string name, Action accept)
    {
        accept_but.onClick.AddListener(() => accept());
        message.text = $"{name} wants to join your party";
    }
    public void request(string name, Action accept)
    {
        accept_but.onClick.AddListener(() => accept());
        message.text = $"{name} wants you to join their party";
    }

}
