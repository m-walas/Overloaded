using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryStation : BaseStation {


    public static DeliveryStation Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }


    public override void Interact(Player player) {
        if (player.HasAssemblyObject()) {
            if (player.GetAssemblyObject().TryGetBox(out BoxAssemblyObject boxAssemblyObject)) {
                // Only accepts Plates

                DeliveryManager.Instance.DeliverRecipe(boxAssemblyObject);

                player.GetAssemblyObject().DestroySelf();
            }
        }
    }

}
