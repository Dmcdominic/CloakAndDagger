using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ========== Status dictionaries ==========
[System.Serializable]
public class Status_BoolVar_Dict : SerializableDictionary<status, player_bool> { }

[System.Serializable]
public class Status_EventObject_Dict : SerializableDictionary<status, int_event_object> { }

[System.Serializable]
public class Status_FloatEventObject_Dict : SerializableDictionary<status, int_float_event> { }

[System.Serializable]
public class Status_Float_Dict : SerializableDictionary<status, player_float> { }
