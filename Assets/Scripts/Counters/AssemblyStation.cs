using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyStation : BaseStation, IHasProgress {


    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData() {
        OnAnyCut = null;
    }


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;


    [SerializeField] private AssemblingRecipeSO[] cuttingRecipeSOArray;


    private int cuttingProgress;


    public override void Interact(Player player) {
        if (!HasAssemblyObject()) {
            // There is no AssemblyObject here
            if (player.HasAssemblyObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetAssemblyObject().GetAssemblyObjectSO())) {
                    // Player carrying something that can be Cut
                    player.GetAssemblyObject().SetAssemblyObjectParent(this);
                    cuttingProgress = 0;

                    AssemblingRecipeSO assemblingRecipeSo = GetCuttingRecipeSOWithInput(GetAssemblyObject().GetAssemblyObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / assemblingRecipeSo.assemblingProgressMax
                    });
                }
            } else {
                // Player not carrying anything
            }
        } else {
            // There is an AssemblyObject here
            if (player.HasAssemblyObject()) {
                // Player is carrying something
                if (player.GetAssemblyObject().TryGetBox(out BoxAssemblyObject boxAssemblyObject)) {
                    // Player is holding a Plate
                    if (boxAssemblyObject.TryAddIngredient(GetAssemblyObject().GetAssemblyObjectSO())) {
                        GetAssemblyObject().DestroySelf();
                    }
                }
            } else {
                // Player is not carrying anything
                GetAssemblyObject().SetAssemblyObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasAssemblyObject() && HasRecipeWithInput(GetAssemblyObject().GetAssemblyObjectSO())) {
            // There is a AssemblyObject here AND it can be assembled
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            AssemblingRecipeSO assemblingRecipeSo = GetCuttingRecipeSOWithInput(GetAssemblyObject().GetAssemblyObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / assemblingRecipeSo.assemblingProgressMax
            });

            if (cuttingProgress >= assemblingRecipeSo.assemblingProgressMax) {
                AssemblyObjectSO outputAssemblyObjectSo = GetOutputForInput(GetAssemblyObject().GetAssemblyObjectSO());

                GetAssemblyObject().DestroySelf();

                AssemblyObject.SpawnAssemblyObject(outputAssemblyObjectSo, this);
            }
        }
    }

    private bool HasRecipeWithInput(AssemblyObjectSO inputAssemblyObjectSo) {
        AssemblingRecipeSO assemblingRecipeSo = GetCuttingRecipeSOWithInput(inputAssemblyObjectSo);
        return assemblingRecipeSo != null;
    }


    private AssemblyObjectSO GetOutputForInput(AssemblyObjectSO inputAssemblyObjectSo) {
        AssemblingRecipeSO assemblingRecipeSo = GetCuttingRecipeSOWithInput(inputAssemblyObjectSo);
        if (assemblingRecipeSo != null) {
            return assemblingRecipeSo.output;
        } else {
            return null;
        }
    }

    private AssemblingRecipeSO GetCuttingRecipeSOWithInput(AssemblyObjectSO inputAssemblyObjectSo) {
        foreach (AssemblingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputAssemblyObjectSo) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
