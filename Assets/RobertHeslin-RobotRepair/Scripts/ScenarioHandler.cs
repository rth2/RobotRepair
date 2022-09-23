using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobertHeslin.RobotRepair
{
    public class ScenarioHandler : MonoBehaviour
    {
        enum LearningMode
        {
            BuildUnit = 0,
            BuildImproper,
            InventoryUnit,
            InventoryImproper,
            Review
        };

        //Will only have robots in game with 2,3,4,5,6, or 8 parts.
        [SerializeField] int[] RobotSize = { 2, 3, 4, 5, 6, 8 };
        [SerializeField] LearningMode mode = LearningMode.BuildUnit;

        int curRobotSize;

        private void Awake()
        {
            SetCurrentRobotSize();

        }

        private void SetCurrentRobotSize()
        {
            curRobotSize = RobotSize[UnityEngine.Random.Range(0, 6)];
        }

        public int GetCurrentRobotSize()
        {
            return curRobotSize;
        }

    }
}

