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

    public void ToggleAllTargets()
    {
        for(int i = 0; i < targets.Length; i++) {
            targets[i].ToggleTarget();
        }
    }
}
