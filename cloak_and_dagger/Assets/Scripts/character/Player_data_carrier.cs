using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_data_carrier : MonoBehaviour {

    public player_data player_Data;
}

public struct player_data {

    public player_data(sbyte tempID, string nickname) {
        this.tempID = tempID;
        this.nickname = nickname;
    }

    // Add more player_data properties here, and include them in the constructor
    public sbyte tempID;
    public string nickname;
}