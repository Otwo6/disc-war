using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorScript : MonoBehaviour
{
    [SerializeField] Renderer discRen;
    [SerializeField] Material orangeDiscMat;
	[SerializeField] Material orangeDiscTrailMat;

	private bool isOrange = false;

    void Start()
    {
        print(GameObject.FindGameObjectsWithTag("Player").Length);
        if(GameObject.FindGameObjectsWithTag("Player").Length <= 2) // player 1 will be orange
        {
            discRen.material = orangeDiscMat;
			isOrange = true;
        }
    }

	public void ChangeDiscColor(GameObject disc)
	{
		if(isOrange)
		{
			disc.GetComponent<Renderer>().material = orangeDiscMat;
			disc.GetComponentInChildren<ParticleSystemRenderer>().material = orangeDiscMat;
			disc.GetComponentInChildren<TrailRenderer>().material = orangeDiscTrailMat;
		}
		else
		{
			print("No change");
		}
	}
}
