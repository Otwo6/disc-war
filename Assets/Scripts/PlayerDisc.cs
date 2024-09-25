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
	private bool canThrow = true;

    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Player") != null)
        {
            print("Im number two LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && canThrow)
        {
			if (hasDisc)
            {
				if(disc == null)
				{
					SetHandDiscStateServerRpc(false);
    	            SpawnDiscServerRpc(); // Call the RPC to spawn the disc
	                hasDisc = false;
					StartCoroutine(DelayInput());
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
					StartCoroutine(DelayInput());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!hasDisc && disc != null && disc.inPlayerRange)
            {
                RequestDespawnServerRpc(); // Call to request despawn on server
                SetHandDiscStateServerRpc(true);
                hasDisc = true;
            }
        }
    }

    [ServerRpc]
    private void SpawnDiscServerRpc()
    {
        Debug.Log("Spawning disc on server.");
        var discInstance = Instantiate(discPrefab, throwLocation.position, transform.rotation);
        var networkObject = discInstance.GetComponent<NetworkObject>();
        networkObject.Spawn();

        NotifyClientsOfDiscClientRpc(networkObject.NetworkObjectId, throwLocation.position, transform.rotation);
    }

    [ClientRpc]
    private void NotifyClientsOfDiscClientRpc(ulong networkObjectId, Vector3 position, Quaternion rotation)
    {
        Debug.Log("Client receiving disc spawn notification for ID: " + networkObjectId);
        var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
        if (networkObject != null)
        {
            disc = networkObject.GetComponent<DiscScript>();
            if (disc != null)
            {
                disc.transform.position = position;
                disc.transform.rotation = rotation;
                disc.SetForwardDirection(camera.transform.forward);
                Debug.Log("Disc spawned at: " + position);
            }
        }
        else
        {
            Debug.LogError("Network object not found for ID: " + networkObjectId);
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
        SetHandDiscStateServerRpc(true);
        hasDisc = true;
    }

	private IEnumerator DelayInput()
	{
		// Used to prevent spamming
		canThrow = false;
		yield return new WaitForSeconds(1f);
		canThrow = true;
	}

    [ServerRpc]
    private void SetHandDiscStateServerRpc(bool state)
    {
        handDisc.SetActive(state);
        UpdateHandDiscStateClientRpc(state);
    }

    [ClientRpc]
    private void UpdateHandDiscStateClientRpc(bool state)
    {
        handDisc.SetActive(state);
    }
}
