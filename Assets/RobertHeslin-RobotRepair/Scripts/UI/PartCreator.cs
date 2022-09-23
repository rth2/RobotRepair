using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RobertHeslin.RobotRepair
{

    public class PartCreator : MonoBehaviour
    {

        Button createSparePartButton;
        [SerializeField] GameObject partToCreate;
        [SerializeField] Transform whereToCreate;

        void Start()
        {
            createSparePartButton = GetComponent<Button>();

            createSparePartButton.onClick.AddListener(CreateSparePart);
        }

        void CreateSparePart()
        {
            Instantiate<GameObject>(partToCreate, whereToCreate.position, Quaternion.identity,whereToCreate);
        }

    }
}

