using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxCompleteVisual : MonoBehaviour {


    [Serializable]
    public struct AssemblyObjectSO_GameObject {

        [FormerlySerializedAs("kitchenObjectSO")] public AssemblyObjectSO assemblyObjectSo;
        public GameObject gameObject;

    }


    [FormerlySerializedAs("plateAssemblyObject")] [FormerlySerializedAs("plateKitchenObject")] [SerializeField] private BoxAssemblyObject boxAssemblyObject;
    [FormerlySerializedAs("kitchenObjectSOGameObjectList")] [SerializeField] private List<AssemblyObjectSO_GameObject> assemblyObjectSOGameObjectList;


    private void Start() {
        boxAssemblyObject.OnIngredientAdded += BoxAssemblyObjectOnIngredientAdded;

        foreach (AssemblyObjectSO_GameObject assemblyObjectSOGameObject in assemblyObjectSOGameObjectList) {
            assemblyObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void BoxAssemblyObjectOnIngredientAdded(object sender, BoxAssemblyObject.OnIngredientAddedEventArgs e) {
        foreach (AssemblyObjectSO_GameObject assemblyObjectSOGameObject in assemblyObjectSOGameObjectList) {
            if (assemblyObjectSOGameObject.assemblyObjectSo == e.AssemblyObjectSo) {
                assemblyObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }

}
