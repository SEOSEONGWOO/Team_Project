using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideC : MonoBehaviour
{
    public static bool inside = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("체크용");
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inside = false;
        }
    }
}
