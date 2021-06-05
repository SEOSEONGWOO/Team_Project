using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamege : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("OnCollisionStay");
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
  
}
