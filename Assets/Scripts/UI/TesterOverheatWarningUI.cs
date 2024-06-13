using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TesterOverheatWarningUI : MonoBehaviour {


    [FormerlySerializedAs("testingStation")] [FormerlySerializedAs("stoveStation")] [FormerlySerializedAs("stoveCounter")] [SerializeField] private OSInstallationStation osInstallationStation;



    private void Start() {
        osInstallationStation.OnProgressChanged += OSInstallationStationOnProgressChanged;

        Hide();
    }

    private void OSInstallationStationOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = .5f;
        bool show = osInstallationStation.IsTested() && e.progressNormalized >= burnShowProgressAmount;

        if (show) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
