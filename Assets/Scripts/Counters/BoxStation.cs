using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxStation : BaseStation {


    public event EventHandler OnBoxSpawned;
    public event EventHandler OnBoxRemoved;


    [FormerlySerializedAs("plateAssemblyObjectSo")] [FormerlySerializedAs("plateKitchenObjectSO")] [SerializeField] private AssemblyObjectSO boxAssemblyObjectSo;


    private float spawnBoxTimer;
    private float spawnBoxTimerMax = 4f;
    private int boxesSpawnedAmount;
    private int boxesSpawnedAmountMax = 4;


    private void Update() {
        spawnBoxTimer += Time.deltaTime;
        if (spawnBoxTimer > spawnBoxTimerMax) {
            spawnBoxTimer = 0f;

            if (OverloadedGameManager.Instance.IsGamePlaying() && boxesSpawnedAmount < boxesSpawnedAmountMax) {
                boxesSpawnedAmount++;

                OnBoxSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasAssemblyObject()) {
            // Player is empty handed
            if (boxesSpawnedAmount > 0) {
                // There's at least one plate here
                boxesSpawnedAmount--;

                AssemblyObject.SpawnAssemblyObject(boxAssemblyObjectSo, player);

                OnBoxRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
