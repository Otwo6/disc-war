using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerDisc : NetworkBehaviour
{
	[SerializeField] GameObject discPrefab;
	[SerializeField] GameObject throwLocation;
	[SerializeField] GameObject camera;
	[SerializeField] GameObject handDisc;
	[SerializeField] float callBackWaitTime;

	public DiscScript disc;
	bool hasDisc = true;

	void Update()
	{
		if(!IsOwner) return;

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(hasDisc)
			{
				handDisc.active = false;
				SpawnDiscServerRpc();
				hasDisc = false;
			}
			else
			{
				if(disc.inPlayerRange)
				{
					/*Destroy(disc.gameObject);
					handDisc.active = true;
					hasDisc = true;*/
				}
				else
				{
					if(disc != null)
					{
						StartCoroutine(CallBackDisc());
						DestroyDisc(disc);
					}
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
			if(!hasDisc)
			{
				if(disc.inPlayerRange)
				{
					DestroyDisc(disc);
					handDisc.active = true;
					hasDisc = true;
				}
			}
		}
	}

	private IEnumerator CallBackDisc()
	{
		yield return new WaitForSeconds(callBackWaitTime);
		handDisc.active = true;
		hasDisc = true;
	}

	[ServerRpc]
    private void SpawnDiscServerRpc()
    {
        // Instantiate the disc on the server and then spawn it
        var discInstance = Instantiate(discPrefab, throwLocation.transform.position, transform.rotation);
        var networkObject = discInstance.GetComponent<NetworkObject>();
        networkObject.Spawn(); // This ensures it gets networked

        disc = discInstance.GetComponent<DiscScript>();
        if (disc != null)
        {
            disc.SetForwardDirection(camera.transform.forward);
        }
    }

    private void DestroyDisc(DiscScript discToDestroy)
    {
        // This should be networked if you want it to destroy for all clients
        if (discToDestroy != null)
        {
            discToDestroy.gameObject.GetComponent<NetworkObject>().Despawn(); // Despawn the network object
            disc = null; // Clear reference
        }
    }
}
