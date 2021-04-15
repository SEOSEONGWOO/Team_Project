using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject hit;
  


     void OnTriggerEnter(Collider collision)
    {
            Instantiate(hit);
            Debug.Log("맞음");
    }
    private void OnTriggerStay(Collider other)
    {
        
    }


}
    
