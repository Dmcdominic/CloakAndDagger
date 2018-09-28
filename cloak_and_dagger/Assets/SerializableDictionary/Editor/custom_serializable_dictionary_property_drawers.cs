﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// ========== Status dictionaries ==========
[CustomPropertyDrawer(typeof(Status_BoolVar_Dict))]
[CustomPropertyDrawer(typeof(Status_EventObject_Dict))]
[CustomPropertyDrawer(typeof(Status_FloatEventObject_Dict))]
[CustomPropertyDrawer(typeof(Status_Float_Dict))]
// ============= Data persistence dicts ================
[CustomPropertyDrawer(typeof(ConfigCat_ScriptableObj_Dict))]
// ========== Config object enum dictionaries ==========
[CustomPropertyDrawer(typeof(GameplayOption_Bool_Dict))]
[CustomPropertyDrawer(typeof(GameplayOption_Float_Dict))]
[CustomPropertyDrawer(typeof(GameplayOption_Int_Dict))]
// =========== Readonly config dictionaries ============
[CustomPropertyDrawer(typeof(ReadonlyGameplayOption_Bool_Dict))]
[CustomPropertyDrawer(typeof(ReadonlyGameplayOption_Float_Dict))]
[CustomPropertyDrawer(typeof(ReadonlyGameplayOption_Int_Dict))]
public class Custom_AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

//[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class Custom_AnySerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }