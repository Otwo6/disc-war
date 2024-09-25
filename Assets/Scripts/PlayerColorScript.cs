using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorScript : MonoBehaviour
{
    [SerializeField] Renderer discRen;
    [SerializeField] Material orangeDiscMat;
    public bool isOrange = false;

    void Start()
    {
        print(GameObject.FindGameObjectsWithTag("Player").Length);
        if(GameObject.FindGameObjectsWithTag("Player").Length > 2)
        {
            discRen.material = orangeDiscMat;
            isOrange = true;
        }
    }
}
