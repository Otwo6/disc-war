using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorScript : MonoBehaviour
{
    [SerializeField] Renderer discRen;
    [SerializeField] Material orangeDiscMat;

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
			print("SHOULD CHANGEEEEEEEEEEEEEEEEE");
		}
		else
		{
			print("FUCK YOUUUUUUUUUUUUUUUUUUUUUUUUU <3");
		}
	}
}
