using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "config/all_maps_list")]
public class all_maps_list : ScriptableObject {

	public String_MapInfo_Dict map_infos;

}

[System.Serializable]
public class String_MapInfo_Dict : SerializableDictionary<string, map_info> { }
