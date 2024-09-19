using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscScript : MonoBehaviour
{
    [SerializeField] float discSpeed = 10f;
    Vector3 forwardDirection = new Vector3(1f, 0f, 0f);

    public bool inPlayerRange = false;

    public ParticleSystem particleSystem;
    
    void Update()
    {
        transform.position += discSpeed * forwardDirection * Time.deltaTime; // Use Time.deltaTime for frame rate independence
    }

    void OnCollisionEnter(Collision col)
    {
        particleSystem.Play();
        
        Debug.DrawRay(transform.position, forwardDirection, Color.blue);
        
        // Calculate the normal of the collision
        Vector3 collisionNormal = col.contacts[0].normal;
        
        // Reflect the forward direction based on the collision normal
        forwardDirection = Vector3.Reflect(forwardDirection, collisionNormal);
        
        // Normalize the direction to maintain consistent speed
        forwardDirection.Normalize();
    }

    public void SetForwardDirection(Vector3 newForward)
    {
        forwardDirection = newForward;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            inPlayerRange = true;
            print("in");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            inPlayerRange = false;
            print("out");
        }
    }
}
