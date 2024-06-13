using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AssemblyObject : MonoBehaviour {


    [FormerlySerializedAs("kitchenObjectSO")] [SerializeField] private AssemblyObjectSO assemblyObjectSo;


    private IAssemblyObjectParent _assemblyObjectParent;


    public AssemblyObjectSO GetAssemblyObjectSO() {
        return assemblyObjectSo;
    }

    public void SetAssemblyObjectParent(IAssemblyObjectParent assemblyObjectParent) {
        if (this._assemblyObjectParent != null) {
            this._assemblyObjectParent.ClearAssemblyObject();
        }

        this._assemblyObjectParent = assemblyObjectParent;

        if (assemblyObjectParent.HasAssemblyObject()) {
            Debug.LogError("IAssemblyObjectParent already has an AssemblyObject!");
        }

        assemblyObjectParent.SetAssemblyObject(this);

        transform.parent = assemblyObjectParent.GetAssemblyObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IAssemblyObjectParent GetAssemblyObjectParent() {
        return _assemblyObjectParent;
    }

    public void DestroySelf() {
        _assemblyObjectParent.ClearAssemblyObject();

        Destroy(gameObject);
    }

    public bool TryGetBox(out BoxAssemblyObject boxAssemblyObject) {
        if (this is BoxAssemblyObject) {
            boxAssemblyObject = this as BoxAssemblyObject;
            return true;
        } else {
            boxAssemblyObject = null;
            return false;
        }
    }



    public static AssemblyObject SpawnAssemblyObject(AssemblyObjectSO assemblyObjectSo, IAssemblyObjectParent assemblyObjectParent) {
        Transform assemblyObjectTransform = Instantiate(assemblyObjectSo.prefab);

        AssemblyObject assemblyObject = assemblyObjectTransform.GetComponent<AssemblyObject>();

        assemblyObject.SetAssemblyObjectParent(assemblyObjectParent);

        return assemblyObject;
    }

}
