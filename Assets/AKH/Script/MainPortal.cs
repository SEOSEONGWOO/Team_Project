using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPortal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Playerspawn.MainSceneBack = true;
    }
}
