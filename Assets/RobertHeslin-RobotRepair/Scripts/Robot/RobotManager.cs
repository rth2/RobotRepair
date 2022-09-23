using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobertHeslin.RobotRepair
{
    public class RobotManager : MonoBehaviour
    {
        [SerializeField] int maxPartNumber, activePartNumber;


        public void AddActivePart()
        {
            activePartNumber++;

            if (activePartNumber > maxPartNumber)
                activePartNumber = maxPartNumber;
        }

        public void RemoveActivePart()
        {
            activePartNumber--;

            if (activePartNumber < 0)
                activePartNumber = 0;
        }

        public int GetNumberOfMaxParts()
        {
            return maxPartNumber;
        }

    }
}

