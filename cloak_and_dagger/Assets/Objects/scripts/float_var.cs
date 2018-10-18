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


[CreateAssetMenu(menuName = "variables/client")]
public class client_var : ScriptableObject
{


    [SerializeField]
    private IProtagoras_Client<object> value;

    public IProtagoras_Client<object> val
    {
        get { return value; }
        set { this.value = value; }
    }
}


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

[CreateAssetMenu(menuName = "variables/vec2_list")]
public class vec2_list : ScriptableObject
{


    [SerializeField]
    private List<Vector2> value;

    private int pos = -1;

    public List<Vector2> val
    {
        get { return value; }
        set { this.value = value; }
    }

    public Vector2 next
    {
        get { pos++; pos %= value.Count; return value[pos]; }
    }
}


[CreateAssetMenu(menuName="variables/scene")]
public class scene_var : ScriptableObject
{
    [SerializeField]
    bool use_constant;

    [SerializeField]
    Scene constant;

    [SerializeField]
    private Scene value;

    public Scene val
    {
        get { return use_constant ? constant : value; }
        set { this.value = value; }
    }
}
