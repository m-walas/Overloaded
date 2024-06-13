using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {


    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;


    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            OverloadedGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    private void Start() {
        OverloadedGameManager.Instance.OnGamePaused += OverloadedGameManager_OnGamePaused;
        OverloadedGameManager.Instance.OnGameUnpaused += OverloadedGameManager_OnGameUnpaused;

        Hide();
    }

    private void OverloadedGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void OverloadedGameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
