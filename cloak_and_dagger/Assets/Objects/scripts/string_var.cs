using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "variables/string")]
public class string_var : ScriptableObject {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	string constant = "";

	[SerializeField]
	private string value;

	public string val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}
}
