using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap2 : MonoBehaviour
{
    public GameObject[] traps;

    public Animation anim1;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("가시함정 미발동");
            anim1.Play("Anim_TrapNeedle_Hide");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("가시함정 발동");
            anim1.Play("Anim_TrapNeedle_Show");
        }

    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
