using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {



    private const string NUMBER_POPUP = "NumberPopup";


    [SerializeField] private TextMeshProUGUI countdownText;


    private Animator animator;
    private int previousCountdownNumber;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        OverloadedGameManager.Instance.OnStateChanged += OverloadedGameManager_OnStateChanged;

        Hide();
    }

    private void OverloadedGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (OverloadedGameManager.Instance.IsCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        int countdownNumber = Mathf.CeilToInt(OverloadedGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber) {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
