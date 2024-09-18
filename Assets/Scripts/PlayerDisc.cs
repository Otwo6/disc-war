using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisc : MonoBehaviour
{
    [SerializeField] GameObject discPrefab;
    [SerializeField] GameObject throwLocation;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject handDisc;

    DiscScript disc;
    bool hasDisc = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            if(hasDisc)
            {
                handDisc.active = false;
                disc = Instantiate(discPrefab, throwLocation.transform.position, transform.rotation).GetComponent<DiscScript>();
                disc.SetForwardDirection(camera.transform.forward);
                hasDisc = false;
            }
            else
            {
                Destroy(disc);
                handDisc.active = true;
                hasDisc = true;
            }
        }
    }
}
