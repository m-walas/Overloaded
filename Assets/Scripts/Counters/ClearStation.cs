using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearStation : BaseStation {


    [FormerlySerializedAs("kitchenObjectSO")] [SerializeField] private AssemblyObjectSO assemblyObjectSo;


    public override void Interact(Player player) {
        if (!HasAssemblyObject()) {
            // There is no AssemblyObject here
            if (player.HasAssemblyObject()) {
                // Player is carrying something
                player.GetAssemblyObject().SetAssemblyObjectParent(this);
            } else {
                // Player not carrying anything
            }
        } else {
            // There is an AssemblyObject here
            if (player.HasAssemblyObject()) {
                // Player is carrying something
                if (player.GetAssemblyObject().TryGetBox(out BoxAssemblyObject boxAssemblyObject)) {
                    // Player is holding a Box
                    print(GetAssemblyObject().GetAssemblyObjectSO());
                    if (boxAssemblyObject.TryAddIngredient(GetAssemblyObject().GetAssemblyObjectSO())) {
                        GetAssemblyObject().DestroySelf();
                    }
                } else {
                    // Player is not carrying Plate but something else
                    if (GetAssemblyObject().TryGetBox(out boxAssemblyObject)) {
                        // Counter is holding a Plate
                        if (boxAssemblyObject.TryAddIngredient(player.GetAssemblyObject().GetAssemblyObjectSO())) {
                            player.GetAssemblyObject().DestroySelf();
                        }
                    }
                }
            } else {
                // Player is not carrying anything
                GetAssemblyObject().SetAssemblyObjectParent(player);
            }
        }
    }

}
