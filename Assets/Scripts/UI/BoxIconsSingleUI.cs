using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxIconsSingleUI : MonoBehaviour {


    [SerializeField] private Image image;


    public void SetAssemblyObjectSO(AssemblyObjectSO assemblyObjectSo) {
        image.sprite = assemblyObjectSo.sprite;
    }

}
