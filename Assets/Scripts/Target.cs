using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool isDown = false;

    public void ToggleTarget()
    {
        if(!isDown)
        {
            transform.Rotate(-90, 0, 0);
            isDown = true;
        }
        else
        {
            transform.Rotate(90, 0, 0);
        }
    }
}
