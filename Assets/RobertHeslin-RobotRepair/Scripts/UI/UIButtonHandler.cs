using LoLSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

namespace RobertHeslin.RobotRepair
{
    public class UIButtonHandler : MonoBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] Button saveStateButton;
        [SerializeField] Button checkAnswerButton;

        [Header("Button Text Fields")]
        [SerializeField] TextMeshProUGUI saveStateButtonText;
        [SerializeField] TextMeshProUGUI checkAnswerButtonText;

        JSONNode langDefs = SharedData.LanguageDefs;

        private void Awake()
        {
            if (saveStateButton)
                saveStateButton.onClick.AddListener(SaveState);

            if (checkAnswerButton)
                checkAnswerButton.onClick.AddListener(CheckAnswer);

        }

        private void Start()
        {
            if (saveStateButtonText)
                saveStateButtonText.text = langDefs["save"];

            if (checkAnswerButtonText)
                checkAnswerButtonText.text = langDefs["checkAnswer"];

        }


        private void SaveState()
        {
            LOLSDK.Instance.SaveState(GameStateData.instance.GetRobotRepairData());
        }

        private void CheckAnswer()
        {
            Debug.Log($"Check the answer here!");
        }

    }
}

