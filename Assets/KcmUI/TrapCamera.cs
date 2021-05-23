using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCamera : MonoBehaviour
{
    public GameObject Trapcamera;
    public GameObject MainCamera;
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
        if(other.tag == "Player")
        {
            MainCamera.SetActive(false);
            Trapcamera.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Trapcamera.SetActive(false);
            MainCamera.SetActive(true);
        }
    }
}
