using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TesterOverheatFlashingBarUI : MonoBehaviour {


    private const string IS_FLASHING = "IsFlashing";


    [FormerlySerializedAs("testingStation")] [FormerlySerializedAs("stoveStation")] [FormerlySerializedAs("stoveCounter")] [SerializeField] private OSInstallationStation osInstallationStation;


    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        osInstallationStation.OnProgressChanged += OSInstallationStationOnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void OSInstallationStationOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = .5f;
        bool show = osInstallationStation.IsTested() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }

}
