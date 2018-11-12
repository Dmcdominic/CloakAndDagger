using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public interface IValue<T>
{
    T val { get; set; }
}


[CreateAssetMenu(menuName = "variables/float")]
public class float_var : ScriptableObject, IValue<float> {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	float constant = 0;

	[SerializeField]
	private float value;

	public float val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}
    public static implicit operator float(float_var v)
    {
        return v.val;
    }
}


