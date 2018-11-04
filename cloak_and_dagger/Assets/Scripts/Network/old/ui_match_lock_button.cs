using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ui_match_lock_button : MonoBehaviour {

	[SerializeField]
	Sprite locked;
	[SerializeField]
	Sprite unlocked;
	
	[HideInInspector]
	public bool is_locked = false;

	Image im;

	void Start()
	{
		im = GetComponent<Image>();
	}


	// Update is called once per frame
	void Update () { //this shouldn't even check after being assigned but this can be fixed later
		if(is_locked)
		{
			im.sprite = locked;
		}
		else 
		{
			im.sprite = unlocked;
		}
	}
}
