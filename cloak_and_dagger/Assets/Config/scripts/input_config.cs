using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenuAttribute(menuName = "config/input_config")]
public class input_config : ScriptableObject {

	[SerializeField]
	KeyCode left;
	[SerializeField]
	KeyCode right;
	[SerializeField]
	KeyCode up;
	[SerializeField]
	KeyCode down;

	[SerializeField]
	KeyCode dagger_key;
	public bool dagger
	{
		get {return Input.GetKeyDown(dagger_key);}
	}

	[SerializeField]
	KeyCode fireball_key;
	public bool fireball {
		get { return Input.GetKeyDown(fireball_key); }
	}

	[SerializeField]
	KeyCode dash_key;
	public bool dash
	{
		get { return Input.GetKeyDown(dash_key); }
	}

	[SerializeField]
	KeyCode reflect_key;
	public bool reflect {
		get { return Input.GetKeyDown(reflect_key); }
	}

	[SerializeField]
	string horizontal_axis;
	[SerializeField]
	string vertical_axis;

	[SerializeField]
	bool use_axis;

	float adhoc(bool b) {return b ? 1 : 0;}

	public float horizontal()
	{
		if(use_axis)
		{
			return Input.GetAxisRaw(horizontal_axis);
		}
		return adhoc(Input.GetKey(right)) - adhoc(Input.GetKey(left));
	}

	public float vertical()
	{
		if(use_axis)
		{
			return Input.GetAxisRaw(vertical_axis);
		}
		return adhoc(Input.GetKey(up)) - adhoc(Input.GetKey(down));
	}
	
}
