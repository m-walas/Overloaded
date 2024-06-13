using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OSInstallationSound : MonoBehaviour {


    [FormerlySerializedAs("testingStation")] [FormerlySerializedAs("stoveStation")] [FormerlySerializedAs("stoveCounter")] [SerializeField] private OSInstallationStation osInstallationStation;


    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        osInstallationStation.OnStateChanged += OSInstallationStationOnStateChanged;
        osInstallationStation.OnProgressChanged += OSInstallationStationOnProgressChanged;
    }

    private void OSInstallationStationOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = .5f;
        playWarningSound = osInstallationStation.IsTested() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void OSInstallationStationOnStateChanged(object sender, OSInstallationStation.OnStateChangedEventArgs e) {
        bool playSound = e.state == OSInstallationStation.State.Installing || e.state == OSInstallationStation.State.Installed;
        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }

    private void Update() {
        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f) {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(osInstallationStation.transform.position);
            }
        }
    }

}
