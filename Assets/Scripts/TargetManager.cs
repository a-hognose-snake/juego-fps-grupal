using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public Target[] targets;

    private void Awake()
    {
        targets = FindObjectsOfType<Target>();
    }

    public void ActivateAllTargets()
    {
        for(int i = 0; i < targets.Length; i++) {
            targets[i].ActivateTarget();
        }
    }

    public bool AreAllTargetsDown()
    {
        bool areAllTargetsDown = true;
        foreach(Target target in targets)
        {
            if(target.isActive)
            {
                areAllTargetsDown = false;
                break;
            }
        }
        return areAllTargetsDown;
    }
}
