using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssemblyObjectParent {

    public Transform GetAssemblyObjectFollowTransform();

    public void SetAssemblyObject(AssemblyObject assemblyObject);

    public AssemblyObject GetAssemblyObject();

    public void ClearAssemblyObject();

    public bool HasAssemblyObject();

}
