using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "variables/bool")]
public class bool_var : ScriptableObject, IValue<bool> {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	bool constant = false;

	[SerializeField]
	private bool value;

	public bool val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}
}
