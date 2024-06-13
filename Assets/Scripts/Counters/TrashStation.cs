using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashStation : BaseStation {


    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData() {
        OnAnyObjectTrashed = null;
    }



    public override void Interact(Player player) {
        if (player.HasAssemblyObject()) {
            player.GetAssemblyObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }

}