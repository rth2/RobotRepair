using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RobertHeslin.RobotRepair
{
    public class Stage01 : MonoBehaviour
    {

        [Header("Mouse Stuff. Probably needs its own script.")]
        [SerializeField] Texture2D defaultMouseIcon;
        [SerializeField] Texture2D onHoverMouseIcon;
        [SerializeField] Texture2D onClickMouseIcon;



        private void Start()
        {
            Vector2 hotspot = new Vector2(64, 100);
            Cursor.SetCursor(defaultMouseIcon,hotspot,CursorMode.ForceSoftware);
            
        }

    }
}

