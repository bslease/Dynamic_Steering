using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] ConnectsTo;

    private void OnDrawGizmos()
    {
        foreach (Node n in ConnectsTo)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, n.transform.position);
            Gizmos.DrawRay(transform.position, (n.transform.position-transform.position).normalized * 2);
        }
    }

}
