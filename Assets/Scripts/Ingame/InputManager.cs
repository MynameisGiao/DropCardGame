using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BYSingletonMono<InputManager>
{
    public static Vector3 delta_mouse;
    private Vector3 ogrinal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // nhấn 
        {
            ogrinal= Input.mousePosition;
        }
        else if(Input.GetMouseButton(0)) // nhấn -trượt - thả
        {
            delta_mouse = Input.mousePosition - ogrinal;
            ogrinal = Input.mousePosition;
        }
        else // thả
        {
            delta_mouse = Vector3.zero;
            ogrinal=Vector3.zero;
        }
    }
}
