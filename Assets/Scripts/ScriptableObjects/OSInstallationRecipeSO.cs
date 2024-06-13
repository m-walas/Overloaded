using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class OSInstallationRecipeSO : ScriptableObject {


    public AssemblyObjectSO input;
    public AssemblyObjectSO output;
    [FormerlySerializedAs("fryingTimerMax")] public float installationTimerMax;


}
