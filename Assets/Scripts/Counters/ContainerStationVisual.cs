using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContainerStationVisual : MonoBehaviour {


    private const string OPEN_CLOSE = "OpenClose";


    [FormerlySerializedAs("containerCounter")] [SerializeField] private ContainerStation containerStation;


    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerStation.OnPlayerGrabbedObject += ContainerStationOnPlayerGrabbedObject;
    }

    private void ContainerStationOnPlayerGrabbedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }

}
