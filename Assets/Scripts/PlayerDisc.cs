using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisc : MonoBehaviour
{
	[SerializeField] GameObject discPrefab;
	[SerializeField] GameObject throwLocation;
	[SerializeField] GameObject camera;
	[SerializeField] GameObject handDisc;
	[SerializeField] float callBackWaitTime;

	DiscScript disc;
	bool hasDisc = true;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(hasDisc)
			{
				handDisc.active = false;
				disc = Instantiate(discPrefab, throwLocation.transform.position, transform.rotation).GetComponent<DiscScript>();
				if(disc != null)
				{
					disc.SetForwardDirection(camera.transform.forward);
				}
				hasDisc = false;
			}
			else
			{
				if(disc.inPlayerRange)
				{
					Destroy(disc.gameObject);
					handDisc.active = true;
					hasDisc = true;
				}
				else
				{
					if(disc != null)
					{
						StartCoroutine(CallBackDisc());
						Destroy(disc.gameObject);
					}
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
}
