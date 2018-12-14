using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	
 *	json_list:
 *	A scriptable object for serializing a list of jsons (or any TextAssets).
 *	Created by Dominic Calkosz.
 */
[CreateAssetMenu(menuName="config/json_list")]
public class json_list : ScriptableObject {

	public List<TextAsset> jsons;

}
