using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "variables/int")]
public class int_var : ScriptableObject, IValue<int> {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	int constant = 0;

	[SerializeField]
	private int value;

	public int val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}
}
