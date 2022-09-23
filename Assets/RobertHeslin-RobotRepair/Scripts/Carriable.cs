using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//
//If you want to click on and pick something up with the mouse, attach this class. Drag and drop style.
//If you go over an object that can be darkened, then it will darken.
//

namespace RobertHeslin.RobotRepair
{
    public class Carriable : MonoBehaviour
    {
        bool beingCarried = false;
        float zCameraOffset = 10f;
        const int overParts = 1;
        RobotPart hoveredPart = null;
        Rigidbody2D rBody2d = null;

        //Constrain carriable things to the screen.
        const float _xRightBound = 8.5f;
        const float _xLeftBound = -8.5f;
        const float _yUpperBound = 4.7f;
        const float _yLowerBound = -3.2f;

        private void Start()
        {
            rBody2d = GetComponent<Rigidbody2D>();
        }
        /*
         * Marks and starts object as being carried.
         * Changes gravityScale so that 'tearing' down doesn't happen.
         * 
        */
        private void OnMouseDown()
        {
            beingCarried = true;
            rBody2d.gravityScale = 0f;          //stops 'dropping visual tear' when moving a part.
            StartCoroutine("_beingCarried");
        }

        /*
         * Object is no longer being carried.
         * GravityScale back to normal as item has been dropped.
         * 
         * If part was hovering over another part, then that part is
         *  activated and this one is destroyed.
         * 
        */
        private void OnMouseUp()
        {
            beingCarried = false;
            rBody2d.gravityScale = 1f;          //object can fall again.

            if (hoveredPart != null)
            {

                hoveredPart.ActivatePart();
                Destroy(gameObject);

            }
            else
            {
                Debug.Log("Turn back into spare parts?");
            }

        }

        /*
         * 
         *  Positions part inside of the play area when called.
         * 
        */
        private Vector3 ClampToScreen(Vector3 requestedPosition)
        {
            Vector3 newPosition = requestedPosition;

            if (requestedPosition.x > _xRightBound)
                newPosition.x = _xRightBound;
            else if (requestedPosition.x < _xLeftBound)
                newPosition.x = _xLeftBound;

            if (requestedPosition.y > _yUpperBound)
                newPosition.y = _yUpperBound;
            else if (requestedPosition.y < _yLowerBound)
                newPosition.y = _yLowerBound;

            return newPosition;
        }


        /*
         * 
         * Handles behaviour when being carried.
         *
         * subFunctions: ClampToScreen
         *
         * affects: hoveredPart (selection)
         * 
        */
        IEnumerator _beingCarried()
        {
            while (beingCarried)
            {
                //move to cursor position
                Vector2 nextPosition = Mouse.current.position.ReadValue();
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, zCameraOffset));

                gameObject.transform.position = ClampToScreen(worldPosition);

                //shoot a raycast, looking for objects to darken
                RaycastHit2D[] hitArray = Physics2D.RaycastAll(worldPosition, Vector2.zero);

                //reset to original color
                if (hoveredPart != null)
                {
                    hoveredPart.DarkenPart(false);
                    hoveredPart = null;
                }

                if (hitArray.Length > 0)
                {
                    for (int i = 0; i < hitArray.Length; i++)
                    {
                        hitArray[i].collider.gameObject.TryGetComponent<RobotPart>(out RobotPart part);

                        if (part != null)
                        {   //do not select active parts
                            if (part.GetActiveState())
                                continue;

                            hoveredPart = part;
                            hoveredPart.DarkenPart(true);
                        }
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            //no longer carrying so don't highlight
            if (hoveredPart != null)
            {
                hoveredPart.DarkenPart(false);
                hoveredPart = null;
            }

        }

    }
}

