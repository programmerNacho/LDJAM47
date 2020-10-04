using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private float lineLength = 0.1f;

    private void Update()
    {
        Ray ray = new Ray(line.transform.position, line.transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, transform.InverseTransformPoint(hit.point));
        }
        else
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.forward * lineLength);
        }
    }
}
