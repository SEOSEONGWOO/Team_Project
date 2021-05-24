using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_2_Change : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Playerspawn.MainScene_2 = true;
    }
}
