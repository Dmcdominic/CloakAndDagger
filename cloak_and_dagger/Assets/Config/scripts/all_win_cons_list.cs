using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "config/all_win_cons_list")]
public class all_win_cons_list : ScriptableObject {

	public WinCon_WinConInfo_Dict win_con_infos;

}

[System.Serializable]
public class WinCon_WinConInfo_Dict : SerializableDictionary<win_condition, win_con_info> { }
