using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class save_config_data {

	// Path settings
	public static string generalPath() {
		return Application.persistentDataPath;
	}

	//private static string path() {
	//	return generalPath() + "/SaveData.dat";
	//}

	
	private static void save_to(string subpath, complete_config_data data) {
		//BinaryFormatter bf = new BinaryFormatter();
		//FileStream file;

		//if (!File.Exists(path())) {
		//	file = File.Create(path());
		//} else {
		//	file = File.Open(path(), FileMode.Open);
		//}

		//bf.Serialize(file, saveData);
		//file.Close();
	}

	private static complete_config_data load_from(string subpath) {
		//if (File.Exists(path())) {
		//	BinaryFormatter bf = new BinaryFormatter();
		//	FileStream file = File.Open(path(), FileMode.Open);

		//	_saveData = (SaveData)bf.Deserialize(file);
		//	file.Close();
		//} else {
		//	_saveData = new SaveData(-1);
		//}
		return new complete_config_data();
	}

}

[Serializable]
public class complete_config_data {
	public GameplayOption_Bool_Dict gameplay_bool_options;
	public GameplayOption_Float_Dict gameplay_float_options;
	public GameplayOption_Int_Dict gameplay_int_options;
}