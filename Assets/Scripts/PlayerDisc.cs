using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisc : MonoBehaviour
{
    [SerializeField] GameObject discPrefab;
    [SerializeField] GameObject throwLocation;
    [SerializeField] GameObject camera;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            DiscScript disc = Instantiate(discPrefab, throwLocation.transform.position, transform.rotation).GetComponent<DiscScript>();
            disc.SetForwardDirection(camera.transform.forward);
        }
    }
}
