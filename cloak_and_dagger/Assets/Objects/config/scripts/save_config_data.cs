using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class save_config_data : MonoBehaviour {

	// Replace this with a serializable dictionary?
	public gameplay_config gameplay_Config;

	public void save_all_config_data() {

	}

	public void load_all_config_data() {

	}

}

[Serializable]
public class complete_config_data {
	public Dictionary<config_category, object> config_data;
}