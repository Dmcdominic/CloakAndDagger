using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum notification_type { request, invite, friend }

public class party_notification_object : ScriptableObject {

    public Action accept;

    public notification_type type;

    public string actor;

    public string message
    {
        get
        {
            switch (type)
            {
                case notification_type.request:
                    return $"{actor} wants you join your party!";
                case notification_type.invite:
                    return $"{actor} has invited you to their party";
                case notification_type.friend:
                    return $"{actor} wants to be your friend!";
            }
            return "";
        }
    }
    //previous stack
    public void setup(Action accept, notification_type type, string actor)
    {
        this.accept = accept;
        this.type = type;
        this.actor = actor;
    }
}
