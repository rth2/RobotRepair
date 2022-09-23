using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LoLSDK;
using UnityEngine.UI;

namespace RobertHeslin.RobotRepair
{
    public class Main : MonoBehaviour
    {
        [SerializeField] Button newGameButton, continueGameButton;

        void Awake()
        {
            newGameButton.onClick.AddListener(OnClickNewGame);
            continueGameButton.onClick.AddListener(OnClickContinueGame);
        }

        void Start()
        {
            Helper.StateButtonInitialize<RobotRepairData>(newGameButton, continueGameButton, OnLoad);
        }

        void OnLoad(RobotRepairData loadedData )
        {
            //overrides serialized state data or continues with editor serialized values
            if (loadedData != null)
            {
                Debug.Log($"loadedData was found to not be null");
                GameStateData.instance.SetStage(loadedData.stage);
                GameStateData.instance.SetScore(loadedData.score);
            }
            else
            {
                Debug.Log($"loadedData was found to be null");
            }

        }

        private void OnClickNewGame()
        {   //start with fresh default data
            //robotRepairData = new RobotRepairData();
            //Debug.Log($"robot repair data should be new.");
            //Debug.Log($"Stage field is set to {robotRepairData.stage}");
            SceneManager.LoadScene("Stage01_Prep", LoadSceneMode.Single);
        }

        private void OnClickContinueGame()
        {
            //testing
            SceneManager.LoadScene("Stage01_Prep", LoadSceneMode.Single);
        }

    }
}

