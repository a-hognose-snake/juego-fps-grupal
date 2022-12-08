using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isActive = false;

    public void ToggleTarget()
    {
        transform.Rotate(-90, 0, 0);
        this.gameObject.GetComponent<AudioSource>().Play();
        isActive = false;
    }

    public void ActivateTarget()
    {
        transform.Rotate(90, 0, 0);
        isActive = true;
    }
}
