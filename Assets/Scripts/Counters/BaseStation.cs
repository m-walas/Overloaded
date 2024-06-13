using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseStation : MonoBehaviour, IAssemblyObjectParent {


    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null;
    }


    [FormerlySerializedAs("counterTopPoint")] [SerializeField] private Transform stationTopPoint;


    private AssemblyObject _assemblyObject;


    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player) {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }


    public Transform GetAssemblyObjectFollowTransform() {
        return stationTopPoint;
    }

    public void SetAssemblyObject(AssemblyObject assemblyObject) {
        this._assemblyObject = assemblyObject;

        if (assemblyObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public AssemblyObject GetAssemblyObject() {
        return _assemblyObject;
    }

    public void ClearAssemblyObject() {
        _assemblyObject = null;
    }

    public bool HasAssemblyObject() {
        return _assemblyObject != null;
    }

}
