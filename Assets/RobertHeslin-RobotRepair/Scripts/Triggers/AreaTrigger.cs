using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobertHeslin.RobotRepair
{
    public class AreaTrigger : MonoBehaviour
    {

        [SerializeField] DisplayEquation equationDisplay;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent<SparePart>(out SparePart part))
            {
                equationDisplay.AddToEquation();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<SparePart>(out SparePart part))
            {
                equationDisplay.SubtractFromEquation();
            }
        }
    }
}

