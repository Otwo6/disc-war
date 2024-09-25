using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorScript : MonoBehaviour
{
    void Start()
    {
        print(GameObject.FindGameObjectsWithTag("Player").Length);
    }
}
