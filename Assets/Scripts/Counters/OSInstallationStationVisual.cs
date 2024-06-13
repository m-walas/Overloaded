using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OSInstallationStationVisual : MonoBehaviour {


    [FormerlySerializedAs("testingStation")] [FormerlySerializedAs("stoveStation")] [FormerlySerializedAs("stoveCounter")] [SerializeField] private OSInstallationStation osInstallationStation;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;


    private void Start() {
        osInstallationStation.OnStateChanged += OSInstallationStationOnStateChanged;
    }

    private void OSInstallationStationOnStateChanged(object sender, OSInstallationStation.OnStateChangedEventArgs e) {
        bool showVisual = e.state == OSInstallationStation.State.Installing || e.state == OSInstallationStation.State.Installed;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }

}
