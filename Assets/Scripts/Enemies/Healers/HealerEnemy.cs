using System;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy : MonoBehaviour
{
    public Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.parent.position, currentPos);
            if (!(dist < minDist)) continue;
            tMin = t.transform.parent;
            minDist = dist;
        }
        return tMin;
    }
}
