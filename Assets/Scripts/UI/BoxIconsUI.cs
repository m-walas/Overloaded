using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxIconsUI : MonoBehaviour {


    [FormerlySerializedAs("plateAssemblyObject")] [FormerlySerializedAs("plateKitchenObject")] [SerializeField] private BoxAssemblyObject boxAssemblyObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        boxAssemblyObject.OnIngredientAdded += BoxAssemblyObjectOnIngredientAdded;
    }

    private void BoxAssemblyObjectOnIngredientAdded(object sender, BoxAssemblyObject.OnIngredientAddedEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (AssemblyObjectSO assemblyObjectSo in boxAssemblyObject.GetAssemblyObjectSOList()) {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<BoxIconsSingleUI>().SetAssemblyObjectSO(assemblyObjectSo);
        }
    }

}
