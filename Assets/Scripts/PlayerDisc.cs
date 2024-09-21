using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class PlayerDisc : NetworkBehaviour
{
    [SerializeField] GameObject discPrefab;
    [SerializeField] Transform throwLocation; // Use Transform for position/rotation
    [SerializeField] GameObject camera;
    [SerializeField] GameObject handDisc;
    [SerializeField] float callBackWaitTime;

    private DiscScript disc;
    private bool hasDisc = true;

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hasDisc)
            {
				if(disc == null)
				{
					handDisc.SetActive(false);
    	            SpawnDiscServerRpc(); // Call the RPC to spawn the disc
	                hasDisc = false;
				}
            }
            else
            {
                if (disc != null && disc.inPlayerRange)
                {
                    // Optional: Logic for picking up the disc if it's in range
                }
                else
                {
                    StartCoroutine(CallBackDisc());
                    RequestDespawnServerRpc(); // Call to request despawn on server
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!hasDisc && disc != null && disc.inPlayerRange)
            {
                RequestDespawnServerRpc(); // Call to request despawn on server
                handDisc.SetActive(true);
                hasDisc = true;
            }
        }
    }

    [ServerRpc]
    private void SpawnDiscServerRpc()
    {
        var discInstance = Instantiate(discPrefab, throwLocation.position, transform.rotation);
        var networkObject = discInstance.GetComponent<NetworkObject>();
        networkObject.Spawn(); // This ensures it gets networked

        // Notify clients of the disc
        NotifyClientsOfDiscClientRpc(networkObject.NetworkObjectId, throwLocation.position, transform.rotation);
    }

    [ClientRpc]
    private void NotifyClientsOfDiscClientRpc(ulong networkObjectId, Vector3 position, Quaternion rotation)
    {
        var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
        if (networkObject != null)
        {
            disc = networkObject.GetComponent<DiscScript>();
            if (disc != null)
            {
                disc.transform.position = position;
                disc.transform.rotation = rotation;
                disc.SetForwardDirection(camera.transform.forward);
            }
        }
    }

    [ServerRpc]
    private void RequestDespawnServerRpc()
    {
        DestroyDisc(); // Call the despawn logic on the server
    }

    private void DestroyDisc()
    {
        if (disc != null)
        {
            disc.gameObject.GetComponent<NetworkObject>().Despawn(); // Despawn the network object
            disc = null; // Clear reference
        }
    }

    private IEnumerator CallBackDisc()
    {
        yield return new WaitForSeconds(callBackWaitTime);
        handDisc.SetActive(true);
        hasDisc = true;
    }
}
