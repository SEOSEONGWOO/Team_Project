using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_main : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Playerspawn.MainSceneBack = true;
    }
}
