using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "variables/party_info")]
public class party_var : ScriptableObject
{


    [SerializeField]
    private Party_Names value = new Party_Names();

    public Party_Names val
    {
        get { return value; }
        set { this.value = value; }
    }
}
