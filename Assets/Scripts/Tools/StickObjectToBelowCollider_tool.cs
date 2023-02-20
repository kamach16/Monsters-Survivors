using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class StickObjectToBelowCollider_tool : MonoBehaviour
{
    private void OnEnable()
    {
        StickToBelowCollider();
    }

    private void StickToBelowCollider()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }
    }
}
