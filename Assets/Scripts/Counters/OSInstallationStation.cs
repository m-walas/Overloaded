using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static AssemblyStation;

public class OSInstallationStation : BaseStation, IHasProgress {


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }


    public enum State {
        Idle,
        Installing,
        Installed,
        BlueScreened,
    }


    [FormerlySerializedAs("fryingRecipeSOArray")] [SerializeField] private OSInstallationRecipeSO[] osInstallationRecipeSOArray;
    [FormerlySerializedAs("burningRecipeSOArray")] [SerializeField] private BlueScreeningRecipeSO[] blueScreeningRecipeSOArray;


    private State state;
    private float installingTimer;
    private OSInstallationRecipeSO osInstallingRecipeSo;
    private float blueScreeningTimer;
    private BlueScreeningRecipeSO blueScreeningRecipeSo;


    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasAssemblyObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Installing:
                    installingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = installingTimer / osInstallingRecipeSo.installationTimerMax
                    });

                    if (installingTimer > osInstallingRecipeSo.installationTimerMax) {
                        // Fried
                        GetAssemblyObject().DestroySelf();

                        AssemblyObject.SpawnAssemblyObject(osInstallingRecipeSo.output, this);

                        state = State.Installed;
                        blueScreeningTimer = 0f;
                        blueScreeningRecipeSo = GetBurningRecipeSOWithInput(GetAssemblyObject().GetAssemblyObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });
                    }
                    break;
                case State.Installed:
                    blueScreeningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = blueScreeningTimer / blueScreeningRecipeSo.bluescreenTimerMax
                    });

                    if (blueScreeningTimer > blueScreeningRecipeSo.bluescreenTimerMax) {
                        // Fried
                        GetAssemblyObject().DestroySelf();

                        AssemblyObject.SpawnAssemblyObject(blueScreeningRecipeSo.output, this);

                        state = State.BlueScreened;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.BlueScreened:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasAssemblyObject()) {
            // There is no KitchenObject here
            if (player.HasAssemblyObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetAssemblyObject().GetAssemblyObjectSO())) {
                    // Player carrying something that can be Fried
                    player.GetAssemblyObject().SetAssemblyObjectParent(this);

                    osInstallingRecipeSo = GetInstallingRecipeSOWithInput(GetAssemblyObject().GetAssemblyObjectSO());

                    state = State.Installing;
                    installingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = installingTimer / osInstallingRecipeSo.installationTimerMax
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

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            } else {
                // Player is not carrying anything
                GetAssemblyObject().SetAssemblyObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(AssemblyObjectSO inputAssemblyObjectSo) {
        OSInstallationRecipeSO testingRecipeSo = GetInstallingRecipeSOWithInput(inputAssemblyObjectSo);
        return testingRecipeSo != null;
    }


    private AssemblyObjectSO GetOutputForInput(AssemblyObjectSO inputAssemblyObjectSo) {
        OSInstallationRecipeSO testingRecipeSo = GetInstallingRecipeSOWithInput(inputAssemblyObjectSo);
        if (testingRecipeSo != null) {
            return testingRecipeSo.output;
        } else {
            return null;
        }
    }

    private OSInstallationRecipeSO GetInstallingRecipeSOWithInput(AssemblyObjectSO inputAssemblyObjectSo) {
        foreach (OSInstallationRecipeSO installingRecipeSO in osInstallationRecipeSOArray) {
            if (installingRecipeSO.input == inputAssemblyObjectSo) {
                return installingRecipeSO;
            }
        }
        return null;
    }

    private BlueScreeningRecipeSO GetBurningRecipeSOWithInput(AssemblyObjectSO inputAssemblyObjectSo) {
        foreach (BlueScreeningRecipeSO burningRecipeSO in blueScreeningRecipeSOArray) {
            if (burningRecipeSO.input == inputAssemblyObjectSo) {
                return burningRecipeSO;
            }
        }
        return null;
    }

    public bool IsTested() {
        return state == State.Installed;
    }

}
