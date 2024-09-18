using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscScript : MonoBehaviour
{
    [SerializeField] float discSpeed = 10f;
    Vector3 forwardDirection = new Vector3(1f, 0f, 0f);
    
    void Update()
    {
        transform.position += discSpeed * forwardDirection;
    }

    void OnCollisionEnter(Collision col)
    {
		Debug.DrawRay(transform.position, forwardDirection, Color.blue);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, forwardDirection, out hit))
        {
            forwardDirection = hit.normal;
        }
    }

    public void SetForwardDirection(Vector3 newForward)
    {
        forwardDirection = newForward;
    }
}
