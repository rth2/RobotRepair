using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobertHeslin.RobotRepair
{
    public class RobotPart : MonoBehaviour
    {

        [SerializeField] Sprite activePart, inactivePart;

        SpriteRenderer sr;
        RobotManager robotManager;
        //[SerializeField]ParticleSystem psSparking;

        bool active;
        bool broken = false;        //if part is broken enable a 'sparking' particle effect

        Color greyColor = new Color();
        Color originalColor = new Color();

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            //psSparking = GetComponent<ParticleSystem>();
            robotManager = gameObject.GetComponentInParent<RobotManager>();

            greyColor = Color.grey;
            originalColor = Color.white;

            DeactivatePart();
        }

        public void ActivatePart()
        {
            sr.sprite = activePart;
            SetColor(originalColor);
            robotManager.AddActivePart();
            active = true;
        }

        public void DeactivatePart()
        {
            sr.sprite = inactivePart;
            robotManager.RemoveActivePart();
            active = false;
        }

        public void DarkenPart(bool toDarken)
        {   //don't shade active parts
            if (active)
            {
                SetColor(originalColor);
                return;
            }


            if (toDarken)
                SetColor(greyColor);
            else
                SetColor(originalColor);
        }

        #region Getters
        public bool GetActiveState()
        {
            return active;
        }

        #endregion

        #region Setters
        private void SetColor(Color color)
        {
            sr.color = color;
        }

        #endregion
    }
}

