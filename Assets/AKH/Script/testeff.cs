using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testeff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test1");
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("test2");
        }
    }
}
