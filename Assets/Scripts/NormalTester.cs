using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTester : MonoBehaviour
{
    void Update()
    {
        
        if(Input.GetKeyDown("f"))
        {
            Vector3 direction = new Vector3(10.0f, 0.0f, 0.0f);
			Debug.DrawRay(transform.position, direction, Color.blue);
            RaycastHit hit;

            if(Physics.Raycast(transform.position, direction, out hit))
            {
                print(hit.normal);
            }
            else
            {
                print("No hit");
            }
        }
    }
}