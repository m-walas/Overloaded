using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxStationVisual : MonoBehaviour {


    [FormerlySerializedAs("platesStation")] [FormerlySerializedAs("platesCounter")] [SerializeField] private BoxStation boxStation;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;


    private List<GameObject> plateVisualGameObjectList;


    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        boxStation.OnBoxSpawned += BoxStationOnBoxSpawned;
        boxStation.OnBoxRemoved += BoxStationOnBoxRemoved;
    }

    private void BoxStationOnBoxRemoved(object sender, System.EventArgs e) {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void BoxStationOnBoxSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

}
