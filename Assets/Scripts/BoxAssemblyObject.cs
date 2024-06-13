using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class BoxAssemblyObject : AssemblyObject {


    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public AssemblyObjectSO AssemblyObjectSo;
    }


    [FormerlySerializedAs("validKitchenObjectSOList")] [SerializeField] private List<AssemblyObjectSO> validAssemblyObjectSOList;


    private List<AssemblyObjectSO> assemblyObjectSOList;


    private void Awake() {
        assemblyObjectSOList = new List<AssemblyObjectSO>();
        
    }

    public bool TryAddIngredient(AssemblyObjectSO assemblyObjectSo) {
        print(assemblyObjectSo);
        if (!validAssemblyObjectSOList.Contains(assemblyObjectSo))
        {

            // Not a valid ingredient
            return false;
        }
        if (assemblyObjectSOList.Contains(assemblyObjectSo)) {
            // Already has this type
            return false;
        } else {
            assemblyObjectSOList.Add(assemblyObjectSo);
            print(assemblyObjectSo);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                AssemblyObjectSo = assemblyObjectSo
            });

            return true;
        }
    }

    public List<AssemblyObjectSO> GetAssemblyObjectSOList() {
        return assemblyObjectSOList;
    }

}
