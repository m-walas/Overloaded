using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject {


    [FormerlySerializedAs("kitchenObjectSOList")] public List<AssemblyObjectSO> assemblyObjectSOList;
    public string recipeName;


}
