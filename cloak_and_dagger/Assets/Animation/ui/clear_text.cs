using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class clear_text : MonoBehaviour {

	public void OnEnable()
    {
        Text t = GetComponent<Text>();
        t.text = "";
        foreach(InputField inp in GetComponentsInParent<InputField>())
        {
            if (inp.textComponent == t) inp.text = ""; 
        }
    }
}
