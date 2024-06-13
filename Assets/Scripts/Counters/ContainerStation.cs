using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContainerStation : BaseStation {


    public event EventHandler OnPlayerGrabbedObject;


    [FormerlySerializedAs("kitchenObjectSO")] [SerializeField] private AssemblyObjectSO assemblyObjectSo;


    public override void Interact(Player player) {
        if (!player.HasAssemblyObject()) {
            // Player is not carrying anything
            AssemblyObject.SpawnAssemblyObject(assemblyObjectSo, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}