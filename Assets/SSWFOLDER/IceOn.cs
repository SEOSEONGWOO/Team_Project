using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceOn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SkillPanelMove.lightning = true;
            Destroy(gameObject);
        }
    }
}
