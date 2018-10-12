using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum death_type {dagger, suicide};

[System.Serializable]
//[CreateAssetMenu(menuName = "events/death_event")]
public class death_event_object : ScriptableObject {
    [System.Serializable]
    public class death_event : UnityEvent<death_event_data> { };

    [SerializeField]
    public death_event e = new death_event();

    public void Invoke(death_event_data death_Event_Data) {
        e.Invoke(death_Event_Data);
    }
}

public struct death_event_data
{
    public death_event_data(byte playerID, death_type death_Type, bool terminated, byte killerID) {
        this.playerID = playerID;
        this.death_Type = death_Type;
		this.terminated = terminated;
        this.killerID = killerID;
    }

    public byte playerID;
    public death_type death_Type;
	public bool terminated; // I.e. it was their last life
    public byte killerID;
}