using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : BYSingletonMono<InputManager>
{
    public static Vector3 delta_mouse;
    [SerializeField] private float mouseDragMultiplier = 2.5f;
    [SerializeField] private float dragDeadZone = 1.5f;

    private Vector3 ogrinal;

    // Update is called once per frame
    void Update()
    {
        delta_mouse = Vector3.zero;

        bool isPointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        if (!isPointerOverUI)
        {
            if (Input.GetMouseButtonDown(0)) // nhấn 
            {
                ogrinal = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0)) // nhấn -trượt - thả
            {
                delta_mouse = (Input.mousePosition - ogrinal) * mouseDragMultiplier;

                if (delta_mouse.sqrMagnitude < dragDeadZone * dragDeadZone)
                {
                    delta_mouse = Vector3.zero;
                }

                ogrinal = Input.mousePosition;
            }
            else // thả
            {
                delta_mouse = Vector3.zero;
                ogrinal = Vector3.zero;
            }
        }

    }
}
