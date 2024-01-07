using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Value", menuName = "BLT/Values/Bool Value", order = 100)]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver {

    //Este scriptable object serializa un int, para poder referenciarlo en diferentes componentes sin que estos tengan referencia entre si.
    public bool boolValue;

    //Se accede a runtimeValue para evitar modificar el valor serializado en el editor y conservar su valor
    [NonSerialized]
    public bool runtimeValue;

    public void OnAfterDeserialize() {
        //Reseteamos el runtime value al valor serializado
        runtimeValue = boolValue;
    }

    public void OnBeforeSerialize() {
    }
}
