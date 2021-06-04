using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SkillPanelMove.fire = true;
            
        }   
    }
}
