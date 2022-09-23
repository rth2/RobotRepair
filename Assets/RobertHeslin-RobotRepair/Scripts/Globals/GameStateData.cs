using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace RobertHeslin.RobotRepair
{
    public class GameStateData : MonoBehaviour
    {
        public static GameStateData instance;

        [SerializeField]
        private RobotRepairData rrd;

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
            {
                instance = this;
            }
        }

        #region Getters
        public int GetScore()
        {
            return rrd.score;
        }

        public int GetStage()
        {
            return rrd.stage;
        }



        public RobotRepairData GetRobotRepairData()
        {
            return rrd;
        }

        #endregion


        #region Setters
        public void SetScore(int value)
        {
            rrd.score = value;
        }

        public void SetStage(int value)
        {
            rrd.stage = value;
        }

        #endregion

        public void AddToScore(int value)
        {
            rrd.score += value;
        }
        public void PrintScore()
        {
            Debug.Log($"Score is {rrd.score}");
        }

    }
}

