using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AssemblyStationVisual : MonoBehaviour {


    private const string CUT = "Cut";


    [FormerlySerializedAs("cuttingStation")] [FormerlySerializedAs("cuttingCounter")] [SerializeField] private AssemblyStation assemblyStation;


    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        assemblyStation.OnCut += AssemblyStationOnCut;
    }

    private void AssemblyStationOnCut(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }

}
