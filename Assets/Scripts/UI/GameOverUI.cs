using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start() {
        OverloadedGameManager.Instance.OnStateChanged += OverloadedGameManager_OnStateChanged;

        Hide();
    }

    private void OverloadedGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (OverloadedGameManager.Instance.IsGameOver()) {
            Show();

            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
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
