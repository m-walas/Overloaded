using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class BlueScreeningRecipeSO : ScriptableObject {


    public AssemblyObjectSO input;
    public AssemblyObjectSO output;
    [FormerlySerializedAs("burningTimerMax")] public float bluescreenTimerMax;


}
