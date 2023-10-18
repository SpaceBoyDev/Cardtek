using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Value", menuName = "BLT/Values/Float Value", order = 100)]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {

    //Este scriptable object serializa un int, para poder referenciarlo en diferentes componentes sin que estos tengan referencia entre si.
    public float floatValue;

    //Se accede a runtimeValue para evitar modificar el valor serializado en el editor y conservar su valor
    [NonSerialized]
    public float runtimeValue;

    public void OnAfterDeserialize() {
        //Reseteamos el runtime value al valor serializado
        runtimeValue = floatValue;
    }

    public void OnBeforeSerialize() {
    }
}