using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobertHeslin.RobotRepair
{
    public class DestroyObjectOnTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }

    }
}

