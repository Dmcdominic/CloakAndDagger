using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_data_carrier : MonoBehaviour {

    public player_data player_Data;
}

public struct player_data {

    public player_data(byte playerID, string nickname) {
        this.playerID = playerID;
        this.nickname = nickname;
    }

    // Add more player_data properties here, and include them in the constructor
    public byte playerID;
    public string nickname;
}
