using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_3_Change : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Playerspawn.MainScene_3 = true;
    }
}
