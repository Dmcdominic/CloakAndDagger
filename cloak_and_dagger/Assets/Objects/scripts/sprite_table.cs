using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "variables/sprite table")]
public class sprite_table : ScriptableObject {

    [SerializeField]
    public sprite_dict data;

}

[System.Serializable]
public class sprite_dict : SerializableDictionary<Vector2,Sprite> { }