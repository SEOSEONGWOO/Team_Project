using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom_test : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Playerspawn.BossRoom_test = true;
    }
}
