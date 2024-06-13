using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class AssemblingRecipeSO : ScriptableObject {


    public AssemblyObjectSO input;
    public AssemblyObjectSO output;
    [FormerlySerializedAs("cuttingProgressMax")] public int assemblingProgressMax;


}
