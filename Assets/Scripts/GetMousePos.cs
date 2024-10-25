using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePos : MonoBehaviour
{
    Vector3? mousePos1 = null;
    Vector3? mousePos2 = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos1 == null)
            {
                mousePos1 = Input.mousePosition;
            }
            else
            {
                mousePos2 = Input.mousePosition;
                Debug.Log(Vector3.Distance(mousePos1.Value, mousePos2.Value));
            }
        }

        if (mousePos1.HasValue && mousePos2.HasValue)
        {
            mousePos1 = mousePos2;
        }
    }
}
